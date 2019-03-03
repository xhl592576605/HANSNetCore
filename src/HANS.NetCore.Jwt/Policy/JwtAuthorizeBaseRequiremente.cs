using HANS.NetCore.Jwt.Interface.Policy;
using System;
using System.Collections.Generic;

namespace HANS.NetCore.Jwt.Policy
{
    /// <summary>
    /// 将验证参数传至Handler
    /// </summary>
    public class JwtAuthorizeBaseRequiremente : IJwtAuthorizRequiremente
    {
        protected internal Func<Dictionary<string, string>, JsonWebTokenSetting, bool> validatePayLoad = (a, b) =>
        {
            return true;
        };

        public virtual IJwtAuthorizRequiremente SetValidateFunc(Func<Dictionary<string, string>, JsonWebTokenSetting, bool> func)

        {
            this.validatePayLoad = func ?? this.validatePayLoad;
            return this;
        }
    }
}