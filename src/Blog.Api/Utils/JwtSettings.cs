namespace Blog.api.Utils
{
    public class JwtSettings
    {
        public string SecretKey { get; set; }
        public string Issuer { get; set; }
        public int ExpirationTimeInHours { get; set; }
    }
}