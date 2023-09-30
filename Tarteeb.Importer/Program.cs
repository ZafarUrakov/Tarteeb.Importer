//=================================
// Copyright (c) Tarteeb LLC.
// Powering True Leadership
//===============================

using System;
using System.Threading.Tasks;
using Tarteeb.Importer.Brokers.DataTimes;
using Tarteeb.Importer.Brokers.Storages;
using Tarteeb.Importer.Models.Clients;
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

            var client = new Client
            {
                Id = Guid.NewGuid(),
                Firstname = "",
                Lastname = "",
                BirthDate = DateTimeOffset.Parse("1/1/2010"),
                Email = "johndoejcc.com",
                PhoneNumber = "+1111111111",
                GroupId = Guid.NewGuid()
            };

            var persistedClient = await clientService.AddClientAsync(client);

            Console.WriteLine(persistedClient.Id);
        }
    }
}