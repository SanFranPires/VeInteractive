namespace VeInteractive.TechTest.OneTimePassword.ServiceRepository
{
    public class RedisServerConfig
    {
        public string Host { get; set; }
        public int Port { get; set; }
        public int PasswordExpiryInSeconds { get; set; }
    }
}
