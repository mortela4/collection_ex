using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ex5
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

            if (obj2.Equals(obj4) && obj2.Equals(tst2))
                Console.WriteLine("Object 'obj2' and 'obj4' are BOTH identical to object 'tst2' :-)");

            // Now, modify original objects & see what gives:
            tst1.SetVal(74);
            tst2.SetVal(138);

            // Re-print for obj3&obj4:
            Console.WriteLine($"Retrieving field from object 'TestClass1': _val = {obj3.GetVal()}");
            Console.WriteLine($"Retrieving field from object 'TestClass2': _val = {obj4.GetVal()}");

            // Tests:
            // =======================================
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

            // Test extension methods - all returns sub-repo with a set of objects matching 'pattern':
            string pattern = "TestClass";

            var objs = uut.StartsWith(pattern);     // 'objs' is of type 'Repo'

            objs.PrintValues();

            objs = uut.EndsWith("2");

            objs.PrintValues();

            objs = uut.Contains("Class");

            objs.PrintValues();

            // Tests specifying TYPE:
            pattern = "Test";
            objs = uut.StartsWith(pattern, typeof(TestClass));    

            objs.PrintValues();

            objs = uut.EndsWith("2", typeof(int));  // NOTE: will cover all INT-types! Int16/32/64 ...

            objs.PrintValues();

            objs = uut.Contains("Class", typeof(TestClass));

            objs.PrintValues();

            objs = uut.OfType(typeof(TestClass));

            objs.PrintValues();

            objs = uut.OfType(typeof(string));

            objs.PrintValues();


            // Test chaining methods:
            // ----------------------
            Console.WriteLine("\r\nChaining-tests:\r\n=================\r\n");

            uut.RegisterObject("String no1", "str_obj1");
            uut.RegisterObject("String no2", "str2");
            uut.RegisterObject("Last string", "str_obj3");
            //
            uut.RegisterObject(777, "int_obj1");
            uut.RegisterObject(888, "int2");
            uut.RegisterObject(999, "int_obj3");

            // First test - both 'uut.OfType(...).Contains(...)' lines will NOT select BOTH int AND string:
            objs = uut.OfType(typeof(string)).Contains("obj");

            objs.PrintValues();

            objs = uut.OfType(typeof(int)).Contains("obj");     // '.Contains()' gets the returned (temporary) 'Repo'-object from '.OfType()'

            objs.PrintValues();

            // Second test - both 'uut.Contains(...).OfType(...)' lines will will NOT select BOTH int AND string:
            objs = uut.Contains("obj").OfType(typeof(string));

            objs.PrintValues();

            objs = uut.Contains("obj").OfType(typeof(int));

            objs.PrintValues();


            // Final test - no objects in list:

            objs = uut.Contains("makkapoo");

            objs.PrintValues();

            objs = uut.OfType(typeof(float));

            objs.PrintValues();
        }
    }

}
