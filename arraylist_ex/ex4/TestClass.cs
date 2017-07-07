using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ex4
{
    class TestClass
    {
        private int _val = 0;

        public TestClass(int v)
        {
            _val = v;
        }

        public int GetVal()
        {
            return _val;
        }

        public void SetVal(int v)
        {
            _val = v;
        }

        public override string ToString()
        {
            return _val.ToString();
        }
    }
}
