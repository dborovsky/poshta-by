using System;
using System.Collections.Generic;
using System.Text;

namespace PoshtaBy.Application.Common.Exceptions
{
    [Serializable]
    public class PoshtaByException : Exception
    {
        public PoshtaByException()
        {
        }

        public PoshtaByException(string message)
            : base(message)
        {
        }
    }
}
