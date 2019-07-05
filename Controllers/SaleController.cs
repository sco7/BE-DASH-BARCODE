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
            List<SorDetail> data = await _context.SorDetail.ToListAsync();
            return Ok(data);
        }

        // GET: api/sale/{dateString}
        [HttpGet("{dateString}")]      
        public async Task<IActionResult> GetSalesByDate(string dateString)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            DateTime date = DateTime.ParseExact(dateString, "dd-MM-yyyy", CultureInfo.InvariantCulture);

            List<SorDetail> data = await _context.SorDetail.Where(x => x.MLineShipDate == date).ToListAsync();

            if (data.Count == 0)
            {
                return NotFound();
            }

            return Ok(data);
        }

        // GET: api/sale/stockcode/{no}
        [HttpGet("stockcode/{no}")]      
        public async Task<IActionResult> GetSalesByStockCode(string no)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            List<SorDetail> data = await _context.SorDetail.Where(x => x.MStockCode == no).ToListAsync();

            if (data.Count == 0)
            {
                return NotFound();
            }

            return Ok(data[0]);
        }

        // PRINT: api/sale/print/{dateString}
        [HttpGet("print/{dateString}")]
        public async Task<IActionResult> PrintLabelsByDate(string dateString)
        {
            
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            DateTime date = DateTime.ParseExact(dateString, "dd-MM-yyyy", CultureInfo.InvariantCulture);

            // Get orders for requested date
            List<vGetChassisNumbers> data = await _context.vGetChassisNumbers.Where(x => (x.MLineShipDate == date) && (x.ChassisNumber != null)).Distinct().ToListAsync();
            if (data.Count == 0) return NotFound("No orders found to dispatch on this date!");

            // Check for duplicates chassis nos on the verification table 
            foreach(var i in data) 
            {
                if ( _context.Verification.Any(x => x.ChassisNo == i.ChassisNumber))
                {
                   return BadRequest("Duplicate chassis number(s) found to exist on the verification table! Data cannot be added");
                }
            }                        

            // Add chassis no's to the verification table
            foreach (var i in data)
            {
                _context.Verification.Add(new Verification {ChassisNo = i.ChassisNumber, SalesOrder = i.SalesOrder, CustomerStockCode = i.MCusSupStkCode, 
                                                            StockDescription = i.MStockDes, DispatchDate = i.MLineShipDate, StockCode = i.MStockCode});
                _context.SaveChanges();
            }

            // Print Labels
            var printLabels = new PrintLabels();
            printLabels.PrintDespatchLabels(data);

            return Ok(data);
        }

        //REPRINT: api/sale/reprint/{chassisNo}
        [HttpGet("reprint/{chassisNo}")]
        public async Task<IActionResult> ReprintLabelsByChassisNo(string chassisNo)
        {
            
            var printLabels = new PrintLabels();

            // Check chassis number exist in verification table
            if ( _context.Verification.Any(x => x.ChassisNo == chassisNo))
            {
                var data = await _context.vGetChassisNumbers.Where(x => x.ChassisNumber == chassisNo).Distinct().ToListAsync();
                printLabels.PrintDespatchLabels(data);
                
                return Ok(data);
            }
                return BadRequest("Chassis no does not exist in the verification table");
            }
    }   
}