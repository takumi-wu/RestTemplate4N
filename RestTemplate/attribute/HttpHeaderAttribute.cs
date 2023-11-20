using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestTemplate.attribute
{
    [AttributeUsage(AttributeTargets.Parameter)]
    public class HttpHeaderAttribute:Attribute
    {
        public string ParamName { get; set; }
        public HttpHeaderAttribute(string paramName)
        {
            this.ParamName = paramName;
        }
    }
}
