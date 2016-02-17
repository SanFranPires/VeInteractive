using VeInteractive.TechTest.OneTimePassword.ServiceInterface;

namespace VeInteractive.TechTest.OneTimePassword.Tests
{
    public class MockPasswordGenerator : IPasswordGenerator
    {
        public string NextPassword { get; set; }

        public string Generate()
        {
            return NextPassword;
        }
    }
}
