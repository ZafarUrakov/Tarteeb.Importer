//=================================
// Copyright (c) Tarteeb LLC.
// Powering True Leadership
//===============================

using System;
using Tarteeb.Importer.Models.Clients.Exceptions;

namespace Tarteeb.Importer.Brokers.Loggings
{
    internal class LoggingBroker
    {
        public void LoggingError(NullClientException nullClientException) =>
            Console.WriteLine(nullClientException.Message);
    }
}
