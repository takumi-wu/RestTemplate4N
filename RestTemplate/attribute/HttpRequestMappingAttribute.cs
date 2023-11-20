using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace RestTemplate.attribute
{
    [AttributeUsage(AttributeTargets.Method)]
    public class HttpRequestMappingAttribute : Attribute
    {
        public string MethodName { get; set; }

        public  string HttpMethod{ get; set; }
        public HttpRequestMappingAttribute(string methodName, string httpMethod)
        {
            this.MethodName = methodName;
            this.HttpMethod = httpMethod;
        }
    }
}
