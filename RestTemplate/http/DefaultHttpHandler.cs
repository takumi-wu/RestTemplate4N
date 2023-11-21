using Newtonsoft.Json;
using RestTemplate.attribute;
using System;
using System.Collections.Generic;
using System.Linq;
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

        protected override void wrapperRequestContent(object[] args, RequestWrapper requestWrapper)
        {
            
            foreach (object item in args)
            {
                HttpRequestParamAttribute getParam = (HttpRequestParamAttribute)item.GetType().GetCustomAttribute(typeof(HttpRequestParamAttribute), false);
                // 设置get请求参数
                if (getParam != null)
                {
                    if (item.GetType().IsValueType)
                    {
                        requestWrapper.httpRequestGetParam.Add(getParam.ParamName, Convert.ToString(item));
                    }
                    else
                    {
                        requestWrapper.httpRequestGetParam.Add(getParam.ParamName, JsonConvert.SerializeObject(item));
                    }
                }
                // 设置httpHeader 参数
                HttpHeaderAttribute requestHeader = (HttpHeaderAttribute)item.GetType().GetCustomAttribute(typeof(HttpHeaderAttribute), false);
                if (requestHeader != null)
                {
                    if (item.GetType().IsValueType)
                    {
                        requestWrapper.httpRequestHeader.Add(getParam.ParamName, Convert.ToString(item));
                    }
                    else
                    {
                        requestWrapper.httpRequestHeader.Add(getParam.ParamName, JsonConvert.SerializeObject(item));
                    }
                }
                // 设置请求体
                HttpRequestBodyAttribute requestBody = (HttpRequestBodyAttribute)item.GetType().GetCustomAttribute(typeof(HttpRequestBodyAttribute), false);
                if (requestBody != null)
                {
                    requestWrapper.httpRequestContent = new StringContent(JsonConvert.SerializeObject(item), Encoding.UTF8, "application/json");
                }
            }
        }
    }
}
