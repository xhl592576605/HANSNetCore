using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;

namespace HANS.NetCore.Jwt.Interface.Policy
{
    public interface IJwtAuthorizRequiremente : IAuthorizationRequirement
    {
        IJwtAuthorizRequiremente SetValidateFunc(Func<Dictionary<string, string>, JsonWebTokenSetting, bool> func);
    }
}