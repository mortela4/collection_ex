using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ex7_demo
{
    /// <summary>
    /// TODO: possibly have a check on '_name' in object-list before usage of any public method?
    /// </summary>
    public class DemoClassA : IDemoInterface
    {
        private string _name;

        // ctor
        public DemoClassA(string name)
        {
            _name = name;
            Globals.objRepo.RegisterObject(this, _name); 
        }
        // dtor
        ~DemoClassA()
        {
            Globals.objRepo.DeregisterObject(_name);
        }


        public void ClassApubMethod()
        {
            Console.WriteLine($"ClassApubMethod(): instance name is '{_name}' ... ");
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

        public void UseOtherClassInstances(string nameB, string nameC)
        {
            // Get objects:
            var obj1 = Globals.objRepo.GetObjectCheckType(nameB, typeof(DemoClassB)) as DemoClassB;
            var obj2 = Globals.objRepo.GetObjectCheckType(nameC, typeof(DemoClassC)) as DemoClassC;

            // Use objects (TODO: null-check etc!):
            if (obj1 != null) obj1.ClassBpubMethod();
            if (obj2 != null) obj2.ClassCpubMethod();
        }

        public void RemoveRef()
        {
            Globals.objRepo.DeregisterObject(_name);
        }
    }
}
