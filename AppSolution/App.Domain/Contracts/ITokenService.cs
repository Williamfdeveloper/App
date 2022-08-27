using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.Domain.Contracts
{
    public interface ITokenService
    {
        string GenerateToken(IdentityUser user);
    }
}
