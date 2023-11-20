using RestTemplate.attribute;
using RestTemplate.bo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace RestTemplate
{
    [RestTemplate(url: "http://xxxx")]
    public interface ITestService
    {
        [HttpRequestMapping(methodName: "login", httpMethod:"POST")]
        string Login([HttpRequestBody]User user);
    }
}
