using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FontaineVerificationProject.Models;
using FontaineVerificationProject.Processers;
using Microsoft.AspNetCore.Mvc;

namespace FontaineVerificationProject.Controllers
{
    //[Route("api/[controller]")]
    [ApiController]
    public class VerificationController : ControllerBase
    {
        // POST api/verification/chassis
        [HttpPost("api/verification/chassis")]
        public bool Post([FromBody] Verification verification )
        {
            if (verification == null)
            {
                return false;
            }

            return VerificationProcessor.ProcessAddChassis(verification);
        }

        // DELETE api/users/chassis
        [HttpDelete("api/verification/chassis")]
        public bool Delete([FromBody] Verification verification)
        {
            if (verification == null)
            {
                return false;
            }

            return VerificationProcessor.ProcessDeleteChassis(verification);
        }

        // PUT api/verification/v1
        [HttpPut("api/verification/v1")]
        public bool PutV1([FromBody] Verification verification)
        {
            if (verification == null)
            {
                return false;
            }

            return VerificationProcessor.ProcessUpdateV1(verification);
        }

        // PUT api/verification/v2
        [HttpPut("api/verification/v2")]
        public bool PutV2([FromBody] Verification verification)
        {
            if (verification == null)
            {
                return false;
            }

            return VerificationProcessor.ProcessUpdateV2(verification);
        }

        // GET api/verification/chassis
        [HttpGet("api/verification/chassis")]
        public string[] Get()
        {      
            return VerificationProcessor.ProcessGetChassis();
        }

        // GET api/verification/chassisNo/{chassisNo:string}   //working on this controller - not working yet
        [HttpGet("api/verification/chassis/chassisNo:string")]
        public string[] Get(Verification verification)
        {
            return VerificationProcessor.ProcessGetChassisNo(verification);
        }
    }
}
