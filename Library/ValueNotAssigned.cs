using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    public class ValueNotAssigned : Exception
    {
        public ValueNotAssigned() { }

        public ValueNotAssigned(string message) : base(message) { }

        public ValueNotAssigned(string message, Exception innerException) : base(message, innerException) { }
    }
}
