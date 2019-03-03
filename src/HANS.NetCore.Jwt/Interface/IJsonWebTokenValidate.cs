using System;
using System.Collections.Generic;

namespace HANS.NetCore.Jwt.Interface
{
    public interface IJsonWebTokenValidate
    {
        bool Validate(string encodeJwt, JsonWebTokenSetting setting, Func<Dictionary<string, string>, JsonWebTokenSetting, bool> validatePayLoad);
    }
}