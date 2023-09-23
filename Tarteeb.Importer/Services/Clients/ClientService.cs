//=================================
// Copyright (c) Tarteeb LLC.
// Powering True Leadership
//===============================

using System.Threading.Tasks;
using Tarteeb.Importer.Brokers.Logging;
using Tarteeb.Importer.Brokers.Storages;
using Tarteeb.Importer.Models.Clients;
using Tarteeb.Importer.Models.Clients.Exceptions;

namespace Tarteeb.Importer.Services.Clients
{
    internal class ClientService
    {
        private readonly StorageBroker storageBroker;
        private readonly LoggingBroker loggingBroker;

        public ClientService()
        {
            this.storageBroker = new StorageBroker();
            this.loggingBroker = new LoggingBroker();
        }

        internal Task<Client> AddClientAsync(Client client)
        {
            if (client == null)
            {
                loggingBroker.LoggingError(new NullClientException());
            }

            return this.storageBroker.InsertClientAsync(client);
        }
    }
}
