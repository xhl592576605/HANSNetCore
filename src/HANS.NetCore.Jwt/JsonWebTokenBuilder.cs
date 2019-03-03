using HANS.NetCore.Jwt.Interface;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace HANS.NetCore.Jwt
{
    public class JsonWebTokenBuilder : IJsonWebTokenBuilder
    {
        private readonly JsonWebTokenSetting setting = new JsonWebTokenSetting();

        public JsonWebTokenBuilder(IOptions<JsonWebTokenSetting> options)
        {
            setting = options.Value;
        }

        /// <summary>
        /// 创建jwt
        /// </summary>
        /// <param name="payLoad">负载</param>
        /// <param name="expiresMinute">失效时间</param>
        /// <param name="securityKey">秘钥</param>
        /// <returns></returns>
        public string CreateJsonWebToken(Dictionary<string, string> payLoad)
        {
            if (string.IsNullOrWhiteSpace(setting.SecurityKey))
            {
                throw new ArgumentNullException("JsonWebTokenSetting.securityKey",
                    "securityKey为NULL或空字符串。请在\"appsettings.json\"配置\"JsonWebToken\"节点及其子节点\"securityKey\"");
            }
            var now = DateTime.UtcNow;

            // Specifically add the jti (random nonce), iat (issued timestamp), and sub (subject/user) claims.
            // You can add other claims here, if you want:
            var claims = new List<Claim>();
            foreach (var key in payLoad.Keys)
            {
                var tempClaim = new Claim(key, payLoad[key]?.ToString());
                claims.Add(tempClaim);
            }

            // Create the JWT and write it to a string
            var jwt = new JwtSecurityToken(
                issuer: null,
                audience: null,
                claims: claims,
                notBefore: now,
                expires: now.Add(TimeSpan.FromMinutes(setting.ExpiresMinute)),
                signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.ASCII.GetBytes(setting.SecurityKey)), SecurityAlgorithms.HmacSha256));
            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);
            return encodedJwt;
        }
    }
}