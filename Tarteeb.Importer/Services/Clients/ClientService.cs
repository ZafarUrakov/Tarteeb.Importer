//=================================
// Copyright (c) Tarteeb LLC.
// Powering True Leadership
//===============================

using System.Threading.Tasks;
using Tarteeb.Importer.Brokers.DataTimes;
using Tarteeb.Importer.Brokers.Logging;
using Tarteeb.Importer.Brokers.Storages;
using Tarteeb.Importer.Models.Clients;
using Tarteeb.Importer.Models.Clients.Exceptions;

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

        /// <exception cref="ClientValidationException"></exception>
        internal Task<Client> AddClientAsync(Client client) =>
        TryCatch(() =>
        {
            ValidateClientOnAdd(client);

            return this.storageBroker.InsertClientAsync(client);
        });
    }
}
