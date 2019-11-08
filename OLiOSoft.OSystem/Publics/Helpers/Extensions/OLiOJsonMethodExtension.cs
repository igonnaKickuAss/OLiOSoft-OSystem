using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OLiOSoft.OSystem.Helpers.Extensions
{
    static public class OLiOJsonMethodExtension
    {
        static public object ToJson(this string Json)
        {
            return Json == null ? null : JsonConvert.DeserializeObject(Json);
        }

        static public string ToJson(this object obj)
        {
            //如果序列化反序列化对象带有日期格式的,下边方法可以解决显示问题
            var timeConverter = new IsoDateTimeConverter { DateTimeFormat = "yyyy-mm-dd hh:mm:ss" };
            return JsonConvert.SerializeObject(obj, timeConverter);
        }

        /// <summary>
        /// 对序列化反序列化对象的日期格式自定义
        /// </summary>
        /// <param name="obj">对象</param>
        /// <param name="dateTimeFormates">自定义</param>
        /// <returns></returns>
        static public string ToJson(this object obj, string dateTimeFormates)
        {
            var timeConverter = new IsoDateTimeConverter { DateTimeFormat = dateTimeFormates };
            return JsonConvert.SerializeObject(obj, timeConverter);
        }

        /// <summary>
        /// JSON格式转化为对应的T
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="Json">该Json格式应该为{"name":"value"}格式</param>
        /// <returns></returns>
        static public T ToObject<T>(this string Json)
        {
            return Json == null ? default(T) : JsonConvert.DeserializeObject<T>(Json);
        }

        /// <summary>
        /// JSON格式数组转化为对应的ListT
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="Json">该Json格式应该为[{"name":"value"},{"name":"value"}]格式</param>
        /// <returns></returns>
        static public List<T> ToList<T>(this string Json)
        {
            return Json == null ? null : JsonConvert.DeserializeObject<List<T>>(Json);
        }

        static public DataTable ToTable(this string Json)
        {
            return Json == null ? null : JsonConvert.DeserializeObject<DataTable>(Json);
        }

        static public JObject ToJObject(this string Json)
        {
            return Json == null ? JObject.Parse("{}") : JObject.Parse(Json.Replace(" ", ""));
        }

    }
}
