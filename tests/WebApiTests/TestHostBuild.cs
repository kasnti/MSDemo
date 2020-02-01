using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using MS.Component.Jwt;
using MS.Component.Jwt.UserClaim;
using MS.WebApi;
using System.Net.Http;

namespace WebApiTests
{
    public static class TestHostBuild
    {
        public static readonly JwtService jwtService = new JwtService(Options.Create(new JwtSetting
        {
            Audience = "MS.Audience",
            Issuer = "MS.WebHost",
            LifeTime = 1440,
            SecurityKey = "MS.WebHost SecurityKey"//此处内容需和服务器appsettings.json中保持一致
        }));
        public static readonly UserData userData = new UserData
        {
            Account = "test",
            Email = "test@qq.com",
            Id = 1,
            Name = "测试用户",
            Phone = "123456789111",
            RoleDisplayName = "testuserRole",
            RoleName = "testuser"
        };//测试用户的数据，也可以改成真实的数据，看需求

        public static IHostBuilder GetTestHost()
        {
            //代码和网站Program中CreateHostBuilder代码很类似，去除了AddNlogService以免跑测试生成很多日志
            //如果网站并没有使用autofac替换原生DI容器，UseServiceProviderFactory这句话可以去除
            //关键是webBuilder中的UseTestServer，建立TestServer用于集成测试
            return new HostBuilder()
            .UseServiceProviderFactory(new AutofacServiceProviderFactory())//替换autofac作为DI容器
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder
                .UseTestServer()//关键时多了这一行建立TestServer
                .UseStartup<Startup>();
            });
        }
        /// <summary>
        /// 生成带token的httpclient
        /// </summary>
        /// <param name="host"></param>
        /// <returns></returns>
        public static HttpClient GetTestClientWithToken(this IHost host)
        {
            var client = host.GetTestClient();
            client.DefaultRequestHeaders.Add("Authorization", $"Bearer {GenerateToken()}");//把token加到Header中
            return client;
        }

        /// <summary>
        /// 生成jwt令牌
        /// </summary>
        /// <returns></returns>
        public static string GenerateToken()
        {
            return jwtService.BuildToken(jwtService.BuildClaims(userData));
        }
    }
}