using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Organizator_Proslava.ViewModel.Utils
{
    public static class ValidationDictionary
    {
        private static readonly IDictionary<string, Func<object, object, string>> _validations;

        static ValidationDictionary()
        {
            _validations = new Dictionary<string, Func<object, object, string>>
            {
                {"FirstName", ValidateFirstName},
                {"LastName", ValidateLastName},
                {"MailAddress", ValidateMailAddress},
                {"UserName", ValidateUserName},
                {"PhoneNumber", ValidatePhoneNumber},
                {"Password", ValidatePassword},
                {"RepeatedPassword", ValidateRepeatedPassword},
            };
        }

        public static string Validate(string validationName, object firstParam, object secondParam)
        {
            if (!_validations.ContainsKey(validationName))
            {
                return null;
            }

            return _validations[validationName].Invoke(firstParam, secondParam);
        }

        private static string ValidateFirstName(object firstName, object _)
        {
            if (string.IsNullOrWhiteSpace(firstName as String))
                return "Morate zadati ime.";

            return null;
        }

        private static string ValidateLastName(object lastName, object _)
        {
            if (string.IsNullOrWhiteSpace(lastName as String))
                return "Morate zadati prezime.";

            return null;
        }

        private static string ValidateMailAddress(object mailAddressObject, object _)
        {
            var emailValidator = new EmailAddressAttribute();

            var mailAddress = mailAddressObject as String;
            if (string.IsNullOrWhiteSpace(mailAddress))
                return "Morate zadati mail adresu.";
            if (!emailValidator.IsValid(mailAddress))
                return "Nevalidna mail adresa.";

            return null;
        }

        private static string ValidatePhoneNumber(object phoneNumber, object _)
        {
            if (string.IsNullOrWhiteSpace(phoneNumber as String))
                return "Morate zadati broj telefona.";
            //TODO: validate format

            return null;
        }

        private static string ValidateUserName(object userNameObject, object _)
        {
            var userName = userNameObject as String;
            if (string.IsNullOrWhiteSpace(userName))
                return "Morate zadati korisnicko ime.";
            if (userName.Trim().Length < 5)
                return "Korisnicko ime je prekratko.";
            if (userName.Trim().Length > 15)
                return "Korisnicko ime je predugacko.";

            return null;
        }

        private static string ValidatePassword(object passwordObject, object repeatedPasswordObject)
        {
            var password = passwordObject as String;
            var repeatedPassword = repeatedPasswordObject as String;

            if (string.IsNullOrWhiteSpace(password))
                return "Morate kreirati sifru.";
            if (password != repeatedPassword)
                return " ";

            return null;
        }

        private static string ValidateRepeatedPassword(object passwordObject, object repeatedPasswordObject)
        {
            var password = passwordObject as String;
            var repeatedPassword = repeatedPasswordObject as String;

            if (string.IsNullOrWhiteSpace(repeatedPassword))
                return "Morate ponoviti sifru.";
            if (repeatedPassword != password)
                return "Nije ista kao sifra.";

            return null;
        }
    }
}