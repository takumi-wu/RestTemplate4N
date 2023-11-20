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

            User user = new User();
            user.username = "xxxx";
            user.password = "xxxx";
            user.loginStatus = 1;
            string response = testService.Login(user);
            Console.WriteLine(response);
            Console.Read();
        }
    }
}
