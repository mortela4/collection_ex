using System;
//
using ex5;


namespace ex7_demo
{
    class Helper
    {
        DemoClassA uut1 = new DemoClassA("A-1");
        DemoClassB uut2 = new DemoClassB("B-2");
        DemoClassC uut3 = new DemoClassC("C-3");

        // Test1:
        private void Test1()
        {
            uut1.UseOtherClassInstances("B-2", "C-3");
            uut2.UseOtherClassInstances("A-1", "C-3");
            uut3.UseOtherClassInstances("A-1", "B-2");
        }


        // Test2:
        private void Test2()
        {
            Globals.objRepo.DeregisterObject("A-1");    // OK
            Globals.objRepo.DeregisterObject("A-2");    // Fail...
            //
            uut1.UseOtherClassInstances("B-2", "C-3");
            uut2.UseOtherClassInstances("A-1", "C-3");
            uut3.UseOtherClassInstances("A-1", "B-2");
        }


        private void Test3Helper(object uutInstance)
        {
            var uut = uutInstance as DemoClassB;

            Console.WriteLine("\r\nTest Loop:\r\n************");

            // Test3 - create TEMPORARY objects & register - should DEREGISTER automatically:
            for (int i = 2; i < 5; i++)
            {
                string name = "A-" + i.ToString();

                Console.WriteLine($"Iteration {i}:\r\n=============");
                var obj = new DemoClassA(name);
                uut.UseOtherClassInstance(name);
                // On exit of this loop - objects made from 'obj' MAY all have been deleted by GarbageCollector - but often NOT ...
                // Therefore, we use EXPLICIT removal of references:
                obj.RemoveRef();
            }

            return;
        }

        public void Test3()
        {
            // Check if objects exists:
            uut3.UseOtherClassInstance("A-2");  // Should yield 'no hit' - repo.GetObject() returns NULL ...
            uut3.UseOtherClassInstance("A-3");  // Should yield 'no hit' - repo.GetObject() returns NULL ...
            uut3.UseOtherClassInstance("A-4");  // Should yield 'no hit' - repo.GetObject() returns NULL ...
        }
        
        public void RunAllTests()
        {
            Test1();
            Test2();
            Test3Helper(uut2);
            //Test3();

            Console.WriteLine("\r\n--------------\r\nRan all tests!\r\n-----------------");
        }
    }


    class Program
    {

        /// <summary>
        /// uut1, uut2, uut3 vet INGENTING *direkte* om hverandre -
        /// all utveksling av referanser skjer via 'Globals.objRepo' objektliste.
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            
            // Test3 - check object lifetimes:
            var helper = new Helper();

            helper.RunAllTests();
            // Force 'garbage collection' (BAD practice - but shown here for reference)
            GC.Collect();
            helper.Test3();
           
        }

    }

}
