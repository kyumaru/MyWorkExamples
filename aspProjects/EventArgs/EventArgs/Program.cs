using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*using EventArgs is a convention when handling events*/

namespace MyEventArgs
{
    class Program
    {
        static void Main(string[] args)
        {
            Wizard w1 = new Wizard();
            w1.ActWizardHandler += makeWizardRun;
            w1.ActWizardHandler += makeWizardCast;

            w1.ActWizardHandler += w1.makeWizardDefend;

            w1.myHandlerMethod();
           
        }

        static void makeWizardRun(object sender, EventArgs e)
        {
            Wizard w1 = sender as Wizard;
            w1.run();
        }

        static void makeWizardCast(object sender, EventArgs e)
        {
            Wizard w1 = sender as Wizard;
            w1.cast();
        }
    }

    class Wizard
    {
        //an event handler, this needs to get a method which handles it
        //when working with events, this the convention.
        //look at the metadata, it´s a delegate to a function with sender, eventArgs paramenters
        public event EventHandler ActWizardHandler;

        public void myHandlerMethod()
        {
            if (ActWizardHandler!=null)
            {
                ActWizardHandler.Invoke(this, EventArgs.Empty);
            }
        }

        public void cast()
        {
            Console.WriteLine("The wizard is casting");
        }

        public void run()
        {
            Console.WriteLine("The wizard flees");

        }

        public void makeWizardDefend(object sender, EventArgs e)
        {
            Console.WriteLine("The wizard defends");

        }

    }
   
}
