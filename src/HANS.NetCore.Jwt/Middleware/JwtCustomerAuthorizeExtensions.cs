using HANS.NetCore.Jwt.Interface;
using HANS.NetCore.Jwt.Interface.Middleware;
using HANS.NetCore.Jwt.Interface.Policy;
using HANS.NetCore.Jwt.Policy;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace HANS.NetCore.Jwt.Middleware
{
    public static class JwtCustomerAuthorizeExtensions
    {
        /// <summary>
        /// 使用Jwt中间件
        /// </summary>
        /// <param name="app"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        public static IApplicationBuilder UseJwtCustomerAuthorize(this IApplicationBuilder app, Action<IJwtCustomerAuthorezeOption> action)
        {
            var _JwtCustomerAuthorezeOption = app.ApplicationServices.GetService<IJwtCustomerAuthorezeOption>() as JwtCustomerAuthorezeOption; //new JwtCustomerAuthorezeOption();
            action(_JwtCustomerAuthorezeOption);
            return app.UseMiddleware<JwtCustomerAuthorizeMiddleware>(_JwtCustomerAuthorezeOption.validatePayLoad, _JwtCustomerAuthorezeOption.anonymousPath);
        }

        /// <summary>
        /// 注册jwt服务
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static IServiceCollection AddJwt(this IServiceCollection services, IConfiguration configuration)
        {
            var section = configuration.GetSection("JsonWebToken");
            if (!section.Exists())
            {
                throw new ArgumentNullException("请在\"appsettings.json\"配置\"JsonWebToken\"节点");
            }
            return services.Configure<JsonWebTokenSetting>(section)
                .AddSingleton<IJsonWebTokenBuilder, JsonWebTokenBuilder>()
                 .AddSingleton<IJsonWebTokenValidate, JsonWebTokenValidate>()
                 .AddSingleton<IJwtCustomerAuthorezeOption, JwtCustomerAuthorezeOption>()
                 .AddSingleton<IJwtAuthorizRequiremente, JwtAuthorizeBaseRequiremente>()
                 .AddSingleton<IAuthorizationHandler, JwtAuthorizeHandler>();
        }
    }
}