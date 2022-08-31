using App.Adapter;
using App.Domain.Contracts.Adapter;
using App.Domain.Entities.Login;
using App.Domain.Entities.Register;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;

namespace App.Adapters
{
    public class AccountAdapter : ApiBase, IAccountAdapter
    {
        public LoginResponse AuthenticateApi(Login model)
        {
            string URL = $"https://localhost:44350/api/account/login";

            if (Request<Login, LoginResponse>(model, null, null, URL, HttpMethod.Get, string.Empty, out LoginResponse Response, out string msgErro) == HttpStatusCode.OK)
            {
                var _response = JsonConvert.DeserializeObject<LoginResponse>(Response.ToString());

                return _response;
            }
            else
            {
                if (!string.IsNullOrEmpty(msgErro))
                {
                    throw new Exception(msgErro);
                }

                return null;
            }
        }

        public RegisterResponse CadastrarApi(Register model)
        {
            string URL = $"https://localhost:44350/api/account/register";

            if (Request<Register, RegisterResponse>(model, null, null, URL, HttpMethod.Get, string.Empty, out RegisterResponse Response, out string msgErro) == HttpStatusCode.OK)
            {
                var _response = JsonConvert.DeserializeObject<RegisterResponse>(Response.ToString());

                return _response;
            }
            else
            {
                if (!string.IsNullOrEmpty(msgErro))
                {
                    throw new Exception(msgErro);
                }

                return null;
            }
        }
    }
}
