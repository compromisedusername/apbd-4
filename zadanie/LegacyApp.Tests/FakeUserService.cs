namespace LegacyApp.Tests;

public class FakeUserService : IUserCreditService
{
    public void Dispose()
    {
        // TODO release managed resources here
    }

    public int GetCreditLimit(string lastName, DateTime dateOfBirth)
    {
        throw new NotImplementedException();
    }
}