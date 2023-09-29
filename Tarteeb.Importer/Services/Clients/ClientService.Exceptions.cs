//=================================
// Copyright (c) Tarteeb LLC.
// Powering True Leadership
//===============================

using EFxceptions.Models.Exceptions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using Tarteeb.Importer.Models.Clients;
using Tarteeb.Importer.Models.Clients.Exceptions;
using Xeptions;

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
                throw CreateValidationException(nullClientException); ;
            }
            catch (InvalidClientException invalidClientException)
            {
                throw CreateValidationException(invalidClientException);
            }
            catch (DuplicateKeyException duplicateKeyException)
            {
                var alreadyExistsClientException =
                    new AlreadyExistsClientException(duplicateKeyException);

                throw CreateDependencyValidationException(alreadyExistsClientException);
            }
            catch (DbUpdateConcurrencyException dbUpdateConcurrencyException)
            {
                var locedClientException = new LocedClientException(dbUpdateConcurrencyException);

                throw CreateDependencyException(locedClientException);
            }
            catch (DbUpdateException dbUpdateException)
            {
                var failedStorageClientException = new FailedStorageClientException(dbUpdateException);

                throw CreateDependencyException(failedStorageClientException);
            }
            catch (Exception exception)
            {
                var failedServiceClientException = new FailedServiceClientException(exception);

                throw CreateServiceException(failedServiceClientException);
            }
        }

        private ClientValidationException CreateValidationException(Xeption xeption)
        {
            var clientValidationException = new ClientValidationException(xeption);

            return clientValidationException;
        }

        private ClientDependencyValidationException CreateDependencyValidationException(Xeption xeption)
        {
            var clientDependencyValidationException =
                 new ClientDependencyValidationException(xeption);

            return clientDependencyValidationException;
        }

        private ClientDependencyException CreateDependencyException(Xeption xeption)
        {
            ClientDependencyException clientDependencyException =
                new ClientDependencyException(xeption);

            return clientDependencyException;
        }

        private ClientServiceException CreateServiceException(Xeption xeption)
        {
            ClientServiceException clientServiceException = new ClientServiceException(xeption);

            return clientServiceException;
        }
    }
}
