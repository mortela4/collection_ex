using System;
using System.Collections;

namespace ex2
{
    public class SamplesArrayList
    {

        public static void Main()
        {
            var uut = new Repo();

            // ArrayList{} holder *heterogene data* - kan m.a.o. lagre data av forskjellig type der!
            uut.AddObject("Hello");
            uut.AddObject(3);
            uut.AddObject("rd");
            uut.AddObject("world!");

            uut.PrintValues();
        }

    }
}

