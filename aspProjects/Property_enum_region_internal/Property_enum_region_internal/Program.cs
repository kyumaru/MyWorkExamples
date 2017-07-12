using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClassLibrary1; //remember to add a reference to this project manually

namespace Property_enum_region_internal
{
    class Program
    {
        static void Main(string[] args)
        {
            ///*PROPERTY*/
            //var myRef = new TestClass();
            ////myRef.Field1 = 7;
            //////myRef.Field1 = -1;
            ////Console.WriteLine(myRef.Field1 );// a property shows in intellisense  
            //myRef.enumProperty = CsharpTypes.delegates;
            //Console.WriteLine(myRef.enumProperty );// a property shows in intellisense  
            ////if you want the key`s value cast into its underlying type
            //Console.WriteLine((int)myRef.enumProperty);// a property shows in intellisense  

            /*INTERNAL*/
            //DEF: allows access to members withing this assembly only
            //WHY: there may be many separate assemblies referenced by this project

            ////cannot reach the internal method
            //TestClassA myRef1 = new TestClassA();

            //TestClassB myRef2 = new TestClassB();
            //myRef2.MyPublicProperty = 1;



        }
    }

    class TestClass
    {
        /*PROPERTY*/
        /*DEF*/
        //a property encapsulates the setter getter methods for a private field
        //it is a good practice to have all fields private
        //a property has the parameter value, which holds the passed in data
        //set assigns value to its private field
        /*WHY IS NEEDED*/
        //control over the assigned values for fields is needed, validation
        //can be implemented to check for valid values or ecxeption handling
        //automatic property: sintaxis sugar,use code snippet prop, it creates
        //a private field within the class and set get accessors for it

        private int field1;//suppose cannot be negative, a property for this shares same signature

        /*DEF*/
        /*REGION*/
        //a way to label your code, IDE will create a toogle button to the left
        /*WHY*/
        //further organization of code, usefull on very large classes, can focus on expanded regions at a time, collapse everything else
        #region properties

        public int autoProperty { get; set; }//automatic property, sintaxis sugar

        public CsharpTypes enumProperty { get; set; }//one of the enum constants

        public int Field1 //a property encapsulates set get accessor
        {
            set //set accessor encapsulates private field assignment with value 
            {
                if (value > 0)
                {
                    field1 = value;
                }
                else
                {
                    //an exception is instantiated but alas not handled
                    throw new Exception("value received by property cannot be negative");
                }               
            }

            get //encapsulates private field recovery
            {
                return field1;
            }
        }
        #endregion
    }

    /*ENUMS*/
    /*DEF*/
    //https://msdn.microsoft.com/en-us/library/sbbt4032.aspx 
    /*WHY*/
    //to increase readability and thus mantainability
    //is easier to map constants to semantic 

    // enum is plenty of sintaxis sugar, better not passing them around as method parameters for now
    //tried to pass the whole enum as argument, code failed
   // enum is value type,
    // enum keyword creates a new enum value type, the new type is known by its name
    public enum CsharpTypes// the enum value type encapsulates list of key-value pairs
    {
        classes=1,//there is an underlying type and default value for the list, int, 0 
        delegates,//since default is 1, this would be 2
        events,
        enums
    };
}
