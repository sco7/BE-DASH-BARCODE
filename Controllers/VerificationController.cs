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

        // GET: api/verification
        [HttpGet]
        public async Task<IActionResult> GetVerification()
        {
            List<Verification> data = await _context.Verification.ToListAsync();
            return Ok(data);
        }

        // GET: api/verification/2
        [HttpGet("{id}")]
        public async Task<IActionResult> GetVerificationById(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var data = await _context.Verification.Where(x => x.VerificationID == id).ToListAsync();

            if (data == null)
            {
                return NotFound();
            }

            return Ok(data);
        }

        // GET: api/verification/chassis/12345
        [HttpGet("chassis/{no}")]
        public async Task<IActionResult> GetVerificationByChassisNo(string no)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var data = await _context.Verification.Where(x => x.ChassisNo.Equals(no)).ToListAsync();

            if (data == null)
            {
                return NotFound();
            }

            return Ok(data);
        }

        // Post: api/verification/chassis/1
        [HttpPost("chassis")]
        public async Task<IActionResult> PostVerificationChassisNo([FromBody]Verification verification)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var emptyVerification = new Verification();
            if (await TryUpdateModelAsync<Verification>(
                emptyVerification,
                "1234",   // Prefix for form value.
                x => x.ChassisNo))
            {
                _context.Verification.Add(emptyVerification);
                await _context.SaveChangesAsync();
            }

                //var data = _context.Verification.Attach(verification);

                //_context.Verification.Add(verification);
                //await _context.SaveChangesAsync();

            //var data = _context.Database.ExecuteSqlCommand(@"INSERT INTO Verification (ChassisNo) VALUES (15)");

            

            return Ok();
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
