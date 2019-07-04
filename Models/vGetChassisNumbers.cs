using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace FontaineVerificationProject.Models
{
    public class vGetChassisNumbers
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public string SalesOrder { get; set; }
        public string MStockCode { get; set; }
        public string MStockDes { get; set; }
        public string MCusSupStkCode { get; set; }
        public DateTime MLineShipDate { get; set; }
        public decimal SalesOrderLine { get; set; }
        public string ChassisNumber { get; set; }
    }
}
