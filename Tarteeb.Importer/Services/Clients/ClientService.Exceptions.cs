//=================================
// Copyright (c) Tarteeb LLC.
// Powering True Leadership
//===============================
using System.Threading.Tasks;
using Tarteeb.Importer.Models.Clients;
using Tarteeb.Importer.Models.Clients.Exceptions;

namespace Tarteeb.Importer.Services.Clients
{
    internal partial class ClientService
    {
        private delegate Task<Client> ReturninClientFunction();

        private Task<Client> TryCatch(ReturninClientFunction returninClientFunction)
        {
            try
            {
                return returninClientFunction();
            }
            catch (NullClientException nullClientException)
            {
                var clientValidationException =
                    new ClientValidationException(nullClientException);

                throw clientValidationException;
            }
            catch (InvalidClientException invalidClientException)
            {
                var clientValidationException =
                   new ClientValidationException(invalidClientException);

                throw clientValidationException;
            }
        }
    }
}
