using App.Domain.Entities;
using System.Collections.Generic;
using System.Security.Claims;

namespace App.Domain.Contracts
{
    public interface ITokenService
    {
        string GenerateToken(Usuario user, IEnumerable<Claim> claims);
    }
}
