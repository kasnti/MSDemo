using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NLog;
using NLog.Config;
using NLog.Web;
using System.Linq;
using System.Xml.Linq;

namespace MS.WebCore.Logger
{
    public static class NLogExtensions
    {
        //优先级：Trace>Debug>Info>Warn>Error>Fatal

        const string _mssqlDbProvider = "Microsoft.Data.SqlClient.SqlConnection, Microsoft.Data.SqlClient";
        const string _mysqlDbProvider = "MySql.Data.MySqlClient.MySqlConnection, MySql.Data";

        /// <summary>
        /// 确保NLog配置文件sql连接字符串正确
        /// </summary>
        /// <param name="nlogPath"></param>
        /// <param name="dbType"></param>
        /// <param name="sqlConnectionStr"></param>
        public static void EnsureNlogConfig(string nlogPath, string dbType, string sqlConnectionStr)
        {
            XDocument xd = XDocument.Load(nlogPath);
            if (xd.Root.Elements().FirstOrDefault(a => a.Name.LocalName == "targets")
                is XElement targetsNode && targetsNode != null &&
                targetsNode.Elements().FirstOrDefault(a => a.Name.LocalName == "target" && a.Attribute("name").Value == "log_database")
                is XElement targetNode && targetNode != null)
            {
                if (!targetNode.Attribute("connectionString").Value.Equals(sqlConnectionStr))//连接字符串不一致则修改
                {
                    targetNode.Attribute("connectionString").Value = sqlConnectionStr;
                    //dbProvider的变动仅限mssql和mysql
                    if (dbType.ToLower() == "mysql")
                    {
                        targetNode.Attribute("dbProvider").Value = _mysqlDbProvider; //mysql 
                    }
                    else
                    {
                        targetNode.Attribute("dbProvider").Value = _mssqlDbProvider; //mssql
                    }
                    xd.Save(nlogPath);
                    //编辑后重新载入配置文件（不依靠NLog自己的autoReload，有延迟）
                    LogManager.Configuration = new XmlLoggingConfiguration(nlogPath);
                }
            }
        }

        /// <summary>
        /// 注入Nlog服务
        /// </summary>
        /// <param name="builder"></param>
        /// <returns></returns>
        public static IHostBuilder AddNlogService(this IHostBuilder builder)
        {
            return builder
                  .ConfigureLogging(logging =>
                  {
                      logging.ClearProviders();
                      logging.AddDebug();
                      logging.SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Trace);
                  })
                  .UseNLog()// 替换NLog作为日志管理
                  ;
        }
    }
}
