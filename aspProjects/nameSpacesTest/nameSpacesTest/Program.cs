using System;

using nameSpaceA;//this is the external part of it
using ALIAS = nameSpacesTest1.nameSpaceA;

/* to refresh namespaces, definetely check
    https://www.youtube.com/watch?v=IQTbvVemMAg
    //check for assemblies
    http://stackoverflow.com/questions/2972732/what-are-net-assemblies
*/

// namespace a way to organize the code and prevent naming clashes 
/*
    .Net frameworks are organized as class libraries with namespaces
    which contain the actual code resources, so the namespaces kinda
    define a virtual path for them
    There is no correspondace between namespaces and files, classes assemblies or 
    directories, they are a c# programming element
*/

//notice how a project createad a new namespace for it
namespace nameSpacesTest1
{
    class Program
    {
        static void Main(string[] args)
        {
            //since sayHello is static no instance is needed.
            ALIAS.TestClass1.sayHello();
            nameSpaceA.TestClass2.sayHello();

            /*
                this one was created to show how a namespace
                can be written over multiple files
            */
            TestClass3.sayHello();

            /*
                TestClass4 is a resource on another project
                within this solution so a reference had to
                be added for it
            */
            TestClass4.sayHello();

            nameSpaceB.TestClass1.sayHello();
        
        }
    }

    namespace nameSpaceA
    {
        class TestClass1
        {
            //lets make the method static so its an object of the class, not of one of its instances
            public static void sayHello()
            {
                Console.WriteLine("Test1 in namespaceA says hello");
            }
        }
    }

    //namespaces can be written amongs multiple files/assemblies
    namespace nameSpaceA
    {
        class TestClass2
        {
            public static void sayHello()
            {
                Console.WriteLine("Test2 says hello");
            }
        }
    }

    namespace nameSpaceB
    {
        class TestClass1
        {
            public static void sayHello()
            {
                Console.WriteLine("Test1 in namespaceB says hello");
            }
        }
    }
}
