using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;
using System.Collections.Generic;
using System.Globalization;

namespace MS.WebCore.MultiLanguages
{
    public static class MultiLangExtensions
    {
        /// <summary>
        /// 支持的语言类型
        /// 此处内容要与真实的文件对应
        /// </summary>
        public static readonly List<string> supportLangs = new List<string>
        {
            "zh-Hans",
            "zh-Hant",
            "en"
        };

        /// <summary>
        /// 更改当前UI线程语言
        /// </summary>
        /// <param name="name"></param>
        public static void SetCurrentUICulture(string name)
        {
            CultureInfo.CurrentUICulture = new CultureInfo(name, false);
        }
        /// <summary>
        /// 获取指定语言的文字内容
        /// </summary>
        /// <param name="localizer"></param>
        /// <param name="specificLang"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string GetSpecificLanguageString(this IStringLocalizer localizer, string specificLang, string key)
        {
#pragma warning disable CS0618 // 类型或成员已过时
            return localizer.WithCulture(new CultureInfo(specificLang))[key].ToString();
#pragma warning restore CS0618 // 类型或成员已过时
        }


        /// <summary>
        /// 添加多语言本地化支持
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddMultiLanguages(this IServiceCollection services)
        {

            services.AddLocalization();

            services.AddSingleton<IStringLocalizer>((sp) =>
            {
                var sharedLocalizer = sp.GetRequiredService<IStringLocalizer<SharedResource>>();
                return sharedLocalizer;
            });
            /*
             //需要在startup中，AddControllers（webapi）的后面或者AddMVC（webmvc）注册
            .AddDataAnnotationsLocalization(options =>
                {
                    options.DataAnnotationLocalizerProvider = (type, factory) =>
                        factory.Create(typeof(SharedResource));//给注解添加本地化资源提供器Localizerprovider
                })
             
             */

            return services;
        }

        /// <summary>
        /// 使用多语言本地化中间件
        /// 默认语言在appsetting中设置
        /// </summary>
        /// <param name="app"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static IApplicationBuilder UseMultiLanguage(this IApplicationBuilder app, IConfiguration configuration)
        {
            List<CultureInfo> supportedCultures = new List<CultureInfo>();
            foreach (var item in supportLangs)
            {
                supportedCultures.Add(new CultureInfo(item));
            }
            return app.UseRequestLocalization(new RequestLocalizationOptions
            {
                DefaultRequestCulture = new RequestCulture(configuration.GetSection("SiteSetting:DefaultLanguage").Value),
                // Formatting numbers, dates, etc.
                SupportedCultures = supportedCultures,
                // UI strings that we have localized.
                SupportedUICultures = supportedCultures
            });
        }
    }
}
