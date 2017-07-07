using System;
using System.Collections;
using System.Collections.Generic;


namespace ex5
{
    public class Repo
    {
        public bool debug = false;

        // Creates and initializes a new ArrayList.
        public ArrayList objList = new ArrayList();
        public Dictionary<string, int> nameToIndex = new Dictionary<string, int>();

        private int idx = 0;

        public Repo(bool debug=false)
        {
            this.debug = debug;
        }

        private void UpdateObjectIndexes(string name)
        {
            int fromIndex = nameToIndex[name];
            Dictionary<string, int> temp = new Dictionary<string, int>();

            // Remove key from dict:
            nameToIndex.Remove(name);

            foreach (var key in nameToIndex.Keys)
            {              
                int currentIndex = nameToIndex[key];
                if (currentIndex > fromIndex)
                {
                    int newIndex = currentIndex - 1;
                    // Assign new index, decremented by one:
                    temp[key] = newIndex;
                }
                else
                {
                    temp[key] = currentIndex;
                }
            }

            // Re-assign:
            nameToIndex = temp;

            // Decrement total number of objects:
            --idx;
        }

        private void AddObject(object obj, string name)
        {
            lock(objList)
            {
                objList.Add(obj);
                nameToIndex[name] = idx;
                ++idx;
            }          
        }

        private void RemoveObject(string name)
        {
            int index = 0;
            // Make updates thread-safe:
            lock(objList)
            {
                index = nameToIndex[name];
                // Update object-list:
                objList.RemoveAt(index);
                // Update dictionary:
                UpdateObjectIndexes(name);

                // Debug:
                Console.WriteLine($"Removed object '{name}' from object-list at index={index} ...");
            }
        }

        public void RegisterObject(object obj, string name)
        {
            // Add protection against adding obj w. same name!
            if (nameToIndex.ContainsKey(name))
            {
                Console.WriteLine("Attempt to add object with same name as existing object in list!");
                return;
                //throw new ArgumentException("Attempted to add same object twice!", nameof(obj));
            }

            // Protection against adding SAME object twice:
            if (objList.Contains(obj))
            {
                Console.WriteLine("Attempt to add same object twice!");
                return;
                //throw new ArgumentException("Attempted to add same object twice!", nameof(obj));
            }

            // Checks=OK --> add object to list & update dictionary:
            AddObject(obj, name);
        }

        public void DeregisterObject(string name)
        {
            // Add protection against adding obj w. same name!
            if (nameToIndex.ContainsKey(name) == false)
            {
                Console.WriteLine("Object to remove not in list!");
                return;
                //throw new ArgumentException("Attempted to remove object not in list!", nameof(obj));
            }

            // Checks=OK --> remove object from list & update dictionary:
            RemoveObject(name);
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
            if (nameToIndex.ContainsKey(name) == false)
            {
                Console.WriteLine($"No such object named {name} exists - returning NULL!");

                return null;
            }

            int index = nameToIndex[name];
            // Check index mismatch:
            if (index >= objList.Count)
            {
                Console.WriteLine($"Non-existing index {index} - returning NULL!");

                return null;
            }
            object obj = objList[index];
            Console.WriteLine($"Returned object no.{index} named '{name}': {obj.ToString()}");

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
            if (objType != type)
            {
                Console.WriteLine($"Object named {name} exists, but has WRONG TYPE! Returning NULL ...");

                return null;

                // throw new ArgumentException("Name-matched object in repo list has wrong TYPE!", nameof(obj));
            }

            Console.WriteLine($"Returned object no.{idx} named '{name}': {obj.ToString()}");

            return obj;
        }


        public object GetObjectCheckDerivedType<T>(string name, T protoObj)
        {
            // Handle missing object:
            if ( nameToIndex.ContainsKey(name) == false )
            {
                Console.WriteLine($"No such object named {name} exists - returning NULL!");

                return null;
            }

            object obj = objList[nameToIndex[name]];

            // Check if object is of wrong type.
            Type objType = obj.GetType();

            // NOTE: shallow checking - may be DANGEROUS!
            if (typeof(T).IsAssignableFrom(objType) == false )
            {
                string objProtoTypeName = typeof(T).Name;
                Console.WriteLine($"Object named {name} exists, but cannot be assigned to object of type '{objProtoTypeName}'! Returning NULL ...");

                return null;

                // throw new ArgumentException("Name-matched object in repo list has wrong TYPE!", nameof(obj));
            }

            Console.WriteLine($"Returned object no.{idx} named '{name}': {obj.ToString()}");

            return obj;
        }

 
        public void PrintValues()
        {
            if ( objList.Count == 0 )
            {
                // Debug:
                Console.WriteLine("WARNING: object list is empty!");

                return;
            }

            foreach (var obj in objList)
                Console.Write("   {0}", obj);
            Console.WriteLine();
        }
    }
}
