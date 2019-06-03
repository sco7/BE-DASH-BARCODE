//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using FontaineVerificationProject.Models;
//using FontaineVerificationProject.Processers;
//using Microsoft.AspNetCore.Mvc;

//namespace FontaineVerificationProject.Controllers
//{
//    //[Route("api/[controller]")]
//    [ApiController]
//    public class UsersController : ControllerBase
//    {     
//        // POST api/users/save
//        [HttpPost("api/users/save")]
//        public bool Post([FromBody] User user )
//        {
//            if (user == null)
//            {
//                return false;
//            }

//            return UserProcessor.ProcessAddUser(user);
//        }

//        // DELETE api/users/delete
//        [HttpDelete("api/users/delete")]
//        public bool Delete([FromBody] User user)
//        {
//            if (user == null)
//            {
//                return false;
//            }

//            return UserProcessor.ProcessDeleteUser(user);
//        }
//    }
//}
