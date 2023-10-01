//=================================
// Copyright (c) Tarteeb LLC.
// Powering True Leadership
//===============================

using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tarteeb.Importer.Brokers.DataTimes;
using Tarteeb.Importer.Brokers.Loggings;
using Tarteeb.Importer.Brokers.Storages;
using Tarteeb.Importer.Models.Clients;
using Tarteeb.Importer.Models.Clients.Exceptions;
using Tarteeb.Importer.Services.Clients;

namespace Tarteeb.Importer
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            LoggingBroker loggingBroker = new LoggingBroker();
            ClientService clientService = new ClientService(
                storageBroker: new StorageBroker(),
                dateTimeBroker: new DateTimeBroker());

            var client = new Client
            {
                Id = Guid.NewGuid(),
                Firstname = "John",
                Lastname = "Doe",
                BirthDate = DateTimeOffset.Parse("1/1/2010"),
                Email = "johndoe@jcc.com",
                PhoneNumber = "+1111111111",
                GroupId = Guid.NewGuid()
            };

            try
            {
                var persistedClient = await clientService.AddClientAsync(client);

                Console.WriteLine(persistedClient.Id);
            }
            catch (ClientValidationException clientValidationException)
            when (clientValidationException.InnerException is InvalidClientException invalidClientExceception)
            {
                foreach (DictionaryEntry entry in invalidClientExceception.Data)
                {
                    string errorSummary = string.Join(", ", (List<string>)entry.Value);

                    Console.WriteLine(entry.Key + " - " + errorSummary);
                }
                Console.WriteLine($"Message: {invalidClientExceception.Message}");
            }
            catch (ClientDependencyValidationException clientDependencyValidationException)
            {
                loggingBroker.LoggingError(clientDependencyValidationException);

                throw clientDependencyValidationException;
            }
            catch (ClientDependencyException clientDependencyException)
            {
                loggingBroker.LoggingError(clientDependencyException);

                throw clientDependencyException;
            }
            catch (ClientServiceException clientServiceException)
            {
                loggingBroker.LoggingError(clientServiceException);

                throw clientServiceException;
            }
            catch (Exception exception)
            {
                loggingBroker.LoggingError(exception);

                throw;
            }
        }
    }
}