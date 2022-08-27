using App.Domain.Entities.Login;
using App.Domain.Entities.Register;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Contracts
{
    public interface IAccountService
    {
        Task<LoginResponse> AuthenticateAsync(Login model);
        Task<RegisterResponse> CadastrarAsync(Register model);
    }
}
