using ServiceStack;
using Funq;
using VeInteractive.TechTest.OneTimePassword.ServiceInterface;
using ServiceStack.Configuration;
using VeInteractive.TechTest.OneTimePassword.ServiceRepository;
using ServiceStack.Redis;

namespace VeInteractive.TechTest.OneTimePassword
{
    public class AppHost : AppHostBase
    {
        public AppHost()
            : base("One-Time Password Service", typeof(OneTimePasswordService).Assembly)
        { }

        public override void Configure(Container container)
        {
            var appSettings = new AppSettings();

            var clientsManager = new RedisManagerPool(appSettings.Get("RedisConnectionString", "redis://localhost:6379"));
            container.Register<IRedisClientsManager>(c => clientsManager);

            container.Register<IPasswordGenerator>(
                new RngPasswordGenerator()
                {
                    PasswordLength = appSettings.Exists("PasswordLength") ? appSettings.Get<uint>("PasswordLength") : 16
                });

            container.Register<IOneTimePasswordRepository>(c => new RedisOneTimePasswordRepository()
            {
                ClientsManager = clientsManager,
                PasswordExpiryInSeconds = appSettings.Get<uint>("PasswordExpiryInSeconds", 30)
            });
        }
    }
}
