using System;
using System.Collections.Generic;

namespace HANS.NetCore.Jwt.Interface.Middleware
{
    public interface IJwtCustomerAuthorezeOption
    {
        List<string> SetAnonymousPaths(List<string> urls);

        void SetValidateFunc(Func<Dictionary<string, string>, JsonWebTokenSetting, bool> func);
    }
}