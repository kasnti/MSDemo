using MS.Common.IDCode;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace MS.WebCore
{
    public static class WebCoreExtensions
    {
        public const string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

        /// <summary>
        /// 添加跨域策略，从appsetting中读取配置
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static IServiceCollection AddCorsPolicy(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddCors(options =>
            {
                options.AddPolicy(MyAllowSpecificOrigins,
                builder =>
                {
                    builder
                    .WithOrigins(configuration.GetSection("Startup:Cors:AllowOrigins").Value.Split(','))
                    .AllowAnyHeader()
                    .AllowAnyMethod();
                });
            });
            return services;
        }

        /// <summary>
        /// 注册WebCore服务，配置网站
        /// do other things
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static IServiceCollection AddWebCoreService(this IServiceCollection services, IConfiguration configuration)
        {
            //绑定appsetting中的SiteSetting
            services.Configure<SiteSetting>(configuration.GetSection(nameof(SiteSetting)));

            #region 单例化雪花算法
            string workIdStr = configuration.GetSection("SiteSetting:WorkerId").Value;
            string datacenterIdStr = configuration.GetSection("SiteSetting:DataCenterId").Value;
            long workId;
            long datacenterId;
            try
            {
                workId = long.Parse(workIdStr);
                datacenterId = long.Parse(datacenterIdStr);
            }
            catch (Exception)
            {
                throw;
            }
            IdWorker idWorker = new IdWorker(workId, datacenterId);
            services.AddSingleton(idWorker);

            #endregion
            return services;
        }
    }
}
