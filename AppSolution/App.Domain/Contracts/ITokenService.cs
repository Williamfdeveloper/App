using App.Domain.Entities;

namespace App.Domain.Contracts
{
    public interface ITokenService
    {
        string GenerateToken(Usuario user);
    }
}
