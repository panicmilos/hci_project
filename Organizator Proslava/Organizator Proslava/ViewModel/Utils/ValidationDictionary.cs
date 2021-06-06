﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Organizator_Proslava.ViewModel.Utils
{
    public static class ValidationDictionary
    {
        private static readonly EmailAddressAttribute _email = new EmailAddressAttribute();
        private static readonly PhoneAttribute _phone = new PhoneAttribute();

        private static readonly IDictionary<string, Func<object, object, string>> _validations = new Dictionary<string, Func<object, object, string>>
        {
            {"FirstName", ValidateFirstName},
            {"LastName", ValidateLastName},
            {"MailAddress", ValidateMailAddress},
            {"UserName", ValidateUserName},
            {"PhoneNumber", ValidatePhoneNumber},
            {"Password", ValidatePassword},
            {"RepeatedPassword", ValidateRepeatedPassword},
        };

        public static string Validate(string validationName, object firstParam, object secondParam)
        {
            if (!_validations.ContainsKey(validationName)) return null;

            return _validations[validationName].Invoke(firstParam, secondParam);
        }

        private static string ValidateFirstName(object firstName, object _)
        {
            if (string.IsNullOrWhiteSpace(firstName as string))
                return "Morate zadati ime.";

            return null;
        }

        private static string ValidateLastName(object lastName, object _)
        {
            if (string.IsNullOrWhiteSpace(lastName as string))
                return "Morate zadati prezime.";

            return null;
        }

        private static string ValidateMailAddress(object mailAddressObject, object _)
        {
            var mailAddress = mailAddressObject as string;
            if (string.IsNullOrWhiteSpace(mailAddress))
                return "Morate zadati mail adresu.";
            if (!_email.IsValid(mailAddress))
                return "Nevalidna mail adresa.";

            return null;
        }

        private static string ValidatePhoneNumber(object phoneNumberObject, object _)
        {
            var phoneNumber = phoneNumberObject as string;
            if (string.IsNullOrWhiteSpace(phoneNumber))
                return "Morate zadati broj telefona.";
            if (!_phone.IsValid(phoneNumber))
                return "Nevalidan broj telefona.";

            return null;
        }

        private static string ValidateUserName(object userNameObject, object _)
        {
            var userName = userNameObject as string;
            if (string.IsNullOrWhiteSpace(userName))
                return "Morate zadati korisničko ime.";
            if (userName.Trim().Length < 5)
                return "Korisničko ime je prekratko.";
            if (userName.Trim().Length > 15)
                return "Korisničko ime je predugačko.";

            return null;
        }

        private static string ValidatePassword(object passwordObject, object repeatedPasswordObject)
        {
            var password = passwordObject as string;
            var repeatedPassword = repeatedPasswordObject as string;
            if (string.IsNullOrWhiteSpace(password))
                return "Morate kreirati šifru.";
            if (password != repeatedPassword)
                return " ";

            return null;
        }

        private static string ValidateRepeatedPassword(object passwordObject, object repeatedPasswordObject)
        {
            var password = passwordObject as string;
            var repeatedPassword = repeatedPasswordObject as string;
            if (string.IsNullOrWhiteSpace(repeatedPassword))
                return "Morate ponoviti šifru.";
            if (repeatedPassword != password)
                return "Nije ista kao šifra.";

            return null;
        }
    }
}