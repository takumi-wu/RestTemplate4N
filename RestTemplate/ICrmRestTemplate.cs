using RestTemplate.attribute;
using RestTemplate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace RestTemplate
{

    [Fegin(url: "https://api.xiaoshouyi.com")]
    public interface ICrmRestTemplate
    {
        [HttpRequestMapping(methodName: "/oauth2/token.action", httpMethod: "GET")]
        JObject Token([HttpRequestParam("grant_type")] string grant_type,
            [HttpRequestParam("client_id")] string client_id,
            [HttpRequestParam("client_secret")] string client_secret,
            [HttpRequestParam("redirect_uri")] string redirect_uri,
            [HttpRequestParam("username")] string username,
            [HttpRequestParam("password")] string password
            );

        [HttpRequestMapping(methodName: "/rest/data/v2/query", httpMethod: "GET")]
        JObject Query([HttpRequestParam("q")] string q, [HttpHeader("Authorization")] string Authorization);
    }

}
