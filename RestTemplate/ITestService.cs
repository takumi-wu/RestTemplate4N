﻿using Newtonsoft.Json.Linq;
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
    [RestTemplate(url: "http://std.sc.5mall.com/user/thirdparty_auth/")]
    public interface ITestService
    {
        [HttpRequestMapping(methodName: "login", httpMethod:"POST")]
        JObject Login([HttpRequestBody]string tp);
    }
}
