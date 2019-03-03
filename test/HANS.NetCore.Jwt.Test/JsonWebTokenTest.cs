using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace HANS.NetCore.Jwt.Test
{
    [TestFixture]
    public class JsonWebTokenTest
    {
        [Test]
        public void TestMethod()
        {
            // TODO: Add your test code here
            Assert.Pass("Your first passing test");
        }

        [Test]
        public void TokenValidateTest()
        {
            Dictionary<string, object> payLoad = new Dictionary<string, object>();
            payLoad.Add("sub", "rober");
            payLoad.Add("jti", "09e572c7-62d0-4198-9cce-0915d7493806");
            payLoad.Add("nbf", null);
            payLoad.Add("exp", null);
            payLoad.Add("iss", "roberIssuer");
            payLoad.Add("aud", "roberAudience");
            payLoad.Add("age", 30);

            var encodeJwt = JsonWebToken.CreateToken(payLoad, 30);
            Console.Write(encodeJwt);
            // var result = JsonWebToken.Validate(encodeJwt, (load) => { return true; });
            Assert.IsTrue(encodeJwt.IsNormalized());
        }
    }
}