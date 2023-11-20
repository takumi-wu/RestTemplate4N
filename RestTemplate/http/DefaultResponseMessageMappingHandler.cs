using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace RestTemplate.http
{
    public class DefaultResponseMessageMappingHandler : IRestResponseMessageMappingHandler
    {
        public object Mapping(RequestWrapper requestWrapper, HttpResponseMessage responseMessage)
        {
            throw new NotImplementedException();
        }
    }
}
