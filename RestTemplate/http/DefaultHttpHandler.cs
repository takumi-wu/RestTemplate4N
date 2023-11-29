using Newtonsoft.Json;
using RestTemplate.attribute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace RestTemplate.http
{
    public class DefaultHttpHandler : BaseHttpHandler
    {
        protected override async Task<string> SendHttp(RequestWrapper requestWrapper)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    HttpResponseMessage response = null;
                    HttpRequestMessage request = new HttpRequestMessage(requestWrapper.httpMethod, requestWrapper.RequestUrl);

                    // 设置HTTP Header 
                    if (requestWrapper.httpRequestHeader != null && requestWrapper.httpRequestHeader.Count > 0)
                    {
                        foreach (var item in requestWrapper.httpRequestHeader)
                        {
                            request.Headers.Add(item.Key, item.Value);
                        }
                    }

                    request.Content = requestWrapper.httpRequestContent;
                    ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
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

        protected override void wrapperRequestContent(Dictionary<ParameterInfo, object> args, RequestWrapper requestWrapper)
        {

            foreach (ParameterInfo key in args.Keys)
            {
                HttpRequestParamAttribute getParam = (HttpRequestParamAttribute)key.GetCustomAttribute(typeof(HttpRequestParamAttribute), false);
                // 设置get请求参数
                if (getParam != null)
                {
                    if (args[key].GetType().Name.ToLower() == "string" || args[key].GetType().IsValueType)
                    {
                        requestWrapper.httpRequestGetParam.Add(getParam.ParamName, Convert.ToString(args[key]));
                    }
                    else
                    {
                        requestWrapper.httpRequestGetParam.Add(getParam.ParamName, JsonConvert.SerializeObject(args[key]));
                    }
                }
                // 设置httpHeader 参数
                HttpHeaderAttribute requestHeader = (HttpHeaderAttribute)key.GetCustomAttribute(typeof(HttpHeaderAttribute), false);
                if (requestHeader != null)
                {
                    if (args[key].GetType().Name.ToLower() == "string" || args[key].GetType().IsValueType)
                    {
                        requestWrapper.httpRequestHeader.Add(requestHeader.ParamName, Convert.ToString(args[key]));
                    }
                    else
                    {
                        requestWrapper.httpRequestHeader.Add(requestHeader.ParamName, JsonConvert.SerializeObject(args[key]));
                    }
                }
                // 设置请求体
                HttpRequestBodyAttribute requestBody = (HttpRequestBodyAttribute)key.GetCustomAttribute(typeof(HttpRequestBodyAttribute), false);
                if (requestBody != null)
                {
                    requestWrapper.httpRequestContent = new StringContent(JsonConvert.SerializeObject(args[key]), Encoding.UTF8, "application/json");
                }
            }
        }
    }
}
