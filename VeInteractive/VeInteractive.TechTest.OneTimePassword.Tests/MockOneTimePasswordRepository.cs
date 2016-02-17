using System.Collections.Generic;
using VeInteractive.TechTest.OneTimePassword.ServiceRepository;

namespace VeInteractive.TechTest.OneTimePassword.Tests
{
    public class MockOneTimePasswordRepository : IOneTimePasswordRepository
    {
        public Dictionary<string, string> MockDatabase { get; set; }

        public MockOneTimePasswordRepository()
        {
            MockDatabase = new Dictionary<string, string>();
        }

        public void CreateOneTimePassword(string userId, string password)
        {
            MockDatabase.Add(userId, password);
        }

        public string GetOneTimePassword(string userId)
        {
            string password = null;
            if (MockDatabase.ContainsKey(userId))
            {
                password = MockDatabase[userId];
            }
            return password;
        }

        public void DeleteOneTimePassword(string userId)
        {
            if (MockDatabase.ContainsKey(userId))
            {
                MockDatabase.Remove(userId);
            }
        }
    }
}
