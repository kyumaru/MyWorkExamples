using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace delegates
{

    //why deletages?, to have fuctions as first class objects like in javaScript,
    //and are widely used in .NET
    delegate bool myDelegate(int n);
    delegate void anotherDelegate(int n);
    delegate int callBackDelegate(int n);

    delegate int multicastDelegate(out int n);// out makes n passed by reference,
    //value of n must be initialized before passing it back to the caller, 
    //the returned value is value type not reference, so is a way of returning a
    //value to the caller by parameter

    class Program
    {
        static void Main(string[] args)
        {
            //make myDelegate point to foo function
            //myDelegate d = foo;//sintaxis sugar, an instance of the delegate is created by compiler
            //new myClass().test(d,9); /*1*/

            // same as /*1*/ but with lambda expression sintaxis sugar
            //check this http://stackoverflow.com/questions/15036584/lambda-expression-for-multiple-parameters
            //the compiler creates a delegate(because the function called -test- receives a delegate parameter)
            //and a function in the background
            //new myClass().test( n => (n >= 10), 11);

            /*CALLBACKS*/
            /*
            // method A calls method B, method B returns data to A during runtime
            //new myClass().testComunication(callBack);// more sintaxis sugar, an instance of the delegate will be created witha referece to callBack
            var myCollection = new myClass().testCallback(n=>n);

            //this foreach is part of the foreach-collection-yield return technique, lost of sugar
            //https://www.youtube.com/watch?v=4fju3xcm21M
            //basically this technique implements an ondemand loop as the values are requested
            //on the compiler creates a code where the controll moves from the caller to the called method
            foreach (var i in myCollection) { 
                Console.WriteLine(i);
            }
           */

            /*MULTICAST DELEGATES*/
            // a delegate can contain references to multiple methods, upon calling
            //such delegate, all methods will get invoked. If the signature returns
            //a type, only the value returned by the las method registered is returned,
            //if an out parameter is present, the return value will be assigned the same value as this out parameter
            //instantiate a delegate, use the overload operator += to register more methods to it
            //since delegate is a class it keeps a property invokationList where all
            //registerd delegates are stored
            //out and ref are used in pairs for every parameter in the caller and callee methods

            //multicastDelegate multiDelegate = chain1;
            //multiDelegate += chain2; //register method chain2
            //multiDelegate += chain3; // this is the last one registered, return value will get returned over out parameter

            ////int n= multiDelegate(out n);
            //int n;
            //Console.WriteLine(multiDelegate(out n));
            //Console.WriteLine(n); 

            /*FUNC and ACTION*/
            //DEF: classes to create genertic <T> delegates, same as lambda expressions with another sintax 
            // FUNC recieves <T1> parameters and returns a <T2> value, ACTION always returns void 
            //WHY: delegates with generics.

            Func<int, int, string> myFunc = (int x, int y  )=>{ return (x + y).ToString(); };
            Console.WriteLine(myFunc(5, 2)); 

            Action<int, string> myAction = (int x, string s)=> { Console.WriteLine(s + x.ToString()); };
            myAction(7 ,"the result is: ");

            Action<int> myAction1 = goo;
            myAction1(6);     
           

        }

        static public void goo(int n)
        {
            Console.WriteLine(n);
        }

        static public bool foo(int n)
        {
            return n >= 10;
        }

        /*MULTICAST DELEGATES*/

        static int chain1(out int n)
        {
            return n=1;
        }

        static int chain2(out int n)
        {
            return n=2;
        }

        static int chain3(out int n)
        {
            return n = 3;
        }

    }
    //DEFINITION
    //a delegate is a class, so is a reference type, it encapsulates a reference
    //to a function, upon declaration, the delegate´s signature types must match function´s
    //when a delegate is instantiated, a function name can be pass onto the constructor
    //the delegate then is pointing to such function foo, foo then, can be called
    //through the delegate. The delegate can get passed as argument to another function
    //goo. A deleagate type is defined by its name upon declaration
    //WHY IS NEEDED
    //Reducing hardcoding of a class: encapsule some class logic into a function and
    //implement the class to receive it as pararameter.
    //Callbacks: a delegate is meant to provide comunication between two parties
    //create a method in a class that receives a delegate as parameter, /*2*/
    //on another class call the method in /*2*/

    class myClass
    {

        public void testComunication(anotherDelegate d)
        {
            for (int i = 0; i < 1000000; ++i)
            {
                d(i);//do a callback
            }
        }

        public IEnumerable<int> testCallback(callBackDelegate d)
        {
            for (int i=0; i< 1000000; ++i)
            {
               
                yield return d(i);//do a callback
            }
        }

        public void test(myDelegate d, int n)
        {
            if (d(n))
            {
                Console.WriteLine(" the passed number is = greater than 10");
            }
            else
            {
                Console.WriteLine(" the passed number is lower than 10");

            }
        }
    }  
}
