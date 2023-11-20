using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace RestTemplate.http
{
    public class DefaultHttpHandler : BaseHttpHandler
    {
        protected override HttpResponseMessage SendHttp(RequestWrapper requestWrapper)
        {
            throw new NotImplementedException();
        }

        protected override void wrapperRequestContent(object[] args, HttpContent httpRequestContent)
        {
            throw new NotImplementedException();
        }
    }
}
