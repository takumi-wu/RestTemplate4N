using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestTemplate.attribute
{
    [AttributeUsage(AttributeTargets.Parameter)]
    public class HttpRequestBodyAttribute: Attribute
    {
    }
}
