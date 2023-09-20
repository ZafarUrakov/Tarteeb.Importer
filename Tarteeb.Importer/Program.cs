//=================================
// Copyright (c) Tarteeb LLC.
// Powering True Leadership
//===============================

using System;
using System.Linq;
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
                Firstname = "Amina",
                Lastname = "Mahmudova",
                PhoneNumber = "12345617890",
                Email = "amina@inom.com",
                BirthDate = DateTime.Now,
                GroupId = Guid.NewGuid()
            };

            using (var storageBroker = new StorageBroker())
            {
                Client persistedClient = await storageBroker.InsertClientAsync(client);
                IQueryable<Client> dbClients = storageBroker.SelectAllClients();
                
                foreach(var dbClient in dbClients)
                {
                    Console.WriteLine("name: " + dbClient.Firstname + " email: " + dbClient.Email);
                }
            }
        }
    }
}