using HANS.NetCore.Jwt.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Primitives;
using System;
using System.Threading.Tasks;

namespace HANS.NetCore.Jwt.Policy
{
    public class JwtAuthorizeHandler : AuthorizationHandler<JwtAuthorizeBaseRequiremente>
    {
        private readonly JsonWebTokenSetting _setting;
        private readonly IJsonWebTokenValidate _jsonWebTokenValidate;

        public JwtAuthorizeHandler(IOptions<JsonWebTokenSetting> setting, IJsonWebTokenValidate jsonWebTokenValidate)
        {
            this._setting = setting.Value;
            this._jsonWebTokenValidate = jsonWebTokenValidate;
        }

        /// <summary>
        /// 验证JWT
        /// </summary>
        /// <param name="context"></param>
        /// <param name="requirement"></param>
        /// <returns></returns>
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, JwtAuthorizeBaseRequiremente requirement)
        {
            var httpContext = (context.Resource as AuthorizationFilterContext).HttpContext;

            var result = httpContext.Request.Headers.TryGetValue("Authorization", out StringValues authStr);
            if (!result || string.IsNullOrEmpty(authStr.ToString()))
            {
                throw new UnauthorizedAccessException("未授权，请传递Header头的Authorization参数。");
            }
            result = result && _jsonWebTokenValidate.Validate(authStr.ToString().Substring("Bearer ".Length).Trim(), _setting, requirement.validatePayLoad);
            if (!result)
            {
                throw new UnauthorizedAccessException("验证失败，请查看传递的参数是否正确或是否有权限访问该地址。");
            }
            context.Succeed(requirement);
            return Task.CompletedTask;
        }
    }
}