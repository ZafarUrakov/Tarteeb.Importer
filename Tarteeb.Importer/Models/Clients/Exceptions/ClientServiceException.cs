//=================================
// Copyright (c) Tarteeb LLC.
// Powering True Leadership
//===============================

using Xeptions;

namespace Tarteeb.Importer.Models.Clients.Exceptions
{
    internal class ClientServiceException : Xeption
    {
        public ClientServiceException(Xeption innerException)
            : base(message: "Client service error occurred, contact support.", innerException)
        { }
    }
}
