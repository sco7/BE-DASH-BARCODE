using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FontaineVerificationProject.Dtos
{
    public class VerificationDto
    {
        public int VerificationNo { get; set; }
        public int ChassisNo { get; set; }
        public string V1UserName { get; set; }
        public DateTime V1DateTime { get; set; }
        public string V1Passed { get; set; }
        public string V2UserName { get; set; }
        public DateTime V2DateTime { get; set; }
        public string V2Passed { get; set; }
    }
}
