//=================================
// Copyright (c) Tarteeb LLC.
// Powering True Leadership
//===============================

using System;

namespace Tarteeb.Importer.Brokers.DataTimes
{
    internal class DateTimeBroker
    {
        internal DateTimeOffset GetDateTimeOffset() =>
            DateTimeOffset.UtcNow;
    }
}