//=================================
// Copyright (c) Tarteeb LLC.
// Powering True Leadership
//===============================

using EFxceptions.Models.Exceptions;
using Xeptions;

namespace Tarteeb.Importer.Models.Clients.Exceptions
{
    internal class AlreadyExistsClientException : Xeption
    {
        private DuplicateKeyException duplicateKeyException;

        public AlreadyExistsClientException(Xeption innerException)
         : base(message: "Client already exists.", innerException)
        { }

        public AlreadyExistsClientException(DuplicateKeyException duplicateKeyException)
        {
            this.duplicateKeyException = duplicateKeyException;
        }
    }
}
