using ServiceStack.Redis;
using System;

namespace VeInteractive.TechTest.OneTimePassword.ServiceRepository
{
    public class RedisOneTimePasswordRepository : IOneTimePasswordRepository
    {
        public IRedisClientsManager ClientsManager { get; set; }

        public uint PasswordExpiryInSeconds { get; set; }

        public void CreateOneTimePassword(string userId, string password)
        {
            using (var redisClient = ClientsManager.GetClient())
            {
                redisClient.Set(userId, password, TimeSpan.FromSeconds(PasswordExpiryInSeconds));
            }
        }

        public string GetOneTimePassword(string userId)
        {
            using (var redisClient = ClientsManager.GetClient())
            {
                return redisClient.Get<string>(userId);
            }
        }

        public void DeleteOneTimePassword(string userId)
        {
            using (var redisClient = ClientsManager.GetClient())
            {
                redisClient.Remove(userId);
            }
        }
    }
}
