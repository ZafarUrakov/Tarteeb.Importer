using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tarteeb.Importer.Models.Clients.Exceptions;

namespace Tarteeb.Importer.Brokers.Logging
{
    internal class LoggingBroker
    {
        public void LoggingError(NullClientException nullClientException) =>
            Console.WriteLine(nullClientException.Message);
    }
}
