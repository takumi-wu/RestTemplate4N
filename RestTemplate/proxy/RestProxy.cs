using Newtonsoft.Json;
using RestTemplate.attribute;
using RestTemplate.http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;
using System.Runtime.Remoting.Messaging;
using System.Runtime.Remoting.Proxies;
using System.Text;
using System.Threading.Tasks;

namespace RestTemplate.proxy
{
    public class RestProxy : RealProxy
    {
        private Object target;
        private BaseHttpHandler baseHttpHandler;
        public RestProxy(Object target,BaseHttpHandler baseHttpHandler, Type baseType) : base(baseType)
        {
            this.target = target;
            this.baseHttpHandler = baseHttpHandler;
        }
        public override IMessage Invoke(IMessage msg)
        {
            //RestTemplateAttribute attr = (RestTemplateAttribute)target.GetType().GetCustomAttribute(typeof(RestTemplateAttribute));
            //if (attr == null)
            //{
            //    throw new Exception("接口未标识RestTemplateAttribute");
            //}
            //string url = attr.requestUrl;
            IMethodCallMessage callMessage = (IMethodCallMessage)msg;
            MethodBase method = callMessage.MethodBase;
            //Attribute methodAttr = null;
            //bool postRequest = false;
            //methodAttr = method.GetCustomAttribute(typeof(HttpGetMethodAttribute));
            //if (methodAttr == null)
            //{
            //    postRequest = true;
            //    methodAttr = method.GetCustomAttribute(typeof(HttpPostMethodAttribute));
            //}
            //object[] args = callMessage.Args;
            //Console.WriteLine("动态代理调用前");
            //Object paramArg = args[0];
            //StringContent content = new StringContent(JsonConvert.SerializeObject(paramArg), Encoding.UTF8, "application/json");
            //Task<string> resultContent = Request(url, null, content, postRequest);
            //object returnValue = resultContent.Result;
            //Console.WriteLine(returnValue);
            //resultContent.Wait();
            //Console.WriteLine("动态代理调用后");
            Object responseMessage = baseHttpHandler.SendHttpRequest(target, method, callMessage);
            return new ReturnMessage(responseMessage, new object[0], 0, null, callMessage);
        }

        // TODO https的调用还有问题，http调用可以通了，但是里面的参数和请求头做的还不是很完善需要改动而且应该把http的请求变成接口因为后期有可能会接入一些新的请求组件
        public async Task<string> Request(string requestUrl,Dictionary<string,string> httpHeaders, HttpContent content, Boolean isPost)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    HttpResponseMessage response = null;
                    HttpRequestMessage request = new HttpRequestMessage(isPost?HttpMethod.Post:HttpMethod.Get, requestUrl);

                    // 设置HTTP Header
       
                    request.Content = content;
                    response = await client.SendAsync(request);
                    response.EnsureSuccessStatusCode(); // 确保响应状态码为成功的状态

                    return response.Content.ReadAsStringAsync().Result; // 读取响应内容
                   
                }
                catch (HttpRequestException e)
                {
                    throw new Exception($"HTTP请求失败: {e.Message}");
                }
            }
        }
    }
}
