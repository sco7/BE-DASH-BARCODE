using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FontaineVerificationProject.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FontaineVerificationProject.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class VerificationController : ControllerBase
    {
        private readonly FontaineContext _context;
        public VerificationController(FontaineContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetVerification()
        {
            var data = await _context.Verification.ToListAsync();
            return Ok(data);
        }
    }


    ////[Route("api/[controller]")]
    //[ApiController]
    //public class VerificationController : ControllerBase
    //{
    //    // POST api/verification/chassis
    //    [HttpPost("api/verification/chassis")]
    //    public bool Post([FromBody] Verification verification )
    //    {
    //        if (verification == null)
    //        {
    //            return false;
    //        }

    //        return VerificationProcessor.ProcessAddChassis(verification);
    //    }

    //// DELETE api/users/chassis
    //[HttpDelete("api/verification/chassis")]
    //public bool Delete([FromBody] Verification verification)
    //{
    //    if (verification == null)
    //    {
    //        return false;
    //    }

    //    return VerificationProcessor.ProcessDeleteChassis(verification);
    //}

    //// PUT api/verification/v1
    //[HttpPut("api/verification/v1")]
    //public bool PutV1([FromBody] Verification verification)
    //{
    //    if (verification == null)
    //    {
    //        return false;
    //    }

    //    return VerificationProcessor.ProcessUpdateV1(verification);
    //}

    //// PUT api/verification/v2
    //[HttpPut("api/verification/v2")]
    //public bool PutV2([FromBody] Verification verification)
    //{
    //    if (verification == null)
    //    {
    //        return false;
    //    }

    //    return VerificationProcessor.ProcessUpdateV2(verification);
    //}

    //// GET api/verification/chassis
    //[HttpGet("api/verification/chassis")]
    //public string[] Get()
    //{      
    //    return VerificationProcessor.ProcessGetChassis();
    //}

    //// GET api/verification/chassisNo/{chassisNo:string}   //working on this controller - not working yet
    //[HttpGet("api/verification/chassis/chassisNo:string")]
    //public string[] Get(Verification verification)
    //{
    //    return VerificationProcessor.ProcessGetChassisNo(verification);
    //}
    //}
}
