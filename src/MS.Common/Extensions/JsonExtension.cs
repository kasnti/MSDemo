using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace MS.Common.Extensions
{
    public static class JsonExtension
    {
        public static JsonSerializerSettings jsonSetting = new JsonSerializerSettings
        {
            ReferenceLoopHandling = ReferenceLoopHandling.Ignore
        };
        /// <summary>
        /// 序列化对象，默认禁止循环引用
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static string ToJsonString(this object data)
        {
            return JsonConvert.SerializeObject(data, jsonSetting);
        }
        /// <summary>
        /// 序列化对象
        /// </summary>
        /// <param name="data"></param>
        /// <param name="timeConverter"></param>
        /// <returns></returns>
        public static string ToJsonString(this object data, IsoDateTimeConverter timeConverter)
        {
            return JsonConvert.SerializeObject(data, timeConverter);
        }

        /// <summary>
        /// 反序列化字符串
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data"></param>
        /// <returns></returns>
        public static T GetDeserializeObject<T>(this string data)
        {
            if (string.IsNullOrWhiteSpace(data)) return default;
            return JsonConvert.DeserializeObject<T>(data, jsonSetting);
        }

        /// <summary>
        /// 使用序列化和反序列化获得一次深拷贝
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data"></param>
        /// <returns></returns>
        public static T GetMemberwiseCopy<T>(this T data)
        {
            return data.ToJsonString().GetDeserializeObject<T>();
        }
    }
}
