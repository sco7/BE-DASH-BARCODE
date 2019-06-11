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
    public class SaleController : ControllerBase
    {
        private readonly FontaineContext _context;
        public SaleController(FontaineContext context)
        {
            _context = context;
        }

        // GET: api/sale
        [HttpGet]
        public async Task<IActionResult> GetAllSales()
        {
            List<Sale> data = await _context.Sale.ToListAsync();
            return Ok(data);
        }

        // GET: api/sale/{date}
        [HttpGet("{date}")]      
        public async Task<IActionResult> GetSalesByDate(DateTime date)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            List<Sale> data = await _context.Sale.Where(x => x.DispatchDate == date).ToListAsync();

            if (data == null)
            {
                return NotFound();
            }

            return Ok(data);
        }

    }   
}