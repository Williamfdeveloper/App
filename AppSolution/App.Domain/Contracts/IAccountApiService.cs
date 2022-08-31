using App.Domain.Entities.Login;
using App.Domain.Entities.Register;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.Domain.Contracts
{
    public interface IAccountApiService
    {
        LoginResponse AuthenticateApi(Login model);
        RegisterResponse CadastrarApi(Register model);
    }
}
