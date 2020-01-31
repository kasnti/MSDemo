using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Hosting;
using MS.WebApi;

namespace WebApiTests
{
    public static class TestHostBuild
    {
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
    }
}