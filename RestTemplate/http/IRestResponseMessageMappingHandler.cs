using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace RestTemplate.http
{
    public interface IRestResponseMessageMappingHandler
    {
        Object Mapping(RequestWrapper requestWrapper, string responseMessage);
    }
}
