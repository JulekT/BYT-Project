using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    public class NumberIsNotPositive : Exception
    {
        public NumberIsNotPositive() { }

        public NumberIsNotPositive(string message) : base(message) { }

        public NumberIsNotPositive(string message, Exception innerException) : base(message, innerException) { }
    }
}
