namespace FontaineVerificationProject.Models
{
    public class User
    {
        public int UserID { get; set; }
        public string FullName { get; set; }
        public string UserName { get; set; }
        public byte[] Password { get; set; }
        public byte[] Salt { get; set; }
    }
}
