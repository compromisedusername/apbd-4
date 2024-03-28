namespace LegacyApp.Tests;

public class FakeClientRepository : IClientRepository
{
    public Client GetById(int clientId)
    {
        throw new NotImplementedException();
    }
}