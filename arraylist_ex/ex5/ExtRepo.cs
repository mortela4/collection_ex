using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ex5
{
    public static class ExtRepo
    {
        public static Repo StartsWith(this Repo repo, string pattern)
        {
            Repo subRepo = new Repo();

            foreach (string key in repo.nameToIndex.Keys)
            {
                if ( key.StartsWith(pattern))
                {
                    int idx = repo.nameToIndex[key];
                    // Debug:
                    if (repo.debug) Console.WriteLine($"Object {idx} named {key} starts with pattern {pattern} ...");

                    subRepo.RegisterObject(repo.objList[idx], key);
                }
            }

            return subRepo;
        }

        public static Repo EndsWith(this Repo repo, string pattern)
        {
            Repo subRepo = new Repo();

            foreach (string key in repo.nameToIndex.Keys)
            {
                if (key.EndsWith(pattern))
                {
                    int idx = repo.nameToIndex[key];
                    // Debug:
                    if (repo.debug) Console.WriteLine($"Object {idx} named {key} ends with pattern {pattern} ...");

                    subRepo.RegisterObject(repo.objList[idx], key);
                }
            }

            return subRepo;
        }

        public static Repo Contains(this Repo repo, string pattern)
        {
            Repo subRepo = new Repo();

            foreach (string key in repo.nameToIndex.Keys)
            {
                if (key.Contains(pattern))
                {
                    int idx = repo.nameToIndex[key];
                    // Debug:
                    if (repo.debug) Console.WriteLine($"Object {idx} named {key} contains pattern {pattern} ...");

                    subRepo.RegisterObject(repo.objList[idx], key);
                }
            }

            return subRepo;
        }

        // ************************** Equivalent methods - but specifying TYPE ******************************

        /// <summary>
        /// Creates subset of original repo.
        /// Object list has keys containing search pattern, 
        /// and is of given type.
        /// </summary>
        /// <param name="repo"></param>
        /// <param name="pattern"></param>
        /// <returns></returns>
        public static Repo StartsWith(this Repo repo, string pattern, Type type)
        {
            Repo subRepo = new Repo();

            foreach (string key in repo.nameToIndex.Keys)
            {
                if ( key.StartsWith(pattern) )
                {
                    int idx = repo.nameToIndex[key];

                    if ( repo.objList[idx].GetType() == type )
                    {
                        // Debug:
                        if (repo.debug) Console.WriteLine($"Object {idx} named {key} starts with pattern {pattern} and is of type={type.Name}  ...");

                        subRepo.RegisterObject(repo.objList[idx], key);
                    }
                }
            }

            return subRepo;
        }

        public static Repo EndsWith(this Repo repo, string pattern, Type type)
        {
            Repo subRepo = new Repo();

            foreach (string key in repo.nameToIndex.Keys)
            {
                if (key.EndsWith(pattern))
                {
                    int idx = repo.nameToIndex[key];

                    if (repo.objList[idx].GetType() == type)
                    {
                        // Debug:
                        if (repo.debug) Console.WriteLine($"Object {idx} named {key} ends with pattern {pattern} and is of type={type.Name} ...");

                        subRepo.RegisterObject(repo.objList[idx], key);
                    }
                }
            }

            return subRepo;
        }

        public static Repo Contains(this Repo repo, string pattern, Type type)
        {
            Repo subRepo = new Repo();

            foreach (string key in repo.nameToIndex.Keys)
            {
                if (key.Contains(pattern))
                {
                    int idx = repo.nameToIndex[key];

                    if (repo.objList[idx].GetType() == type)
                    {
                        // Debug:
                        if (repo.debug) Console.WriteLine($"Object {idx} named {key} contains pattern {pattern} and is of type={type.Name} ...");

                        subRepo.RegisterObject(repo.objList[idx], key);
                    }
                }
            }

            return subRepo;
        }

        // ********************** Matching based on type/subtype/interface ************************

        public static Repo OfType(this Repo repo, Type type)
        {
            Repo subRepo = new Repo();

            foreach (string key in repo.nameToIndex.Keys)
            {
                int idx = repo.nameToIndex[key];

                object obj = repo.objList[idx];

                if ( obj.GetType() == type )
                {
                    // Debug:
                    if (repo.debug) Console.WriteLine($"Object {idx} named '{key}' is of type '{type.Name}' ...");

                    subRepo.RegisterObject(repo.objList[idx], key);
                }
            }

            return subRepo;
        }

    }
}
