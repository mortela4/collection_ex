using System;
using System.Collections;


namespace ex2
{
    public class Repo
    {
        // Creates and initializes a new ArrayList.
        ArrayList objList = new ArrayList();

        public void AddObject(object obj)
        {
            objList.Add(obj);
        }

        public void PrintValues()
        {
            foreach (var obj in objList)
                Console.Write("   {0}", obj);
            Console.WriteLine();
        }
    }
}
