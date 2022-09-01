using App.Domain.Entities;
using App.Domain.Entities.Login;
using App.Domain.Entities.Register;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;

namespace App.Test
{
    [TestClass]
    public class Test : ApiBase
    {

        [TestMethod]
        public void AuthenticateApiOK(Login model)
        {
            string URL = $"https://localhost:44350/api/account/login";

            model.Email = "williamf.developer@gmail.com";
            model.Password = "123456";

            if (Request<Login, LoginResponse>(model, null, null, URL, HttpMethod.Get, string.Empty, out LoginResponse Response, out string msgErro) == HttpStatusCode.OK)
                Assert.IsNotNull(JsonConvert.DeserializeObject<LoginResponse>(Response.ToString()));
        }

        [TestMethod]
        public void CadastrarApiOK(Register model)
        {
            string URL = $"https://localhost:44350/api/account/register";
            model.Email = "williamf.developer@gmail.com";
            model.Password = "BR@sil500";
            model.ConfirmPassword = "BR@sil500";
            model.Nome = "William Fernando da Silva";
            model.Cpf = "331.673.878.90";
            model.DataNascimento = Convert.ToDateTime("1986-02-22");
            model.Endereco = new Endereco()
            {
                Rua = "Antonio Rosique Garcia",
                Numero = "74",
                Bairro = "Jd Aeronave de Viracopos",
                Cidade = "Campinas",
                Estado = "SP",
                CEP = "13056116"
            };

            if (Request<Register, RegisterResponse>(model, null, null, URL, HttpMethod.Get, string.Empty, out RegisterResponse Response, out string msgErro) == HttpStatusCode.OK)
            {
                Assert.IsNotNull(JsonConvert.DeserializeObject<RegisterResponse>(Response.ToString()));
            }
            else
            {
                if (!string.IsNullOrEmpty(msgErro))
                {
                    //throw new Exception(msgErro);
                    Assert.IsNotNull(msgErro);
                }
                Assert.IsNull(msgErro);
            }
        }

        [TestMethod]
        public void CadastrarApiErroEmail(Register model)
        {
            string URL = $"https://localhost:44350/api/account/register";
            model.Email = "williamf.developer@@gmail.com";
            model.Password = "BR@sil500";
            model.ConfirmPassword = "BR@sil500";
            model.Nome = "William Fernando da Silva";
            model.Cpf = "331.673.878.90";
            model.DataNascimento = Convert.ToDateTime("1986-02-22");
            model.Endereco = new Endereco()
            {
                Rua = "Antonio Rosique Garcia",
                Numero = "74",
                Bairro = "Jd Aeronave de Viracopos",
                Cidade = "Campinas",
                Estado = "SP",
                CEP = "13056116"
            };

            if (Request<Register, RegisterResponse>(model, null, null, URL, HttpMethod.Get, string.Empty, out RegisterResponse Response, out string msgErro) == HttpStatusCode.OK)
            {
                //JsonConvert.DeserializeObject<RegisterResponse>(Response.ToString());
            }
            else
            {
                Assert.IsNotNull(msgErro);
            }
        }
    }
}
