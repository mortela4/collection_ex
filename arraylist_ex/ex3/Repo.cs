using System;
using System.Collections;
using System.Collections.Generic;


namespace ex3
{
    public class Repo
    {
        // Creates and initializes a new ArrayList.
        ArrayList objList = new ArrayList();
        int idx = 0;
        Dictionary<string, int> nameToIndex = new Dictionary<string, int>(); 

        private void AddObject(object obj)
        {
            objList.Add(obj);
            ++idx;
        }

        public void RegisterObject(object obj, string name)
        {
            objList.Add(obj);
            nameToIndex[name] = idx;
            ++idx;
        }

        public object GetObject(string name)
        {
            object obj = objList[nameToIndex[name]];
            Console.WriteLine($"Returned object no.{idx} named '{name}': {obj.ToString()}");

            return obj;
        }

        public void PrintValues()
        {
            foreach (var obj in objList)
                Console.Write("   {0}", obj);
            Console.WriteLine();
        }
    }
}
