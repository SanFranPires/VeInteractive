using ServiceStack;

namespace VeInteractive.TechTest.OneTimePassword.ServiceModel
{
    [Route("/onetimepassword", "POST")]
    public class CreateOneTimePassword
    {
        public string UserId { get; set; }
    }

    [Route("/onetimepassword/login", "POST")]
    public class AuthenticateUser : IReturn<bool>
    {
        public string UserId { get; set; }
        public string Password { get; set; }
    }
}
