//=================================
// Copyright (c) Tarteeb LLC.
// Powering True Leadership
//===============================

using System;
using Xeptions;

namespace Tarteeb.Importer.Models.Clients.Exceptions
{
    internal class LocedClientException : Xeption
    {
        public LocedClientException(Exception innerException)
            : base(message: "Client is locked, try again later.", innerException)
        { }
    }
}
