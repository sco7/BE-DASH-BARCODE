using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FontaineVerificationProject.Models
{
    public class User
    {
        public int UserID { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public byte[] Password { get; set; }
        public byte[] Salt { get; set; }
    }
}
