using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
    notice how convinient this is for developing,
    we just need to write TestClass3 belong to 
    insideFileNameSpaceA on any project file
*/
namespace nameSpaceA
{
    class TestClass3
    {
        //lets make the method static so its an object of the class, not of one of its instances
        public static void sayHello()
        {
            Console.WriteLine("Test3 says hello");
        }
    }
    
}
