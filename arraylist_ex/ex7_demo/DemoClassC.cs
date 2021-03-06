﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ex7_demo
{
    public class DemoClassC : IDemoInterface
    {
        private string _name;

        // ctor
        public DemoClassC(string name)
        {
            _name = name;
            Globals.objRepo.RegisterObject(this, _name);
        }
        // dtor
        ~DemoClassC()
        {
            Globals.objRepo.DeregisterObject(_name);
        }


        public void ClassCpubMethod()
        {
            Console.WriteLine($"ClassCpubMethod(): instance name is '{_name}' ... ");
        }

        public void InterfaceMethod()
        {
            Console.WriteLine($"ClassA InterfaceMethod(): instance name is '{_name}' ... ");
        }

        // Only for proto use:
        class Proto : IDemoInterface
        {
            public void InterfaceMethod()
            {
                throw new NotImplementedException();
            }
            public void RemoveRef()
            {
                throw new NotImplementedException();
            }
        }
        public void UseOtherClassInstance(string name)
        {
            IDemoInterface objProto = new Proto();

            // Get object:
            var obj = Globals.objRepo.GetObjectCheckDerivedType(name, objProto) as IDemoInterface;

            // Use object:
            if (obj != null) obj.InterfaceMethod();
        }

        public void UseOtherClassInstances(string nameA, string nameB)
        {
            // Get objects:
            var obj1 = Globals.objRepo.GetObjectCheckType(nameA, typeof(DemoClassA)) as DemoClassA;
            var obj2 = Globals.objRepo.GetObjectCheckType(nameB, typeof(DemoClassB)) as DemoClassB;

            // Use objects (TODO: null-check etc!):
            if (obj1 != null) obj1.ClassApubMethod();
            if (obj2 != null) obj2.ClassBpubMethod();
        }

        public void RemoveRef()
        {
            Globals.objRepo.DeregisterObject(_name);
        }
    }
}
