//=================================
// Copyright (c) Tarteeb LLC.
// Powering True Leadership
//===============================

using Bogus;
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

            var fake = new Faker();

            for (int clientIndex = 0; clientIndex <= 2000; clientIndex++)
            {

                var client = new Client
                {
                    Id = fake.Random.Guid(),
                    Firstname = fake.Name.FindName(),
                    Lastname = fake.Name.LastName(),
                    BirthDate = fake.Date.PastOffset(20, DateTime.Now.AddYears(-18)),
                    Email = fake.Internet.Email(),
                    PhoneNumber = "+" + fake.Phone.PhoneNumber(),
                    GroupId = fake.Random.Guid()
                };

                try
                {
                    var persistedClient = await clientService.AddClientAsync(client);

                    Console.WriteLine($"Client with id {persistedClient.Id} added.");
                }
                catch (ClientValidationException clientValidationException)
                {
                    foreach (DictionaryEntry entry in clientValidationException.Data)
                    {
                        string errorSummary = string.Join(", ", (List<string>)entry.Value);

                        Console.WriteLine(entry.Key + " - " + errorSummary);
                    }
                    Console.WriteLine($"Message: {clientValidationException.Message}");
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
            }
        }
    }
}