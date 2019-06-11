using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using FontaineVerificationProject.Dtos;
using FontaineVerificationProject.Models;
using FontaineVerificationProject.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;


namespace FontaineVerificationProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository _repo;
        private readonly IConfiguration _config;
        private readonly IMapper _mapper;

        public AuthController(IAuthRepository repo, IConfiguration config, IMapper mapper)
        {
            _mapper = mapper;
            _config = config;
            _repo = repo;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDto registerDto)
        {
            registerDto.Email = registerDto.Email.ToLower();
            if (await _repo.UserExists(registerDto.Email))
                return BadRequest("Email already exists");

            var userToCreate = _mapper.Map<User>(registerDto);
            var createdUser = await _repo.Register(userToCreate, registerDto.Password);
            return StatusCode(201, new { email = createdUser.Email, username = createdUser.UserName });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            var userFromRepo = await _repo.Login(loginDto.Email.ToLower(), loginDto.Password);
            if (userFromRepo == null)
                return BadRequest("User not registered! Please check that your Email and Password have been entered correctly");

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, userFromRepo.UserID.ToString()),
                new Claim(ClaimTypes.Name, userFromRepo.Email)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8
                .GetBytes(_config.GetSection("AppSettings:Token").Value));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(30),
                SigningCredentials = creds
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return Ok(new { token = tokenHandler.WriteToken(token), email = userFromRepo.Email, username = userFromRepo.UserName });
        }
    }
}