using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ex5;


namespace ex6_utest
{
    [TestClass]
    public class RepoUnitTests
    {
        Repo uut = new Repo();

        const string expected2 = "String no2";
        const int expected3 = 111;
        const int expected4 = 77779999;
        //
        TestClass tst1 = new TestClass(7);
        TestClass tst2 = new TestClass(expected3);
        TestClass tst3 = new TestClass(expected4);

        /// <summary>
        /// NOTE:
        /// there are INTER-DEPENDENCIES between each of the TestMethods below.
        /// Ideally, they should all be self-contained -
        /// and able to be run in full isolation.
        /// However, they all depend on the 'uut' object (Repo class instance);
        /// meaning if one testmethod modifies it, it MAY cause another testmethod to fail -
        /// unless care has been taken to ensure modification in one testmethod will not cause 
        /// any problems in another testmethod.
        /// </summary>
        public RepoUnitTests()
        {
            // Test SETUP:
            uut.RegisterObject(tst1, "TestClass1");
            uut.RegisterObject(tst2, "TestClass2");
            uut.RegisterObject(tst3, "CheckClassNo3");
            // 
            uut.RegisterObject("Hello", "o1");
            uut.RegisterObject(3, "o2");
             //
            uut.RegisterObject("String no1", "str_obj1");
            uut.RegisterObject(expected2, "str2");
            uut.RegisterObject("Last string", "str_obj3");
            uut.RegisterObject("Rolls-Royce", "Classy");
            //
            uut.RegisterObject(777, "int_obj1");
            uut.RegisterObject(888, "int2");
            uut.RegisterObject(999, "int_obj3");
        }

        [TestMethod]
        public void CheckSimpleMatch()
        {
            var actual = uut.GetObject("str2");

            Assert.AreEqual(expected2, actual, $"Mismatch between string objects! expected={expected2} but actual={actual}");
        }

        [TestMethod]
        public void CheckMatchWithType()
        {
            var actual = uut.GetObjectCheckType("TestClass2", typeof(TestClass));

            Assert.AreSame(tst2, actual, $"Mismatch between string objects! expected={tst2} but actual={actual}");
        }

        [TestMethod]
        public void GetModifyGetThenVerify()
        {
            const int expected = 357;

            var obj2 = uut.GetObject("TestClass2") as TestClass;    // TODO: exception-handling code here!
            int actual = obj2.GetVal();

            Assert.AreEqual(expected3, actual, $"Mismatch between INT values! expected={expected3} but actual={actual}");

            uut.PrintValues();

            // Modify value:
            obj2.SetVal(expected);

            var obj2_modified = uut.GetObjectCheckType("TestClass2", typeof(TestClass)) as TestClass;

            actual = obj2_modified.GetVal();

            Assert.AreEqual(expected, actual, $"Mismatch between INT values! expected={expected} but actual={actual}");
        }

        [TestMethod]
        public void CheckSimpleNoMatch()
        {
            var actual = uut.GetObject("makkapoo");
            object expected = null;

            Assert.AreEqual(expected, actual, $"Mismatch between (presumed NULL) objects! expected={expected} but actual={actual}");
        }

        [TestMethod]
        public void CheckSimpleNoTypeMatch()
        {
            var actual = uut.GetObjectCheckType("TestClass2", typeof(string));
            object expected = null;

            Assert.AreEqual(expected, actual, $"Mismatch between (presumed NULL) objects! expected={expected} but actual={actual}");
        }

        [TestMethod]
        public void CheckStartsWithMatching()
        {
            var repo_obj = uut.StartsWith("Check") as Repo;
            var actual = repo_obj.GetObjectCheckType("CheckClassNo3", typeof(TestClass)) as TestClass;
            Assert.AreSame(tst3, actual, $"Mismatch between objects! expected={tst3} but actual={actual}");
        }

        [TestMethod]
        public void CheckEndsWithMatching()
        {
            var repo_obj = uut.EndsWith("No3") as Repo;
            var actual = repo_obj.GetObjectCheckType("CheckClassNo3", typeof(TestClass)) as TestClass;
            Assert.AreSame(tst3, actual, $"Mismatch between objects! expected={tst3} but actual={actual}");
        }

        [TestMethod]
        public void CheckContainsMatching()
        {
            var repo_obj = uut.Contains("Class") as Repo;
            var actual = repo_obj.GetObjectCheckType("CheckClassNo3", typeof(TestClass)) as TestClass;
            Assert.AreSame(tst3, actual, $"Mismatch between objects! expected={tst3} but actual={actual}");
        }

        [TestMethod]
        public void CheckOfTypeMatching()
        {
            var repo_obj = uut.OfType(typeof(TestClass)) as Repo;
            var actual = repo_obj.GetObjectCheckType("CheckClassNo3", typeof(TestClass)) as TestClass;
            Assert.AreSame(tst3, actual, $"Mismatch between objects! expected={tst3} but actual={actual}");
        }

        
        [TestMethod]
        public void CheckFilterMatching_1()
        {
            var repo_obj = uut.Contains("Class").OfType(typeof(TestClass)) as Repo;
            var actual = repo_obj.GetObjectCheckType("CheckClassNo3", typeof(TestClass)) as TestClass;
            Assert.AreSame(tst3, actual, $"Mismatch between objects! expected={tst3} but actual={actual}");
        }

        [TestMethod]
        public void CheckFilterMatching_2()
        {
            var repo_obj = uut.OfType(typeof(TestClass)).Contains("Class") as Repo;
            var actual = repo_obj.GetObjectCheckType("CheckClassNo3", typeof(TestClass)) as TestClass;
            Assert.AreSame(tst3, actual, $"Mismatch between objects! expected={tst3} but actual={actual}");
        }

        [TestMethod]
        public void CheckObjectRemoval_GetSame()
        {
            uut.DeregisterObject("CheckClassNo3");

            var actual = uut.GetObject("CheckClassNo3");
            object expected = null;

            Assert.AreEqual(expected, actual, $"Mismatch between (presumed NULL) objects! expected={expected} but actual={actual}");
        }

        [TestMethod]
        public void CheckObjectRemoval_GetOther()
        {
            uut.DeregisterObject("int_obj1");

            var actual = uut.GetObject("int_obj3");
            object expected = 999;

            Assert.AreEqual(expected, actual, $"Mismatch between (presumed NULL) objects! expected={expected} but actual={actual}");
        }

    }
}
