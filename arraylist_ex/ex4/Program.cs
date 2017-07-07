using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ex4
{
    class Program
    {
        public static void Main()
        {
            var uut = new Repo();
            var tst1 = new TestClass(7);
            var tst2 = new TestClass(13);

            // ArrayList{} holder *heterogene data* - kan m.a.o. lagre data av forskjellig type der!
            uut.RegisterObject("Hello", "o1");
            uut.RegisterObject(3, "o2");
            uut.RegisterObject(tst1, "TestClass1");
            uut.RegisterObject(tst2, "TestClass2");

            var obj1 = uut.GetObject("TestClass1") as TestClass;    // TODO: exception-handling code here!
            Console.WriteLine($"Retrieving field from object 'TestClass1': _val = {obj1.GetVal()}");
            
            var obj2 = uut.GetObject("TestClass2") as TestClass;    // TODO: exception-handling code here!
            Console.WriteLine($"Retrieving field from object 'TestClass2': _val = {obj2.GetVal()}");

            uut.PrintValues();

            // Modify them values:
            obj1.SetVal(22);
            obj2.SetVal(33);

            // Re-print:
            Console.WriteLine($"Retrieving field from object 'TestClass1': _val = {obj1.GetVal()}");
            Console.WriteLine($"Retrieving field from object 'TestClass2': _val = {obj2.GetVal()}");

            // Retrieve SAME objects once more & test:
            var obj3 = uut.GetObject("TestClass1") as TestClass;    // TODO: exception-handling code here!
            Console.WriteLine($"Retrieving field from object 'TestClass1': _val = {obj3.GetVal()}");

            var obj4 = uut.GetObject("TestClass2") as TestClass;    // TODO: exception-handling code here!
            Console.WriteLine($"Retrieving field from object 'TestClass2': _val = {obj4.GetVal()}");

            if (obj1.Equals(obj3) && obj1.Equals(tst1))
                Console.WriteLine("Object 'obj1' and 'obj3' are BOTH identical to object 'tst1' :-)");

            if ( obj2.Equals(obj4) && obj2.Equals(tst2) )
                Console.WriteLine("Object 'obj2' and 'obj4' are BOTH identical to object 'tst2' :-)");

            // Now, modify original objects & see what gives:
            tst1.SetVal(74);
            tst2.SetVal(138);

            // Re-print for obj3&obj4:
            Console.WriteLine($"Retrieving field from object 'TestClass1': _val = {obj3.GetVal()}");
            Console.WriteLine($"Retrieving field from object 'TestClass2': _val = {obj4.GetVal()}");

            // Tests:
            uut.RegisterObject(tst1, "TestClass1-1");
            uut.RegisterObject("hmmm...", "o1");
            var chkNullObj = uut.GetObject("makkapoo");
            if (chkNullObj == null)
                Console.WriteLine("PASS: No object named 'makkapoo' found :-/");
            else
                Console.WriteLine("FAIL: Object named 'makkapoo' found ?");

            var chkTypeObj1 = uut.GetObjectCheckType("TestClass2", typeof(TestClass));
            if (chkTypeObj1 == null)
                Console.WriteLine("FAIL: should have found object of type 'TestClass' ... :-(");
            else
                Console.WriteLine("PASS: Object named 'TestClass2' of type 'TestClass' found! :-)");

            var chkTypeObj2 = uut.GetObjectCheckType("TestClass2", typeof(string));
            if (chkTypeObj2 == null)
                Console.WriteLine("PASS: did not find object of type 'string' named 'TestClass2'! :-/");
            else
                Console.WriteLine("FAIL: Object named 'TestClass2' of type 'TestClass' found - this should NOT happen ... :-(");
        }
    }

}
