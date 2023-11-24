using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestTemplate.attribute
{
    [AttributeUsage(AttributeTargets.Interface)]
    public class FeginAttribute:Attribute
    {
       public string requestUrl { get; set; }
       public  FeginAttribute(String url)
        {
            this.requestUrl = url;
        }
    }
}
