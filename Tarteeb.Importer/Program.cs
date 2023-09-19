//=================================
// Copyright (c) Tarteeb LLC.
// Powering True Leadership
//===============================

using System;
using System.Threading.Tasks;
using Tarteeb.Importer.Brokers.Storages;
using Tarteeb.Importer.Models.Clients;

namespace Tarteeb.Importer
{
    public class Program
    {
        static async Task Main(string[] args)
        {
            var client = new Client
            {
                Id = Guid.NewGuid(),
                Firstname = "Abdulloh",
                Lastname = "Mahmudov",
                PhoneNumber = "1234567890",
                Email = "null",
                BirthDate = DateTime.Now,
                GroupId = Guid.NewGuid()
            };

            using (var storageBroker = new StorageBroker())
            {
                await storageBroker.InsertClientAsync(client);
            }
        }
    }
}