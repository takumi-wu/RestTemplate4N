using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace RestTemplate.http
{
    public class RequestWrapper
    {
        /// <summary>
        /// 请求的url
        /// </summary>
        public string RequestUrl { get; set; }

        /// <summary>
        /// 请求的编码
        /// </summary>
        public string RequestEncoding { get; set; }

        /// <summary>
        /// 请求的类型
        /// </summary>
        public string MediaType { get; set; }

        /// <summary>
        /// get post put delete 请求方式
        /// </summary>
        public HttpMethod httpMethod { get; set; }

        /// <summary>
        /// 请求内容
        /// </summary>
        public HttpContent httpRequestContent { get; set; }

        /// <summary>
        /// 方法响应返回的结果
        /// </summary>
        public Type returnType { get; set; }
    }
}
