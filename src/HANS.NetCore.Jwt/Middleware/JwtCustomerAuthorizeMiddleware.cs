using HANS.NetCore.Jwt.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HANS.NetCore.Jwt.Middleware
{
    public class JwtCustomerAuthorizeMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly JsonWebTokenSetting _setting;
        private readonly Func<Dictionary<string, string>, JsonWebTokenSetting, bool> _validatePayLoad;
        private readonly List<string> _anonymousPathList;
        private readonly IJsonWebTokenValidate _jsonWebTokenValidate;

        public JwtCustomerAuthorizeMiddleware(RequestDelegate next, IOptions<JsonWebTokenSetting> options, IJsonWebTokenValidate jsonWebTokenValidate, Func<Dictionary<string, string>, JsonWebTokenSetting, bool> validatePayLoad, List<string> anonymousPathList)
        {
            this._next = next;
            this._setting = options.Value;
            this._jsonWebTokenValidate = jsonWebTokenValidate;
            this._validatePayLoad = validatePayLoad;
            this._anonymousPathList = anonymousPathList;
        }

        public async Task Invoke(HttpContext context)
        {
            //JsonWebTokenValidate
            //若是路径可以匿名访问，直接跳过
            if (_anonymousPathList.Contains(context.Request.Path.Value))
            {
                //还未验证
                await _next(context);
                return;
            }
            var result = context.Request.Headers.TryGetValue("Authorization", out StringValues authStr);
            if (!result || string.IsNullOrEmpty(authStr.ToString()))
            {
                throw new UnauthorizedAccessException("未授权，请传递Header头的Authorization参数。");
            }

            //进行验证与自定义验证
            result = _jsonWebTokenValidate.Validate(authStr.ToString().Substring("Bearer ".Length).Trim()
                    , _setting, _validatePayLoad);
            if (!result)
            {
                throw new UnauthorizedAccessException("验证失败，请查看传递的参数是否正确或是否有权限访问该地址。");
            }

            await _next(context);
        }
    }
}