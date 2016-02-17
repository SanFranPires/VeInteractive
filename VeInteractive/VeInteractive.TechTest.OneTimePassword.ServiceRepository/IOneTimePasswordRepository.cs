namespace VeInteractive.TechTest.OneTimePassword.ServiceRepository
{
    public interface IOneTimePasswordRepository
    {
        void CreateOneTimePassword(string userId, string password);
        string GetOneTimePassword(string userId);
        void DeleteOneTimePassword(string userId);
    }
}
