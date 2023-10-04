//=================================
// Copyright (c) Tarteeb LLC.
// Powering True Leadership
//===============================

using System;
using Xeptions;

namespace Tarteeb.Importer.Models.Clients.Exceptions
{
    internal class AlreadyExistsClientException : Xeption
    {
        public AlreadyExistsClientException(Exception innerException)
         : base(message: "Client already exists.", innerException)
        { }
    }
}
