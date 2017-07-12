using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Inheritance
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // new allocates heap memory space for the reference type and
            //returns a pointer to such object, here myChild1 is a 
            //object reference type( a value type in the stack) that contains
            //such pointer, other value types are int, boolean, structs  
            //var myChild1 = new childClass(101);
            //var myChild2 = new childClass();
            //var myChild3 = new childClass("hello", "there");

            //https://youtu.be/0FhagHE3nw8?list=PLAC325451207E3105&t=414
            //the defined hierarchy allows for having an object reference
            //of the base class type pointing to a child class object, this
            //is made possible thanks to both objects(base and child) being 
            //instantiated, and is the base to polymorphism
            /* 
             myBaseClass baseRef1 = new myBaseClass();

             //a reference acceses the members its type encapsulates
             myBaseClass baseRef2 =new childClass();

             baseRef1.printField(); //printField base class

             ((childClass)baseRef2).printField(); //prinField child class
            */
            //however an object reference of child type to point a base object is
            //a compile error, here only the base class would get instantiated 
            //childClass mcc = new myBaseClass()
            //this fails with a cast exeption
            //childClass childref2 = (childClass)baseref2;
            //REMEMBER, child references cannot point to base objects

            /*
                ABSTRACT CLASS INHERITANCE
            */
            //var myRef=new ChildFromAbstract();
            //Console.WriteLine(myRef.func());

            /*
                Polymorphism
            */

            //Polymorphism is a way to selectively call base or derived version of a
            //method through a baseClass reference at runtime
            //the base of polymorphism is having a parentclass reference to a 
            //childclass instance 
            //myBaseClass baseRef2 =new childClass();
            //virtual, new, override are polymorphism keywords
            //virtual means it can be overriden by a derived class
            //override forces execution of the member´s derived version 
            //new (hiding) forces execution of the member´s base version 

            myBaseClass polRef = new childClass();
            polRef.sayHello();//calls derivedClass version
            polRef.sayBye();//calls baseClass version

            //myBaseClass polRef1 = new childClass();
            //myBaseClass polRef2 = new childClass();

            ////example of use
            //var myPolList = new List<myBaseClass> { polRef, polRef1, polRef2 };

            //foreach (var element in myPolList)
            //{
            //    element.sayHello();
            //    element.sayBye();
            //}

        }
    }

    //parent, base, superclass
    class myBaseClass
    {
        protected string field1;

        public myBaseClass()
        {
            Console.WriteLine("this is the default constructor of base class");
        }

        //overload contructor
        public myBaseClass(string field1)
        {
            this.field1 = field1;
            Console.WriteLine("this is the overloaded constructor of the base class for child number: " + this.field1);
        }

        //overload contructor
        public myBaseClass(string field1, string field2)
        {
            Console.WriteLine("this is another overloaded constructor of the base class, parameters are " + field1 + " , " + field2);
        }

        public void printField()
        {
            myBaseClass baseRef2 = new childClass();
        }
        //for polymorphism
        public virtual void sayHello()
        {
            Console.WriteLine("Hello from base class");
        }

        //for polymorphism
        public virtual void sayBye()
        {
            Console.WriteLine("Good Bye from base class");
        }
    }

    //remember, before a child class(subClass) instanciates, the base class
    //will instaciate first, since both are created, the child class can use 
    // : base() to pass a parameter into the base class constructor, usefull
    //when th constructor is overloaded, base.aBaseMethod can be used inside a
    //class method to call base class methods  
    //child, derived, subclass
    class childClass : myBaseClass
    {
        new int field1;

        public childClass()
        {
            Console.WriteLine("this is default constructor of child class");

        }

        public childClass(int childNumber) : base(childNumber.ToString())
        {
            field1 = childNumber;

            Console.WriteLine("this is the overloaded constructor of child class");
        }

        public childClass(string s1, string s2) : base(s1, s2)
        {
            Console.WriteLine("this is another overloaded constructor of child class, passing 2 parameters to base constructor");
        }

        public new void printField()
        {
            Console.WriteLine("this is child class printField, value of field1 is: " + field1);
        }

        //for polymorphism
        public override void sayHello()
        {
            Console.WriteLine("Hello from derived class");
        }

        //for polymorphism
        public new void sayBye()
        {
            Console.WriteLine("Good Bye from child class");
        }

    }


    /*
        ABSTRACT CLASSES
    */

    /*abstract in a class def means:      
        it can only be used as a base class 
        cannot be instatiated, only the child instatiation takes place
        it may contain abstract methods
        an abstract methods can only be within a abstract class
        an abstract methods cannot provide a definition because
        anything abstract is meant to be defined in a derived(child) class
        if a class inherits from an abstract class it must define anything abstract in it 
    */
    abstract class MyAbstractClass
    {
        protected int a;
        public abstract int func();

        /*other members like delegates, and fields cannot be abstract*/
    }

    class ChildFromAbstract : MyAbstractClass
    {
        public ChildFromAbstract(int a = -1)
        {
            this.a = a;
        }

        public override int func()  //when defining the inherited methods overrde must be used
        {
            return this.a;
        }
    }

}
