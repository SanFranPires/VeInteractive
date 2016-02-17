using System.Security.Cryptography;
using System.Text;

namespace VeInteractive.TechTest.OneTimePassword.ServiceInterface
{
    public class RngPasswordGenerator : IPasswordGenerator
    {
        public uint PasswordLength { get; set; }

        public string Generate()
        {
            StringBuilder passwordBuilder = new StringBuilder();
            using (var rngProvider = new RNGCryptoServiceProvider())
            {
                while (passwordBuilder.Length != PasswordLength)
                {
                    byte[] nextBytes = new byte[PasswordLength - passwordBuilder.Length];
                    rngProvider.GetBytes(nextBytes);
                    foreach (var nextByte in nextBytes)
                    {
                        char character = (char)nextByte;
                        if (char.IsLetterOrDigit(character))
                        {
                            passwordBuilder.Append(character);
                        }
                    }
                }
            }
            return passwordBuilder.ToString();
        }
    }
}
