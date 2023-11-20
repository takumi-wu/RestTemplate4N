using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestTemplate.attribute
{
    [AttributeUsage(AttributeTargets.Interface)]
    public class RestTemplateAttribute:Attribute
    {
       public string requestUrl { get; set; }
       public  RestTemplateAttribute(String url)
        {
            this.requestUrl = url;
        }
    }
}
