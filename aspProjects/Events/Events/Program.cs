using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//https://www.youtube.com/watch?v=NgrnCMaxIXM&index=14&list=PLAE7FECFFFCBE1A54
namespace Events
{
    
    class CharacterManager
    {
        //a delegate is a reference type that encapsulates a list of references to functions. 
        public delegate void CharaterActionHandler();// the delegate events will reference to

        //an event is a value type that references a delegate
        //an event imposes 2 restrictions over how the delegate can be invoked
        //1-cannot register methods to it externally
        //2-cannot invoke it externally
        //1 and 2 must be done through the reference, the event
         
        public event CharaterActionHandler myHandlerREF;

        public void startGame()
        {
            if (myHandlerREF!=null)//good practice, invoke only if there is something registered against the delegate 
            {
                myHandlerREF.Invoke();//invoke the deleate, sugar: myHandlerREF();
            }
        }


    }

    class Program
    {
       
        static void Main(string[] args)
        {
            CharacterManager c = new CharacterManager();

            Wizard wizard= new Wizard(c);
            Fighter fighter = new Fighter(c);

            c.startGame();    

        }       
    }



    class Wizard
    {

        public Wizard(CharacterManager c)
        {
            //register methods on delegate through event
            if (new Random().Next() % 3 == 0)
                c.myHandlerREF += run;
            else
                c.myHandlerREF += cast;
        }

        void cast()
        {
            Console.WriteLine("The wizard is casting");
        }

        void run()
        {
            Console.WriteLine("The wizard flees");

        }

    }


    class Fighter
    {

        public Fighter(CharacterManager c)
        {
            //register methods on delegate through event
            if (new Random().Next() % 2 == 0)
                c.myHandlerREF += attack;
            else
                c.myHandlerREF += Charge;
        }

        void attack()
        {
            Console.WriteLine("The fighter is attacking");
        }

        void Defend()
        {
            Console.WriteLine("The fighter is Defending");

        }

        void Charge()
        {
            Console.WriteLine("The fighter is charging");

        }

    }
}
