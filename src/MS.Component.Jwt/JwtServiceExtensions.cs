using MS.Component.Jwt.UserClaim;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Text;

namespace MS.Component.Jwt
{
    public static class JwtServiceExtensions
    {
        public static IServiceCollection AddJwtService(this IServiceCollection services, IConfiguration configuration)
        {
            //绑定appsetting中的jwtsetting
            services.Configure<JwtSetting>(configuration.GetSection(nameof(JwtSetting)));

            //注册jwtservice
            services.AddSingleton<JwtService>();
            //注册IHttpContextAccessor
            services.AddScoped<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<IClaimsAccessor, ClaimsAccessor>();

            var jwtConfig = configuration.GetSection("JwtSetting");

            services
                .AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(o =>
                {
                    o.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtConfig["SecurityKey"])),

                        ValidateIssuer = true,
                        ValidIssuer = jwtConfig["Issuer"],

                        ValidateAudience = true,
                        ValidAudience = jwtConfig["Audience"],

                        //总的Token有效时间 = JwtRegisteredClaimNames.Exp + ClockSkew ；
                        RequireExpirationTime = true,
                        ValidateLifetime = true,// 是否验证Token有效期，使用当前时间与Token的Claims中的NotBefore和Expires对比.同时启用ClockSkew 
                        ClockSkew = TimeSpan.Zero //注意这是缓冲过期时间，总的有效时间等于这个时间加上jwt的过期时间，如果不配置，默认是5分钟

                    };
                });
            return services;
        }
    }
}
