using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TopUp.Application
{
    public class ApiApplicationException : Exception
    {
        public ApiApplicationException()
        { }

        public ApiApplicationException(string message)
            : base(message)
        { }

        public ApiApplicationException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }
}
