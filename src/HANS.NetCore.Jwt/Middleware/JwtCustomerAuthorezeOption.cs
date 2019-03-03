using HANS.NetCore.Jwt.Interface.Middleware;
using System;
using System.Collections.Generic;

namespace HANS.NetCore.Jwt.Middleware
{
    /// <summary>
    /// 配置验证的参数
    /// </summary>
    public class JwtCustomerAuthorezeOption : IJwtCustomerAuthorezeOption
    {
        protected internal readonly List<string> anonymousPath = new List<string>();

        protected internal Func<Dictionary<string, string>, JsonWebTokenSetting, bool> validatePayLoad = (a, b) =>
        {
            return true;
        };

        public JwtCustomerAuthorezeOption()
        {
        }

        /// <summary>
        /// 自定义可以跳过验证的url路径
        /// </summary>
        /// <param name="urls"></param>
        /// <returns></returns>
        public List<string> SetAnonymousPaths(List<string> urls)
        {
            urls.ForEach(url =>
            {
                anonymousPath.Add(url);
            });
            return anonymousPath;
        }

        /// <summary>
        /// 自定义验证函数
        /// </summary>
        /// <param name="func"></param>
        public void SetValidateFunc(Func<Dictionary<string, string>, JsonWebTokenSetting, bool> func)
        {
            validatePayLoad = func;
        }
    }
}