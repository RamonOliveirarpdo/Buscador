namespace Buscador.Core.Interfaces
{
    public interface IHashService
    {
        string EncriptPassword(string password);
        string DecriptPassword(string password);
        string VerifyPassword(string userPassword, string loginPassword);
    }
}
