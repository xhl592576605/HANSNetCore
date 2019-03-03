using Microsoft.AspNetCore.Authorization;

namespace HANS.NetCore.Jwt.Interface.Policy
{
    public class JwtAuthorizeAttribute : AuthorizeAttribute
    {
        // public bool IsUseJwtMiddleware { get; set; } = false;
    }
}