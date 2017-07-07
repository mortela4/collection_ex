using System;
using System.Collections;


namespace ex1
{
    public class SamplesArrayList
    {

        public static void Main()
        {

            // Creates and initializes a new ArrayList.
            ArrayList objList = new ArrayList();
            // ArrayList{} holder *heterogene data* - kan m.a.o. lagre data av forskjellig type der!
            objList.Add("Hello");
            objList.Add(3);
            objList.Add("rd");
            objList.Add("world!");

            // Displays the properties and values of the ArrayList.
            Console.WriteLine("objList");
            Console.WriteLine("    Count:    {0}", objList.Count);
            Console.WriteLine("    Capacity: {0}", objList.Capacity);
            Console.Write("    Values:");
            PrintValues(objList);
        }


        public static void PrintValues(IEnumerable myList)
        {
            foreach (Object obj in myList)
                Console.Write("   {0}", obj);
            Console.WriteLine();
        }

    }

}
