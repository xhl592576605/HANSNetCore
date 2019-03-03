using HANS.NetCore.Jwt.Interface.Policy;
using HANS.NetCore.Jwt.Policy;
using System;
using System.Collections.Generic;

namespace HANS.NetCore.Jwt.Sample
{
    public class CommonAuthorize : JwtAuthorizeBaseRequiremente
    {
        public override IJwtAuthorizRequiremente SetValidateFunc(Func<Dictionary<string, string>, JsonWebTokenSetting, bool> func)
        {
            return base.SetValidateFunc(func);
        }
    }
}