using RestTemplate.attribute;
using RestTemplate.http;
using RestTemplate.proxy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace RestTemplate
{
    public class RestContext
    {
        public static Dictionary<Type, Object> restTemplates = new Dictionary<Type, object>();

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="assembly"></param>
        public static void init()
        {
            Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();
            
            foreach(Assembly assembly in assemblies)
            {
                BaseHttpHandler baseHttpHandler = new DefaultHttpHandler();
                // 请求默认实现
                Type[] types = assembly.GetTypes();
                Type baseHttpHandlerType = types.AsParallel().Where(x => x.IsSubclassOf(typeof(BaseHttpHandler)) && x != typeof(DefaultHttpHandler)).FirstOrDefault();
                if (baseHttpHandlerType != null)
                {
                    baseHttpHandler = (BaseHttpHandler)Activator.CreateInstance(baseHttpHandlerType);
                }
                types.AsParallel().ForAll(x =>
                {
                    // 是接口并且有RestTemplateAttribute标识
                    if (x.IsInterface && x.GetCustomAttribute(typeof(RestTemplateAttribute)) != null)
                    {
                        Type t = RestInterfaceFactory.createType(x);
                        Object obj = Activator.CreateInstance(t);
                        RestProxy restProxy = new RestProxy(obj, baseHttpHandler, x);
                        Object transparentProxy = restProxy.GetTransparentProxy();
                        restTemplates.Add(x, transparentProxy);
                    }
                });
            }
        }

        /// <summary>
        /// 获得透明代理对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <returns></returns>
        public static T getProxy<T>()
        {
            T result;
            result = (T)restTemplates[typeof(T)];
            return result;

        }
    }
}
