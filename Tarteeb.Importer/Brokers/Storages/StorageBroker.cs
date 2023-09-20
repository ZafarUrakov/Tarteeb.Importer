//=================================
// Copyright (c) Tarteeb LLC.
// Powering True Leadership
//===============================

using EFxceptions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using Tarteeb.Importer.Models.Clients;

namespace Tarteeb.Importer.Brokers.Storages
{
    public class StorageBroker : EFxceptionsContext
    {
        public DbSet<Client> Clients { get; set; }

        public StorageBroker() =>
            this.Database.EnsureCreated();

        public async Task<Client> InsertClientAsync(Client client)
        {
            await this.Clients.AddAsync(client);
            await this.SaveChangesAsync();

            return client;
        }

        public IQueryable<Client> SelectAllClients() =>
            return this.Clients.AsQueryable();

        public async Task<Client> SelectClientByIdAsync(Guid clientId) =>
            await this.Clients.FindAsync(clientId);

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string connectionString = "Data source=..\\..\\..\\ Tarteeb.db";
            optionsBuilder.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
            optionsBuilder.UseSqlite(connectionString);
        }
    }
}
