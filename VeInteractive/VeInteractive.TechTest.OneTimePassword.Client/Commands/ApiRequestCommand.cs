using ManyConsole;
using ServiceStack;

namespace VeInteractive.TechTest.OneTimePassword.Client.Commands
{
    public abstract class ApiRequestCommand : ConsoleCommand
    {
        protected JsonServiceClient client;
        private string host = "localhost";
        private uint port = 80;

        public ApiRequestCommand()
        {
            HasOption<string>("h|host=", "API Host", option => host = option);
            HasOption<uint>("p|port=", "API Port", option => port = option);
        }

        protected void InitialiseClient()
        {
            client = new JsonServiceClient(string.Format("http://{0}:{1}", host, port));
        }
    }
}
