using App.Domain.Entities;
using App.Test;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Net.Http;

namespace App.Test
{
    [TestClass]
    public class UnitTest1 : ApiBase
    {
        [TestMethod]
        public void TestMethod1()
        {
            string URL = $"https://localhost:44350/produto";

            if (Request<object, object>(null, null, null, URL, HttpMethod.Get, string.Empty, out object Response, out string msgErro) == HttpStatusCode.OK)
            {
                var _response = JsonConvert.DeserializeObject<Produto>(Response.ToString());

                Assert.IsNotNull( _response);
            }
            else
            {
                if (!string.IsNullOrEmpty(msgErro))
                {
                    throw new Exception(msgErro);
                }

                Assert.Fail();
            }


        }
    }
}
