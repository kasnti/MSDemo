using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MS.DbContexts;
using MS.UnitOfWork;
using MS.WebCore.Logger;
using NLog;
using System;

namespace MS.WebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Logger logger = LogManager.GetCurrentClassLogger();
            try
            {
                var host = CreateHostBuilder(args).Build();
                logger.Trace("网站启动中...");
                using (IServiceScope scope = host.Services.CreateScope())
                {
                    logger.Trace("初始化NLog");
                    //确保NLog.config中连接字符串与appsettings.json中同步
                    NLogExtensions.EnsureNlogConfig("NLog.config", "MySQL", scope.ServiceProvider.GetRequiredService<IConfiguration>().GetSection("ConectionStrings:MSDbContext").Value);

                    logger.Trace("初始化数据库");
                    //初始化数据库
                    DBSeed.Initialize(scope.ServiceProvider.GetRequiredService<IUnitOfWork<MSDbContext>>());

                    //for test -start
                    //用于查看彩色控制台样式，以及日志等级过滤
                    //logger.Trace("Test For Trace");
                    //logger.Debug("Test For Debug");
                    //logger.Info("Test For Info");
                    //logger.Warn("Test For Warn");
                    //logger.Error("Test For Error");
                    //logger.Fatal("Test For Fatal");
                    //for test -end
                }
                logger.Trace("网站启动完成");
                host.Run();
            }
            catch (Exception ex)
            {
                logger.Fatal(ex, "网站启动失败");
                throw;
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
            .UseServiceProviderFactory(new AutofacServiceProviderFactory())//替换autofac作为DI容器
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseStartup<Startup>();
            })
            .AddNlogService();
    }
}
