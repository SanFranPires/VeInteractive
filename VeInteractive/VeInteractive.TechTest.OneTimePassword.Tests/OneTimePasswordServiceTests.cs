using NUnit.Framework;
using ServiceStack;
using ServiceStack.Testing;
using VeInteractive.TechTest.OneTimePassword.ServiceInterface;
using VeInteractive.TechTest.OneTimePassword.ServiceModel;
using VeInteractive.TechTest.OneTimePassword.ServiceRepository;

namespace VeInteractive.TechTest.OneTimePassword.Tests
{
    [TestFixture]
    public class OneTimePasswordServiceTests
    {
        private ServiceStackHost applicationHost;
        private MockPasswordGenerator mockPasswordGenerator;
        private MockOneTimePasswordRepository mockOneTimePasswordRepository;

        [OneTimeSetUp]
        public void FixtureSetup()
        {
            mockPasswordGenerator = new MockPasswordGenerator();
            mockOneTimePasswordRepository = new MockOneTimePasswordRepository();

            applicationHost = new BasicAppHost(typeof(OneTimePasswordService).Assembly)
            {
                ConfigureContainer = container =>
                {
                    container.Register<IPasswordGenerator>(mockPasswordGenerator);

                    container.Register<IOneTimePasswordRepository>(mockOneTimePasswordRepository);
                }
            }.Init();
        }

        [OneTimeTearDown]
        public void FixtureTearDown()
        {
            applicationHost.Dispose();
        }

        [SetUp]
        public void TestSetup()
        {
            mockOneTimePasswordRepository.MockDatabase.Clear();
        }

        [Test]
        public void ShouldCreateNewPassword()
        {
            var oneTimePasswordService = applicationHost.TryResolve<OneTimePasswordService>();

            var user = "user1";
            mockPasswordGenerator.NextPassword = "user1password";
            var userPassword = oneTimePasswordService.Post(new CreateOneTimePassword() { UserId = user });
            Assert.That(userPassword.UserId == user);
            Assert.That(userPassword.Password == mockPasswordGenerator.NextPassword);
            Assert.That(mockOneTimePasswordRepository.MockDatabase.Count == 1);
            Assert.That(mockOneTimePasswordRepository.MockDatabase.ContainsKey(user));
            Assert.That(mockOneTimePasswordRepository.MockDatabase[user] == userPassword.Password);

            user = "user2";
            mockPasswordGenerator.NextPassword = "user2password";
            userPassword = oneTimePasswordService.Post(new CreateOneTimePassword() { UserId = user });
            Assert.That(userPassword.UserId == user);
            Assert.That(userPassword.Password == mockPasswordGenerator.NextPassword);
            Assert.That(mockOneTimePasswordRepository.MockDatabase.Count == 2);
            Assert.That(mockOneTimePasswordRepository.MockDatabase.ContainsKey(user));
            Assert.That(mockOneTimePasswordRepository.MockDatabase[user] == userPassword.Password);
        }

        [Test]
        public void ShouldAuthenticate()
        {
            var oneTimePasswordService = applicationHost.TryResolve<OneTimePasswordService>();

            mockOneTimePasswordRepository.MockDatabase.Add("user1", "user1password");
            mockOneTimePasswordRepository.MockDatabase.Add("user2", "user2password");

            var result = oneTimePasswordService.Post(new AuthenticateUser { UserId = "user1", Password = "user1password" });
            Assert.IsTrue(result);

            result = oneTimePasswordService.Post(new AuthenticateUser { UserId = "user2", Password = "user2password" });
            Assert.IsTrue(result);
        }

        [Test]
        public void ShouldNotAuthenticate()
        {
            var oneTimePasswordService = applicationHost.TryResolve<OneTimePasswordService>();

            mockOneTimePasswordRepository.MockDatabase.Add("user", "password");

            var result = oneTimePasswordService.Post(new AuthenticateUser { UserId = "wrongUser", Password = "password" });
            Assert.IsFalse(result);

            result = oneTimePasswordService.Post(new AuthenticateUser { UserId = "user", Password = "wrongPassword" });
            Assert.IsFalse(result);

            result = oneTimePasswordService.Post(new AuthenticateUser { UserId = "user", Password = "password" });
            Assert.IsTrue(result);

            result = oneTimePasswordService.Post(new AuthenticateUser { UserId = "user", Password = "password" });
            Assert.IsFalse(result);
        }
    }
}
