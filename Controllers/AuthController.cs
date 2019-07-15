using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using FontaineVerificationProject.Dtos;
using FontaineVerificationProject.Models;
using FontaineVerificationProject.Repositories;
using FontaineVerificationProjectBack.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;

namespace FontaineVerificationProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository _repo;
        private readonly IConfiguration _config;
        private readonly IMapper _mapper;
        private readonly JwtIssuerOptions JwtIssuerOptions;
        private readonly JsonSerializerSettings JsonSerializerSettings;

        public AuthController(IAuthRepository repo, IConfiguration config, IMapper mapper, IOptions<JwtIssuerOptions> jwtOptions)
        {
            //jwtOptions.Value.NotNull("JwtIssuerOptions");
            //jwtOptions.Value.SigningCredentials.NotNull("JwtIssuerOptions.SigningCredentials");
            //jwtOptions.Value.JtiGenerator.NotNull("JwtIssuerOptions.JtiGenerator");
            //jwtOptions.Value.ValidFor.NotZeroTimeRange("JwtIssuerOptions.Validfor");
            JwtIssuerOptions = jwtOptions.Value;
            JsonSerializerSettings = new JsonSerializerSettings
            {
                Formatting = Formatting.Indented,
                DateTimeZoneHandling = DateTimeZoneHandling.Local
            };
            _mapper = mapper;
            _config = config;
            _repo = repo;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDto registerDto)
        {
            registerDto.UserName = registerDto.UserName.ToLower();
            if (await _repo.UserExists(registerDto.UserName))
                return BadRequest("This UserName is already registered");

            var userToCreate = _mapper.Map<User>(registerDto);
            var createdUser = await _repo.Register(userToCreate, registerDto.Password);
            return StatusCode(201, new { UserName = createdUser.UserName, FullName = createdUser.FullName });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            var userFromRepo = await _repo.Login(loginDto.UserName.ToLower(), loginDto.Password);
            if (userFromRepo == null)
                return BadRequest("User not registered! Please check that your UserName and Password have been entered correctly");

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, userFromRepo.UserID.ToString()),
                new Claim(ClaimTypes.Name, userFromRepo.UserName)
            };

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Issuer = JwtIssuerOptions.Issuer,
                Audience = JwtIssuerOptions.Audience,
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(30),
                SigningCredentials = JwtIssuerOptions.SigningCredentials,
                NotBefore = JwtIssuerOptions.NotBefore,
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return Ok(new { token = tokenHandler.WriteToken(token), UserName = userFromRepo.UserName, FullName = userFromRepo.FullName });
        }
    }
}