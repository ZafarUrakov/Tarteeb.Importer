using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xeptions;

namespace Tarteeb.Importer.Models.Clients.Exceptions
{
    internal class InvalidClientException : Xeption
    {
        public InvalidClientException()
            : base(message: "Client is invalid. Fix the error and try again.") 
        { }
    }
}
