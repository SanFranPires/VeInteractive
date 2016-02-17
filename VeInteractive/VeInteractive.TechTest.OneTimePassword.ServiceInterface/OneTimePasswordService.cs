using ServiceStack;
using VeInteractive.TechTest.OneTimePassword.ServiceModel;
using VeInteractive.TechTest.OneTimePassword.ServiceModel.Types;
using VeInteractive.TechTest.OneTimePassword.ServiceRepository;

namespace VeInteractive.TechTest.OneTimePassword.ServiceInterface
{
    public class OneTimePasswordService : IService
    {
        public IOneTimePasswordRepository OneTimePasswordRepository { get; set; }
        public IPasswordGenerator PasswordGenerator { get; set; }

        public UserPassword Post(CreateOneTimePassword request)
        {
            string password = PasswordGenerator.Generate();
            OneTimePasswordRepository.CreateOneTimePassword(request.UserId, password);
            return new UserPassword
            {
                UserId = request.UserId,
                Password = password
            };
        }

        public bool Post(AuthenticateUser request)
        {
            var password = OneTimePasswordRepository.GetOneTimePassword(request.UserId);
            if (password != null && password == request.Password)
            {
                OneTimePasswordRepository.DeleteOneTimePassword(request.UserId);
            }
            return password != null && password == request.Password;
        }
    }
}
