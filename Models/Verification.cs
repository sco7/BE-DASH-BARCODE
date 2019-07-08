using System;
namespace FontaineVerificationProject.Models
{
    public class Verification
    {
        public int VerificationID { get; set; }
        public string ChassisNo { get; set; }
        public string V1UserName { get; set; }
        public DateTime? V1DateTime { get; set; }
        public string V1Passed { get; set; }
        public string V2UserName { get; set; }
        public DateTime? V2DateTime { get; set; }
        public string V2Passed { get; set; }
        public string SalesOrder { get; set; }
        public string CustomerStockCode { get; set; }
        public string StockDescription { get; set; }
        public DateTime? DispatchDate { get; set; }
        public string StockCode { get; set; }
    }
}
