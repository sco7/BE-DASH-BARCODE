namespace FontaineVerificationProjectBack.Models
{
  public class KestrelConfiguration
    {
        public int Port { get; set; }
        public SslSettings SslSettings { get; set; }
        public CorsSettings Cors { get; set; }

        public KestrelConfiguration()
        {

        }
    }

    public class SslSettings
    {
        public string CertificatePath { get; set; }
        public string CertificatePassword { get; set; }
        public SslSettings() { }
    }

    public class CorsSettings
    {
        public string AllowedHeaders { get; set; }
        public string AllowedOrigins { get; set; }
        public string AllowedMethods { get; set; }
    }
}
