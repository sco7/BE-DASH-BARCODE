﻿using System;
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

        // Post: api/verification/chassis/12345
        [HttpPost("chassis")]
        public async Task<IActionResult> PostVerificationChassisNo([FromBody]Verification verification)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

             _context.Verification.Add(verification);
            await _context.SaveChangesAsync();          
            return Ok();
        }
    

        // Delete: api/verification/2
        [HttpDelete("chassis/{no}")]
        public async Task<IActionResult> DeleteVerificationChassisNo(string no)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);          
            }

            var data = await _context.Verification.Where(x => x.ChassisNo.Equals(no)).ToListAsync();
            
            if (data.Count == 0) 
            {
                return NotFound();
            }

             _context.Verification.Remove(data[0]);
            await _context.SaveChangesAsync();          
            return Ok(data[0]);
        }

        // Put api/verification/v1
        [HttpPut("api/verification/v1/{no}")]
        public async Task<IActionResult> UpdateVerificationV1(string no, [FromBody]Verification verification)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var id = await GetVerificationByChassisNo(no);

            var data = _context.Verification.Find(id);           

            _context.Entry(data).CurrentValues.SetValues(verification);                
            await _context.SaveChangesAsync();          
            return Ok(data);

        }

    }

}


    

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

