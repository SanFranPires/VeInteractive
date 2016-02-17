using System;
using VeInteractive.TechTest.OneTimePassword.ServiceModel;
using VeInteractive.TechTest.OneTimePassword.ServiceModel.Types;

namespace VeInteractive.TechTest.OneTimePassword.Client.Commands
{
    public class CreatePasswordCommand : ApiRequestCommand
    {
        public CreatePasswordCommand()
        {
            IsCommand("create", "creates a one-time password for specified <userId>");

            HasAdditionalArguments(1, "<userId>");
        }

        public override int Run(string[] remainingArguments)
        {
            InitialiseClient();

            CreateOneTimePassword request = new CreateOneTimePassword
            {
                UserId = remainingArguments[0]
            };

            var response = client.Post<UserPassword>(request);

            if (response.Password != null)
            {
                Console.WriteLine("User Password: {0}", response.Password);
            }
            else
            {
                Console.WriteLine("Password not set or expired.");
            }
            return 0;
        }
    }
}
