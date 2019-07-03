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
            List<SorDetail> orderData = await _context.SorDetail.Where(x => x.MLineShipDate == date).Distinct().ToListAsync();
            if (orderData.Count == 0) return NotFound("No orders found to dispatch on this date!");
            
            // Get chassis no's relating to the selected orders
            List<string> chassisNoList = new List<string>();
            string chassisNo;
            foreach (var i in orderData)
            {
                chassisNo = await _context.SorDetail.Where(x => (x.SalesOrder == i.SalesOrder) && (x.NCommentFromLin == i.SalesOrderLine) &&
                                                                (x.NComment.StartsWith("Chassis")))
                                                    .Select(y => y.NComment.Substring(16)).FirstOrDefaultAsync();

                chassisNoList.Add(chassisNo);

                // Check for duplicate chassis no's on the verification table
                if( _context.Verification.Any(x => x.ChassisNo == chassisNo))
                {
                     return BadRequest("Duplicate chassis number(s) found to exist on the validation table, data will not be added");
                }
            }

            // Add chassis no's to the verification table
            int c = 0;
            foreach (var i in chassisNoList)
            {
                _context.Verification.Add(new Verification {ChassisNo = i, SalesOrder = orderData[c].SalesOrder});
                _context.SaveChanges();
                c++;
            }

            // Print Lables
            var printLabels = new PrintLabels();
            printLabels.PrintDespatchLabels(orderData, chassisNoList);

            
                                                    
            return Ok(chassisNoList);
        }

        // REPRINT: api/sale/reprint/{chassisNo}
        // [HttpGet("reprint/{chassisNo}")]
        // public async Task<IActionResult> PrintV1LabelsByChassisNo(int chassisNo)
        // {
            
        //     if (!ModelState.IsValid)
        //     {
        //         return BadRequest(ModelState);
        //     }

        //     List<SorDetail> data = await _context.SorDetail.Where(x => x.NComment == "Chassis Number: " + chassisNo).ToListAsync();

        //     if (data.Count == 0) return NotFound("No matching chassis number found");
            
        //     var printLabels = new PrintLabels();
        //     printLabels.PrintDespatchLabels(data);

        //     return Ok(data);
        //}
    }   
}