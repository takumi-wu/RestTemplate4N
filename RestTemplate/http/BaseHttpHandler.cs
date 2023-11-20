﻿using RestTemplate.attribute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace RestTemplate.http
{
    public abstract class BaseHttpHandler
    {
        // 动态选择
        private IRestResponseMessageMappingHandler restResponseMessageMappingHandler ;

        public Object SendHttpRequest(Object target ,MethodBase method, IMethodCallMessage callMessage)
        {
            RequestWrapper requestWrapper = WrapperRequestParam(target, method, callMessage);
            HttpResponseMessage responseMessage = SendHttp(requestWrapper);
            return restResponseMessageMappingHandler.Mapping(requestWrapper, responseMessage);
        }

        /// <summary>
        /// 发送http请求
        /// </summary>
        /// <param name="requestWrapper"></param>
        /// <returns></returns>
        protected abstract HttpResponseMessage SendHttp(RequestWrapper requestWrapper);

        private RequestWrapper WrapperRequestParam(object target, MethodBase method, IMethodCallMessage callMessage)
        {
            RequestWrapper requestWrapper = new RequestWrapper();
            string url = "";
            HttpMethod httpMethod = new HttpMethod("POST");
            object[] args = callMessage.Args;
            RestTemplateAttribute attr = (RestTemplateAttribute)target.GetType().GetCustomAttribute(typeof(RestTemplateAttribute));
            if (attr == null)
            {
                throw new Exception("接口未标识RestTemplateAttribute");
            }
            // 找到最后一个分割符
            int lastSplitCharIndex = attr.requestUrl.LastIndexOf('/');
            // 截取最后一个/ 字符
            url = ((lastSplitCharIndex != 0) && (attr.requestUrl.Length - 1) == lastSplitCharIndex) ? attr.requestUrl.Substring(0, lastSplitCharIndex) : attr.requestUrl;
            // 处理方法上的路径
            Attribute methodAttr = null;
            methodAttr = method.GetCustomAttribute(typeof(HttpRequestMappingAttribute));
            Type returnType = ((MethodInfo)method).ReturnType;
            if (returnType != null && returnType ==typeof(void))
            {
                requestWrapper.returnType = returnType;
            }
            if (methodAttr == null)
            {
               string methodUrl = ((HttpRequestMappingAttribute)methodAttr).MethodName;
                if (methodUrl.Trim()!="" && methodUrl.Trim().IndexOf('/') == 0)
                {
                    url = url + methodUrl.Trim().Substring(1, methodUrl.Length - 1);
                    httpMethod = new HttpMethod(((HttpRequestMappingAttribute)methodAttr).HttpMethod);
                }
            }
            // 封装请求内容和请求头
            wrapperRequestContent(args, requestWrapper.httpRequestContent);

            return requestWrapper;
        }

        /// <summary>
        /// 根据方法参数设置请求内容和请求头
        /// </summary>
        /// <param name="args"></param>
        /// <param name="httpRequestContent"></param>
        protected abstract void wrapperRequestContent(object[] args, HttpContent httpRequestContent);
    }
}