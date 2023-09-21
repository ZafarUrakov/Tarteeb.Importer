using System;
using Tarteeb.Importer.Models.Clients.Exceptions;

namespace Tarteeb.Importer.Brokers.Logging
{
    internal class LoggingBroker
    {
        public void LoggingError(NullClientException nullClientException) =>
            Console.WriteLine(nullClientException.Message);
    }
}
