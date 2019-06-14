using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FontaineVerificationProject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

namespace FontaineVerificationProject.Controllers
{
   //[Route("api/[controller]")]
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly FontaineContext _context;
        public UserController(FontaineContext context)
        {
            _context = context;
        }  

        // Delete: api/user/delete/{user}
        [HttpDelete("delete/{user}")]
        public async Task<IActionResult> DeleteUser(string user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);          
            }

            var data = await _context.User.Where(x => x.UserLog.Equals(user)).FirstOrDefaultAsync();
            
            if (data == null) 
            {
                return NotFound();
            }

            _context.User.Remove(data);
            await _context.SaveChangesAsync();          
            return Ok($"User account {user} deleted");
            
        }
    }
}


