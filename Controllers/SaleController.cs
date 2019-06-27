using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FontaineVerificationProject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Neodynamic.SDK.Printing;
using FontaineVerificationProject.Labels;
using FontaineVerificationProject.Helpers;
using System.Globalization;


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

            List<Sale> data = await _context.Sale.Where(x => x.DispatchDate == date && x.Customer == "SCAHOL").ToListAsync();

            if (data.Count == 0)
            {
                return NotFound();
            }

            return Ok(data);
        }

        // GET: api/sale/stockcode/{no}
        [HttpGet("stockcode/{no}")]      
        public async Task<IActionResult> GetSalesByStockCode(int no)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            List<Sale> data = await _context.Sale.Where(x => x.StockCode == no && x.Customer == "SCAHOL").ToListAsync();

            if (data.Count == 0)
            {
                return NotFound();
            }

            return Ok(data[0]);
        }


        // PRINT: api/sale/print/{date}
        [HttpGet("print/{dateString}")]
        public async Task<IActionResult> PrintLabelsByDate(string dateString)
        {
            
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            DateTime date = DateTime.ParseExact(dateString, "dd-MM-yyyy", CultureInfo.InvariantCulture);

            List<Sale> data = await _context.Sale.Where(x => x.DispatchDate == date && x.Customer == "SCAHOL").ToListAsync();

            if (data.Count == 0) return NotFound("No orders found to dispatch on this date!");
            
            // Check chassis no's to the verification table before printing labels and validate for duplicates no's
            foreach (var i in data) {

                if( _context.Verification.Any(x => x.ChassisNo == i.ChassisNo))
                {
                     return BadRequest("Duplicate chassis number(s) found to exist on the validation table, data will not be added");
                }
            }

            foreach (var i in data) {

                _context.Verification.Add(new Verification {ChassisNo = i.ChassisNo});
                _context.SaveChanges();
            }
            var printLabels = new PrintLabels();
            printLabels.PrintDespatchLabels(data);

            return Ok(data);
        }

        // REPRINT: api/sale/reprint/{chassisNo}
        [HttpGet("reprint/{chassisNo}")]
        public async Task<IActionResult> PrintV1LabelsByChassisNo(int chassisNo)
        {
            
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            List<Sale> data = await _context.Sale.Where(x => x.ChassisNo == chassisNo && x.Customer == "SCAHOL").ToListAsync();

            if (data.Count == 0) return NotFound("No matching chassis number found");
            
            var printLabels = new PrintLabels();
            printLabels.PrintDespatchLabels(data);

            return Ok(data);
        }
    }   
}