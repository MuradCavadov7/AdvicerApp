using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvicerApp.BL.Exceptions.Common
{
    public class UserLockedOutException : Exception
    {
        public UserLockedOutException() : base("User is locked out due to multiple failed login attempts.") { }

        public UserLockedOutException(string message) : base(message) { }

        public UserLockedOutException(string message, Exception innerException) : base(message, innerException) { }
    }
}
