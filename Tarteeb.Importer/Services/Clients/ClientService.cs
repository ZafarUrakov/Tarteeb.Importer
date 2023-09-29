//=================================
// Copyright (c) Tarteeb LLC.
// Powering True Leadership
//===============================

using System.Threading.Tasks;
using Tarteeb.Importer.Brokers.DataTimes;
using Tarteeb.Importer.Brokers.Loggings;
using Tarteeb.Importer.Brokers.Storages;
using Tarteeb.Importer.Models.Clients;

namespace Tarteeb.Importer.Services.Clients
{
    internal partial class ClientService
    {
        private readonly StorageBroker storageBroker;
        private readonly LoggingBroker loggingBroker;
        private readonly DateTimeBroker dateTimeBroker;

        public ClientService()
        {
            this.storageBroker = new StorageBroker();
            this.loggingBroker = new LoggingBroker();
            this.dateTimeBroker = new DateTimeBroker();
        }

        internal Task<Client> AddClientAsync(Client client) =>
        TryCatch(() =>
        {
            ValidateClientOnAdd(client);

            return this.storageBroker.InsertClientAsync(client);
        });
    }
}
