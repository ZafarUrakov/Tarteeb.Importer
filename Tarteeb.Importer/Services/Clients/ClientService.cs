
//=================================
// Copyright (c) Tarteeb LLC.
// Powering True Leadership
//===============================

using System;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Tarteeb.Importer.Brokers.DataTimes;
using Tarteeb.Importer.Brokers.Logging;
using Tarteeb.Importer.Brokers.Storages;
using Tarteeb.Importer.Models.Clients;
using Tarteeb.Importer.Models.Clients.Exceptions;

namespace Tarteeb.Importer.Services.Clients
{
    internal class ClientService : DateTimeBroker
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

            Validate(
                (Rule: IsInvalid(client.Id), Parameter: nameof(client.Id)),
                (Rule: IsInvalid(client.Firstname), Parameter: nameof(client.Firstname)),
                (Rule: IsInvalid(client.Lastname), Parameter: nameof(client.Lastname)),
                (Rule: IsInvalid(client.Email), Parameter: nameof(client.Email)));

            Validate(
                (Rule: IsInvalidBirthData(client.BirthDate), Parameter: nameof(client.BirthDate)),
                (Rule: IsInvalidEmail(client.Email), Parameter: nameof(client.Email)),
                (Rule: IsInvalidPhoneNumber(client.PhoneNumber), Parameter: nameof(client.PhoneNumber)));

            return this.storageBroker.InsertClientAsync(client);
        }

        private dynamic IsInvalid(Guid id) => new
        {
            Condition = id == default,
            Message = "Id is required"
        };

        private dynamic IsInvalid(string text) => new
        {
            Condition = String.IsNullOrWhiteSpace(text),
            Message = "Text is required"
        };

        private dynamic IsInvalidEmail(string email) => new
        {
            Condition = Regex.IsMatch(email, @"^(.+)@(.+)$"),
            Message = "Email is invalide"
        };

        private dynamic IsInvalidPhoneNumber(string phoneNumber) => new
        {
            Condition = Regex.IsMatch(phoneNumber, "^\\+?[1-9][0-9]{7,14}$"),
            Message = "Phone number is invalide"
        };

        private void Validate(params (dynamic Rule, string Parameter)[] validations)
        {
            var invalidClientException = new InvalidClientException();

            foreach ((dynamic rule, string parameter) in validations)
            {
                if (rule.Condition)
                {
                    invalidClientException.UpsertDataList(
                        key: parameter,
                        value: rule.Message);
                }
            }

            invalidClientException.ThrowIfContainsErrors();
        }
    }
}
