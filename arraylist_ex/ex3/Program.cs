using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ex3
{
    class Program
    {
        public static void Main()
        {
            var uut = new Repo();

            // ArrayList{} holder *heterogene data* - kan m.a.o. lagre data av forskjellig type der!
            uut.RegisterObject("Hello", "o1");
            uut.RegisterObject(3, "o2");
            uut.RegisterObject("rd", "o3");
            uut.RegisterObject("world!", "o4");

            var obj1 = uut.GetObject("o2");
            Console.WriteLine($"Retrieving object 'o2' = {obj1}");

            var obj2 = uut.GetObject("o3");
            Console.WriteLine($"Retrieving object 'o3' = {obj2}");

            uut.PrintValues();
        }
    }
}
