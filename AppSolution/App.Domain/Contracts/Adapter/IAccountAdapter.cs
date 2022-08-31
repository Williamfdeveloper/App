using App.Domain.Entities.Login;
using App.Domain.Entities.Register;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Contracts.Adapter
{
    public interface IAccountAdapter
    {
        LoginResponse AuthenticateApi(Login model);
        RegisterResponse CadastrarApi(Register model);
    }
}
