//=================================
// Copyright (c) Tarteeb LLC.
// Powering True Leadership
//===============================

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
            ClientService clientService = new ClientService(
                storageBroker: new StorageBroker(),
                dateTimeBroker: new DateTimeBroker());

            var loggingBroker = new LoggingBroker();

            var client = new Client
            {
                Id = Guid.NewGuid(),
                Firstname = "",
                Lastname = "",
                BirthDate = DateTimeOffset.Parse("1/1/2010"),
                PhoneNumber = "+1111111111",
                GroupId = Guid.NewGuid()
            };

            try
            {
                var persistedClient = await clientService.AddClientAsync(client);

                Console.WriteLine(persistedClient.Id);

            }
            catch (ClientValidationException ClientValidationException)
            {
                if (ClientValidationException.InnerException is InvalidClientException invalidClientXeception)
                {
                    foreach (DictionaryEntry key in invalidClientXeception.Data)
                    {
                        string ErrorSummary = ((List<string>)key.Value)
                            .Select((string value) => value).
                            Aggregate((string current, string next) => current + ", " + next);

                        Console.WriteLine(key.Key + " - " + ErrorSummary);
                    }
                    Console.WriteLine($"Message: {invalidClientXeception.Message}");
                }
                throw;
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