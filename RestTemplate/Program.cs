using RestTemplate.bo;
using RestTemplate.proxy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestTemplate
{
    class Program
    {
        static void Main(string[] args)
        {
            RestContext.init(typeof(Program).Assembly);

            ITestService testService = RestContext.getProxy<ITestService>();


            CpqResponse response = testService.Login("55f7bcba6bf23bd5979bf663d4058d8c1cd3c52a10b08c73d4ebb7067d4119cd");
            Console.WriteLine(response);
            Console.Read();
        }
    }
}
