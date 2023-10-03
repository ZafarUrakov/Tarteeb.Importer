//=================================
// Copyright (c) Tarteeb LLC.
// Powering True Leadership
//===============================

using System;
using System.Text.RegularExpressions;
using Tarteeb.Importer.Models.Clients;
using Tarteeb.Importer.Models.Clients.Exceptions;

namespace Tarteeb.Importer.Services.Clients
{
    internal partial class ClientService
    {

        private void ValidateClientOnAdd(Client client)
        {
            ValidateClientNotNull(client);

            Validate(
                (Rule: IsInvalid(client.Id), Parameter: nameof(client.Id)),
                (Rule: IsInvalid(client.Firstname), Parameter: nameof(client.Firstname)),
                (Rule: IsInvalid(client.Lastname), Parameter: nameof(client.Lastname)),
                (Rule: IsInvalid(client.BirthDate), Parameter: nameof(client.BirthDate)),
                (Rule: IsLessThen12(client.BirthDate), Parameter: nameof(client.BirthDate)),
                (Rule: IsInvalid(client.GroupId), Parameter: nameof(client.GroupId)),
                (Rule: IsInvalid(client.Email), Parameter: nameof(client.Email)));

            Validate(
            (Rule: IsInvalidEmail(client.Email), Parameter: nameof(client.Email)),
            (Rule: IsInvalidPhoneNumber(client.PhoneNumber), Parameter: nameof(client.PhoneNumber)));
        }

        private void ValidateClientNotNull(Client client)
        {
            if (client == null)
            {
                loggingBroker.LoggingError(new NullClientException());
            }
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

        private dynamic IsInvalid(DateTimeOffset date) => new
        {
            Condition = date == default,
            Message = "Date is required"
        };

        private dynamic IsLessThen12(DateTimeOffset date) => new
        {
            Condition = IsAgeLessThen12(date),
            Message = "Age is less than 12"
        };

        private bool IsAgeLessThen12(DateTimeOffset date)
        {
            DateTimeOffset now = this.dateTimeBroker.GetDateTimeOffset();
            int age = (now - date).Days / 365;

            return age < 12;
        }

        private dynamic IsInvalidEmail(string email) => new
        {
            Condition = !Regex.IsMatch(email, @"^(.+)@(.+)$"),
            Message = "Email is invalid"
        };

        private dynamic IsInvalidPhoneNumber(string phoneNumber) => new
        {
            Condition = !Regex.IsMatch(phoneNumber, @"^\+?[1-9][0-9]{7,14}$"),
            Message = "Phone number is invalid"
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
