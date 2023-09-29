//=================================
// Copyright (c) Tarteeb LLC.
// Powering True Leadership
//===============================
using Xeptions;

namespace Tarteeb.Importer.Models.Clients.Exceptions
{
    internal class ClientDependencyValidationException : Xeption
    {
        public ClientDependencyValidationException(Xeption innerException)
        : base(message: "Client dependency validation error occurred. Fix the error and try again. ", innerException)
        { }
    }
}
