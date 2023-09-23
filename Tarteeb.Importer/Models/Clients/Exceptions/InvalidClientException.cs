//=================================
// Copyright (c) Tarteeb LLC.
// Powering True Leadership
//===============================

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