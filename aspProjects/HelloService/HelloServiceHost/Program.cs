using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;//first add reference to this project

//to host a service, a reference to serviceModelAssembly
// and a reference to the hosted serverice must be added
//do it on References for HelloServiceHost project within this solution

namespace HelloServiceHost
{
    class Program
    {
        static void Main(string[] args)
        {
            using (ServiceHost host = new ServiceHost(typeof(HelloService.HelloService))) 
            {
                host.Open();
                Console.WriteLine("host started @ "+DateTime.Now.ToString());
                Console.ReadLine();
            }
        }
    }
}
