using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace insideFileNameSpaceA
{
    public class TestClass3
    {
        //lets make the method static so its an object of the class, not of one of its instances
        public static void sayHello()
        {
            Console.WriteLine("Test3 says hello");
        }
    }
}
