using NUnit.Framework;
using VeInteractive.TechTest.OneTimePassword.ServiceInterface;

namespace VeInteractive.TechTest.OneTimePassword.Tests
{
    [TestFixture]
    public class RngPasswordGeneratorTest
    {
        [Test]
        public void ShouldReturnPasswordWithRequestedLength()
        {
            RngPasswordGenerator passwordGenerator = new RngPasswordGenerator();
            passwordGenerator.PasswordLength = 8;
            Assert.That(passwordGenerator.Generate().Length == passwordGenerator.PasswordLength);
            passwordGenerator.PasswordLength = 16;
            Assert.That(passwordGenerator.Generate().Length == passwordGenerator.PasswordLength);
        }
    }
}
