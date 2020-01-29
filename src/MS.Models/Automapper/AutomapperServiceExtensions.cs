using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace MS.Models.Automapper
{
    public static class ModelExtensions
    {
        /// <summary>
        /// 注册automapper服务
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddAutomapperService(this IServiceCollection services)
        {
            //将AutoMapper映射配置所在的程序集名称注册
            services.AddAutoMapper(Assembly.Load(Assembly.GetExecutingAssembly().GetName().Name));
            return services;
        }
    }
}
