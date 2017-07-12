using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Exepcion_Handling
{
    class Program
    {
        static void Main(string[] args)
        {
            /*EXCEPTION*/
            //DEF: an object created on runtime when something goes wrong
            //this object can be handled
            //keywords: try,catch,finally, throw Exception 

            //readFile1();
            //readFile2();
            //readFile3();

            foo(0, 1, 2, 3, 4, 5);

        }

        static void foo(int x,params int[] args)
        {
            foreach (var item in args)
            {
                try
                {
                    var wdiv = item / x;

                }
                catch (Exception e)
                {
                   //e = new Exception(e.Message + x.ToString(), e).InnerException;
                    Console.WriteLine(e.Message);
                    Console.WriteLine(e.StackTrace);
                }
            }
        }

        static void readFile1()
        {
            //the streamReader object is vulnerable to an exception if the file cannot be found
            StreamReader myRef = new StreamReader(@"C:\Users\tRasHcAn\Desktop\Exepcion_Handling\Exepcion_Handling\test.txt");
            Console.WriteLine(myRef.ReadToEnd());
            myRef.Close();//release all resources 

        }

        static void readFile2()
        {
            //the streamReader object is vulnerable to an exception if the file cannot be found
            StreamReader myRef = new StreamReader(@"Wrong path C:\Users\tRasHcAn\Desktop\Exepcion_Handling\Exepcion_Handling\test.txt");
            Console.WriteLine(myRef.ReadToEnd());
            myRef.Close();//release all resources 

        }

        static void readFile3()
        {
            StreamReader myRef = null;
            try
            {
                //the streamReader object is vulnerable to an exception if the file cannot be found

                myRef = new StreamReader(@"Wrong path C:\Users\tRasHcAn\Desktop\Exepcion_Handling\Exepcion_Handling\test.txt");
                Console.WriteLine(myRef.ReadToEnd());
            }
            catch (Exception e)
            {
                //throw;
                //log any info anywhere
                Console.WriteLine(e.Message);
            }
            finally
            {
                if (myRef!=null)
                {
                    myRef.Close();//release all resources 
                }
            }


        }
    }
}
