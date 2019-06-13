using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
namespace FontaineVerificationProject.Models
{
    public class Verification
    {
        public int VerificationID { get; set; }
        public int ChassisNo { get; set; }
        public string V1UserName { get; set; }
        public DateTime? V1DateTime { get; set; }
        public string V1Passed { get; set; }
        public string V2UserName { get; set; }
        public DateTime? V2DateTime { get; set; }
        public string V2Passed { get; set; }
    }
}
