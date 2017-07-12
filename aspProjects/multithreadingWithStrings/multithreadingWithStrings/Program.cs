using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

// a process in windows is a context of resources(cpu, ram , i/o) for execution, 
// a thread is linked to a process, it is the unit where the execution of code
//actually takes place

namespace multithreadingWithStrings
{
    class Program
    {
        static void Main(string[] args)
        {
            //NameHolder myRef = new NameHolder("my name is buddy");
            //NameHolder myRef2 = new NameHolder("my name is Anne");

            ////use Thread class to create a new thread onto the process,
            ////the purpose of a thread is to execute a method, so the constructor
            ////of the thread should receive a delegate to the method
            ////ThreadStart is such delegate

            /*PASSIN DATA FROM PARENT THREAD TO CHILD THREAD*/
            ////the best practice to pass values into such method is encapulating
            ////such values into a helper class which at the same thime encapsulates
            ////the thread linked to this new thread as shown below
            //Thread myThreadRef = new Thread(new ThreadStart(myRef.printName));
            //Thread myThreadRef2 = new Thread(new ThreadStart(myRef2.printName));

            //myThreadRef.Start();//you should start your threads
            //myThreadRef2.Start();//you should start your threads

            /*GETTING DATA FROM CHILD THREAD TO PARENT THREAD*/
            // the key is a callback, so a delegate is needed, again the best practice
            //is to encapsulate a reference to an instance of it as an instance field,
            //then a function in the helper class can use it to perform the callback

            callBackDelegate delegateREF = callBackFunction;//sintaxis sugar for method instantiation

            var myREF = new CounterToN(10, delegateREF);

            Thread threadREF = new Thread(new ThreadStart(myREF.count));
            threadREF.Start();

           
        }

        //call back function
        public static IEnumerable<int> callBackFunction(int n)
        {
            for (int i = 0; i < n; i++)
            {
                Console.Read();
        
                yield return i;

                Console.Read();

            }
        }
    }


    /*getting values from child thread by delegate*/
    public delegate IEnumerable<int> callBackDelegate(int n);
   

    class CounterToN
    {
        callBackDelegate _delegateREF;
        int _N;

        public CounterToN(int n, callBackDelegate d )
        {
            this._delegateREF = d;
            this._N = n;
        }

        public void count()
        {
            var myCollection = _delegateREF(this._N);

            Console.WriteLine("Press enter to get the next number");

            foreach (var i in myCollection)
            {
                Console.WriteLine(i);
            }

        }
    }


    /*Passing arguments to child thread*/
    //https://youtu.be/asSMDgkwIvw?list=PLAC325451207E3105
    class NameHolder
    {
        string _name; //this encapsulates the values passed into the new thread

        public NameHolder(string s)
        {
            this._name = s;
        }

        // ToString() is a nice example of inheritance through interfaces
        //lets override the object method ToString(), it is a virtual method, polymorphism
        public override string ToString()
        {
            //return base.ToString();
            return _name;
        }

        public void printName()
        {
            Console.WriteLine(this.ToString());
        }

    }
}
