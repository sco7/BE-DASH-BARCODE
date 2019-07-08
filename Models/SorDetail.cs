using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace FontaineVerificationProject.Models
{
    public class SorDetail
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public string SalesOrder { get; set; }
        public string MStockCode { get; set; }
        public string MCusSupStkCode { get; set; }
        public string NComment { get; set; }
        public string MStockDes { get; set; }
        public DateTime MLineShipDate { get; set; }
        public decimal SalesOrderLine { get; set; }
        public string MWarehouse { get; set; }
        public decimal NCommentFromLin { get; set; }
    }
}
