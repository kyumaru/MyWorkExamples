using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary1
{
    public class TestClassA 
    {
        MyinternalClass internalClassRef= new MyinternalClass();

        internal int MyInternalProperty { get; set; }
    }

    internal class MyinternalClass
    {
        TestClassA testClassARef = new TestClassA();

        public MyinternalClass()
        {
            testClassARef.MyInternalProperty = 1;
        } 
    }

    public class TestClassB
    {
        public int MyPublicProperty { get; set; }
    } 

}
