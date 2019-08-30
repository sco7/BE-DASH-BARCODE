using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FontaineVerificationProject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using FontaineVerificationProject.Helpers;
using System.Globalization;
using FontaineVerificationProjectBack.Models;
using Microsoft.Extensions.Options;

namespace FontaineVerificationProject.Controllers
{

    //[Route("api/[controller]")]
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class SaleController : ControllerBase
    {
        private readonly PrintingConfig _printing;
        private readonly FontaineContext _context;
        public SaleController(FontaineContext context, IOptions<PrintingConfig> printing)
        {
            _context = context;
            _printing = printing.Value;
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
            List<vGetChassisNumbers> data2 = new List<vGetChassisNumbers>();
            bool duplicates = false;
            foreach(var i in data) 
            {
                if ( _context.Verification.Any(x => x.ChassisNo == i.ChassisNumber))
                {
                    duplicates = true;
                }
                else
                {
                    data2.Add(i);
                }
            }

            if (data2.Count == 0) return NotFound("Only duplicate chassis numbers found! No data has been added to the verification table.");           

            // Add chassis no's to the verification table
            foreach (var i in data2)
            {
                _context.Verification.Add(new Verification {ChassisNo = i.ChassisNumber, SalesOrder = i.SalesOrder, CustomerStockCode = i.MCusSupStkCode, 
                                                            StockDescription = i.MStockDes, DispatchDate = i.MLineShipDate, StockCode = i.MStockCode});
                _context.SaveChanges();
            }

            // Print Labels
            var printLabels = new PrintLabels();
            await printLabels.PrintDespatchLabels(data2, _printing);

            if (duplicates)
            {
                   return BadRequest("Duplicate chassis number(s) found to exist on the verification table!  These cannot be added to the verification table!");
            }
            else
            {
                return Ok(data2);
            }
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
                await printLabels.PrintDespatchLabels(data, _printing);
                
                return Ok(data);
            }
            else
            {
                return BadRequest("Chassis number does not exist in the verification table");
            }
        }   
    }
}