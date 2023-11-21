using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace RestTemplate.http
{
    public class DefaultResponseMessageMappingHandler : IRestResponseMessageMappingHandler
    {
        public object Mapping(RequestWrapper requestWrapper, string responseMessage)
        {
            if (requestWrapper.returnType != null)
            {
                if (requestWrapper.returnType.IsValueType)
                {
                    ValueType instance = Activator.CreateInstance(requestWrapper.returnType) as ValueType;
                    FieldInfo fieldInfo = requestWrapper.returnType.GetField("m_value");
                    fieldInfo.SetValueDirect(__makeref(instance), responseMessage);
                    return instance;
                }
                else
                {
                    return JsonConvert.DeserializeObject(responseMessage, requestWrapper.returnType);
                }
            }
            else
            {
                return responseMessage;
            }
        }
    }
}
