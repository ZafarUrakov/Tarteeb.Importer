//=================================
// Copyright (c) Tarteeb LLC.
// Powering True Leadership
//===============================

using System;

namespace Tarteeb.Importer.Brokers.Loggings
{
    internal class LoggingBroker
    {
        public void LoggingError(Exception exception) =>
            Console.WriteLine(exception.Message);
    }
}
