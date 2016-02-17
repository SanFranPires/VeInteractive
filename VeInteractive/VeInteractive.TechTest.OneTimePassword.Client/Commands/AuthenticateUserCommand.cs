using System;
using VeInteractive.TechTest.OneTimePassword.ServiceModel;

namespace VeInteractive.TechTest.OneTimePassword.Client.Commands
{
    public class AuthenticateUserCommand : ApiRequestCommand
    {
        public AuthenticateUserCommand()
        {
            IsCommand("authenticate", "performs one-time password authentication for specified <userId> with <password>");

            HasAdditionalArguments(2, "<userId> <password>");
        }

        public override int Run(string[] remainingArguments)
        {
            InitialiseClient();

            AuthenticateUser request = new AuthenticateUser
            {
                UserId = remainingArguments[0],
                Password = remainingArguments[1],
            };

            var response = client.Post<bool>(request);

            Console.WriteLine("User authentication {0}.", response ? "successful" : "failed");

            return 0;
        }
    }
}
