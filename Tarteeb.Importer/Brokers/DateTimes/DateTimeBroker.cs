using System;

namespace Tarteeb.Importer.Brokers.DataTimes
{
    internal class DateTimeBroker
    {
        protected dynamic IsInvalidBirthData(DateTimeOffset birthDate)
        {
            var today = DateTimeOffset.UtcNow;
            var age = today.Year - birthDate.Year;

            if (birthDate.Month >= today.Month && 
                birthDate.Day >= today.Day)
                age--;

            return new
            {
                Condition = age < 6,
                Message = "User must be 6 years old or older"
            };
        }
    }
}
