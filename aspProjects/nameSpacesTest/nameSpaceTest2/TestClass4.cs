using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


/*
    another part of nameSpaceA is written in this 
    class library project, upon compiling, a separate assembly(.exe or .dll)
    gets created for this project

    Remember when creating a WCF service it is done withing a class library,
    and then on the client of such service, a refference must be added to it
    Notice the similarity 
*/

namespace nameSpaceA
{
    public class TestClass4
    {
        //lets make the method static so its an object of the class, not of one of its instances
        public static void sayHello()
        {
            Console.WriteLine("Test4 says hello");
        }
    }
}
