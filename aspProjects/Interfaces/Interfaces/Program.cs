using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//multipe inheritance
//https://youtu.be/Huj3Jbz-NFw?list=PLAC325451207E3105&t=253

namespace Interfaces
{
    class Program
    {
        static void Main(string[] args)
        {
            /*MULTIPLE INHERITANCE*/
            //var myRef = new MyMultipleInheritance();
            //myRef.sayHello();
            //myRef.sayGoodBye();

            /*EXPLICIT INTERFACE IMPLEMENTATION*/
            //var myRef1 =new ExplicitInterfaceImplementation();
            //((IinterfaceA)myRef1).sayHi();
            //((IinterfaceB)myRef1).sayHi();

            /*using parent interface reference types to child classes*/
            //a reference acceses the members its type encapsulate, and only such members
            //it is important to note that even when interfaces or abstract classes
            //cannot get an instance, refereces to such elements can be created.
            //check for reference, value types http://www.codeproject.com/Articles/76153/Six-important-NET-concepts-Stack-heap-value-types

            // a value reference type like ImyInterface2 iRef2 can hold a reference to 
            //any class that implements ImyInterface2, thus accessing such class 
            //version for methods defined by interface ImyInterface2 
            ImyInterface2 iRef2 = new classInheritAll();
            iRef2.sayGoodBye();

            ImyInterface1 iRef1 = new classInheritAll();
            iRef1.sayHello();

            classInheritAll allRef = (classInheritAll)iRef1;
            allRef.sayHello();
            allRef.sayGoodBye();

            //since A, B are explicitly implemented
            ((IinterfaceA)allRef).sayHi();
            ((IinterfaceB)allRef).sayHi();

        }
    }

    /*
        MULTIPLE INHERITANCE
    */

    interface ImyInterface1
    {
        //interfaces cannot declare fields, delegates...etc
        void sayHello();
       
    }

    class MyClass1: ImyInterface1
    {
        //in an interface all methods are public by default
        public void sayHello()
        {
            Console.WriteLine("Hello buddy");
        }       
    }

    interface ImyInterface2
    {
        void sayGoodBye();
    }

    class MyClass2 : ImyInterface2
    {
        public void sayGoodBye()
        {
            Console.WriteLine("Bye buddy");
        }
    }

    class MyMultipleInheritance : ImyInterface1, ImyInterface2
    {
        MyClass1 myRef1 = new MyClass1();
        MyClass2 myRef2 = new MyClass2();

        public void sayGoodBye()
        {
            myRef2.sayGoodBye();
        }

        public void sayHello()
        {
            myRef1.sayHello();

        }
    }
    /*
        EXPLICIT INTERFACE IMPLEMENTATION
    */

    //usefull when 2 interfaces have the same signatures, we need a way to tell 
    //apart which method to call 
    interface IinterfaceA
    {
        void sayHi();
    }

    interface IinterfaceB
    {
        void sayHi();
    }

    class ExplicitInterfaceImplementation : IinterfaceA, IinterfaceB
    {
        void IinterfaceB.sayHi()
        {
            Console.WriteLine("Hi from B");
        }

        void IinterfaceA.sayHi()
        {
            Console.WriteLine("Hi from A");
        }
    }

    //child, derived, subclass
    class classInheritAll : IinterfaceA, IinterfaceB, ImyInterface1, ImyInterface2
    {
      
        MyClass1 ref1= new MyClass1();
        MyClass2 ref2 = new MyClass2();


        public void sayGoodBye()
        {
            ref2.sayGoodBye();
        }

        public void sayHello()
        {
            ref1.sayHello();
        }

        void IinterfaceB.sayHi()
        {
            Console.WriteLine("Hi from implemented interface B");
        }

        void IinterfaceA.sayHi()
        {
            Console.WriteLine("Hi from implemented interface A");
        }

        // this should be implemented as explicit1

    }
}
