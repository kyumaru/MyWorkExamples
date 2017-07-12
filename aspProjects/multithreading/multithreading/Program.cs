using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

/*use a multiple core CPU for executing multithreading programs*/
namespace multithreading
{
    class Program
    {
        static void Main(string[] args)
        {
            callBackDelegate d = callBackFunction;//sintax sugar for delegate instantiation

            var myREF=new MyCounter(10,d);
            
            Thread threadREF = new Thread(new ThreadStart(myREF.count));
            threadREF.Start();

            //threadREF.Join();//wait for the spawn thread to return
            //on 1 CPU notice how join is not necessary thanks to yiel return pattern
            var collectionREF = myREF.MyDick;

            if (threadREF.IsAlive)// a child thread will never be alive on 1 CPU available
            {
                Console.WriteLine("child thread is working");

            }

            Console.WriteLine("Press enter to get the next number");

            foreach (var i in collectionREF)
            {
                Console.WriteLine(i);              
            }

            if (!threadREF.IsAlive)
            {
                Console.WriteLine("child thread finished");

            }

            Console.WriteLine("main thread completed execution");

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


    class MyCounter
    {
        //reference to shared data between parent and child threads
        public IEnumerable<int> MyDick { get; set; }
        
        callBackDelegate _delegateREF;
        int _N;

        public MyCounter(int n, callBackDelegate d)
        {
            this._delegateREF = d;
            this._N = n;
        }

        public void count()
        {
            this.MyDick= _delegateREF(_N);
        }
    }
}
