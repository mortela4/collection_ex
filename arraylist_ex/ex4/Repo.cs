using System;
using System.Collections;
using System.Collections.Generic;


namespace ex4
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
            // Add protection against adding obj w. same name!
            if ( nameToIndex.ContainsKey(name) )
            {
                Console.WriteLine("Attempt to add object with same name as existing object in list!");
                return;
                //throw new ArgumentException("Attempted to add same object twice!", nameof(obj));
            }

            // Protection against adding SAME object twice:
            if ( objList.Contains(obj) )
            {
                Console.WriteLine("Attempt to add same object twice!");
                return;
                //throw new ArgumentException("Attempted to add same object twice!", nameof(obj));
            }

            // Checks=OK --> add object to list & update dictionary:
            objList.Add(obj);
            nameToIndex[name] = idx;
            ++idx;
        }

        /// <summary>
        /// Get object from object list by name.
        /// No checking on object type.
        /// </summary>
        /// <param name="name"></param>
        /// <returns>object</returns>
        public object GetObject(string name)
        {
            // Handle missing object:
            if ( nameToIndex.ContainsKey(name) == false )
            {
                Console.WriteLine($"No such object named {name} exists - returning NULL!");

                return null;
            }

            object obj = objList[nameToIndex[name]];
            Console.WriteLine($"Returned object no.{idx} named '{name}': {obj.ToString()}");

            return obj;
        }

        /// <summary>
        /// Get object from object list by name.
        /// Verifies object type equals requested object type as specified in 'type' arg.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="type"></param>
        /// <returns>object</returns>
        public object GetObjectCheckType(string name, Type type)
        {
            // Handle missing object:
            if (nameToIndex.ContainsKey(name) == false)
            {
                Console.WriteLine($"No such object named {name} exists - returning NULL!");

                return null;
            }

            object obj = objList[nameToIndex[name]];

            // Check if object is of wrong type.
            Type objType = obj.GetType();
            if ( objType != type )
            {
                Console.WriteLine($"Object named {name} exists, but has WRONG TYPE! Returning NULL ...");

                return null;

                // throw new ArgumentException("Name-matched object in repo list has wrong TYPE!", nameof(obj));
            }

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
