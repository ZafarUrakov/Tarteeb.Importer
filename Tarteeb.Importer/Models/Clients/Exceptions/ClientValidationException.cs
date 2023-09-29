//=================================
// Copyright (c) Tarteeb LLC.
// Powering True Leadership
//===============================

using Xeptions;

namespace Tarteeb.Importer.Models.Clients.Exceptions
{
    internal partial class ClientValidationException : Xeption
    {
        public ClientValidationException(Xeption innerException)
            : base(message: "Client validation error occured. Fix the errors and try again.",
                  innerException)
        { }
    }
}
