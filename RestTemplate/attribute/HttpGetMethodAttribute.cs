using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestTemplate.attribute
{
    [AttributeUsage(AttributeTargets.Method)]
    public class HttpGetMethodAttribute : Attribute
    {
        public string MethodName { get; set; }
        public HttpGetMethodAttribute(string methodName)
        {
            this.MethodName = methodName;
        }
    }
}
