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
        private IRestResponseMessageMappingHandler restResponseMessageMappingHandler;
        public RestProxy(Object target,BaseHttpHandler baseHttpHandler, Type baseType) : base(baseType)
        {
            this.target = target;
            this.baseHttpHandler = baseHttpHandler;
        }
        public override IMessage Invoke(IMessage msg)
        {
            IMethodCallMessage callMessage = (IMethodCallMessage)msg;
            MethodBase method = callMessage.MethodBase;
            //To 请求记录日志
            Object responseMessage = baseHttpHandler.SendHttpRequest(target, method, callMessage);
            // TO 请求后
            return new ReturnMessage(responseMessage, new object[0], 0, null, callMessage);
        }
    }
}
