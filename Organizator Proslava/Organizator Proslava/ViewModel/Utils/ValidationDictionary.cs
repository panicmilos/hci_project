using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Linq;

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
            {"PIB", ValidatePIB},
            {"IdentificationNumber", ValidateIdentificationNumber},
            {"WholeAddress", ValidateWholeAddress},
            {"JMBG", ValidateJMBG},
            {"PersonalId", ValidatePersonalId},

            {"CSType", ValidateCSType},
            {"CSDescription", ValidateCSDescription},
            {"CSName", ValidateCSName},
            {"CSPrice", ValidateCSPrice},
            {"CSUnit", ValidateCSUnit},

            {"CHName", ValidateCHName},
            {"CHSeats", ValidateCHSeats},

            {"CelebrationType", ValidateCelebrationType},
            {"DateTimeFrom", ValidateDateTimeFrom},
            {"DateTimeTo", ValidateDateTimeTo},
            {"ExpectedNumberOfGuests", ValidateExpectedNumberOfGuests},
            {"BudgetFrom", ValidateBudget},
            {"BudgetTo", ValidateBudget},

            {"DetailTitle", ValidateDetailTitle},
            {"Content", ValidateContent},
            {"ProposalTitle", ValidateProposalTitle},
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

        private static string ValidatePIB(object PIBObject, object _)
        {
            var PIB = PIBObject as string;
            if (string.IsNullOrWhiteSpace(PIB))
                return "Morate zadati PIB.";

            if (PIB.Any(c => !char.IsDigit(c)))
                return "PIB morati imati samo cifre.";

            if (PIB.Length != 9)
                return "PIB nije ispravan.";

            if (double.Parse(PIB.Substring(0, 8)) < 10000001)
                return "PIB nije ispravan.";

            var rest = 10;
            for (int i = 0; i < 8; i++)
            {
                rest = (PIB[i] - '0' + rest) % 10;
                if (rest == 0)
                    rest = 10;

                rest = rest * 2 % 11;
            }
            if ((PIB[8] - '0') != ((11 - rest) % 10))
                return "PIB nije ispravan.";

            return null;
        }

        private static string ValidateIdentificationNumber(object MBObject, object _)
        {
            var MB = MBObject as string;
            if (string.IsNullOrWhiteSpace(MB as string))
                return "Morate zadati matični broj.";

            if (MB.Any(c => !char.IsDigit(c)))
                return "Matični broj morati imati samo cifre.";

            if (MB.Length != 8)
                return "Matični broj nije ispravan.";

            var L = 0;
            for (int i = MB.Length - 2, mnozilac = 2; i >= 0; i--)
            {
                L += (MB[i] - '0') * mnozilac;
                mnozilac = mnozilac == 7 ? 2 : mnozilac + 1;
            }
            L = 11 - (L % 11);
            L = L > 9 ? 0 : L;

            if (MB[7] - '0' != L)
                return "Matični broj nije ispravan.";

            return null;
        }

        private static string ValidateWholeAddress(object wholeAddress, object _)
        {
            if (string.IsNullOrWhiteSpace(wholeAddress as string))
                return "Morate zadati adresu.";

            return null;
        }

        private static string ValidateJMBG(object JMBGObject, object _)
        {
            var JMBG = JMBGObject as string;
            if (string.IsNullOrWhiteSpace(JMBG as string))
                return "Morate zadati JMBG.";

            if (JMBG.Any(c => !char.IsDigit(c)))
                return "JMBG mora imati samo cifre.";

            if (JMBG.Length != 13)
                return "JMBG nije ispravan.";

            var L = 11 - ((7 * (JMBG[0] - '0' + JMBG[6] - '0') + 6 * (JMBG[1] - '0' + JMBG[7] - '0') + 5 * (JMBG[2] - '0' + JMBG[8] - '0') +
                4 * (JMBG[3] - '0' + JMBG[9] - '0') + 3 * (JMBG[4] - '0' + JMBG[10] - '0') + 2 * (JMBG[5] - '0' + JMBG[11] - '0')) % 11);
            L = L <= 9 ? L : 0;

            if (JMBG[12] - '0' != L)
                return "JMBG nije ispravan.";

            return null;
        }

        private static string ValidatePersonalId(object personalIdObject, object _)
        {
            var personalId = personalIdObject as string;
            if (string.IsNullOrWhiteSpace(personalId as string))
                return "Morate zadati broj lične karte.";

            if (personalId.Any(c => !char.IsDigit(c)))
                return "Broj lične karte morati imati samo cifre.";

            if (personalId.Length != 9)
                return "Broj lične karte nije ispravan.";

            return null;
        }

        private static string ValidateCSType(object type, object _)
        {
            if (string.IsNullOrWhiteSpace(type as string))
                return "Morate zadati tip.";

            return null;
        }

        private static string ValidateCSDescription(object description, object _)
        {
            if (string.IsNullOrWhiteSpace(description as string))
                return "Morate zadati opis.";

            return null;
        }

        private static string ValidateCSName(object name, object _)
        {
            if (string.IsNullOrWhiteSpace(name as string))
                return "Morate zadati ime.";

            return null;
        }

        private static string ValidateCSPrice(object price, object _)
        {
            if (string.IsNullOrWhiteSpace(price as string))
                return "Morate zadati cenu.";

            if (!double.TryParse(price as string, out var _))
                return "Cena mora biti broj.";

            return null;
        }

        private static string ValidateCSUnit(object unit, object _)
        {
            if (string.IsNullOrWhiteSpace(unit as string))
                return "Morate zadati jedinicu.";

            return null;
        }

        private static string ValidateCHName(object name, object _)
        {
            if (string.IsNullOrWhiteSpace(name as string))
                return "Morate zadati ime.";

            return null;
        }

        private static string ValidateCHSeats(object seatsObject, object _)
        {
            var seats = seatsObject as string;
            if (string.IsNullOrWhiteSpace(seats))
                return "Morate zadati broj stolica.";

            if (!int.TryParse(seats, out var seatsNum))
                return "Broj stolica mora biti broj.";

            else if (seatsNum < 1 || seatsNum > 100)
                return "Broj stolica mora biti između 1 i 100.";

            return null;
        }
        private static string ValidateBudget(object budgetObject, object _)
        {
            var budgetStr = budgetObject as string;
            if (string.IsNullOrWhiteSpace(budgetStr))
                return "Morate zadati budžet.";

            if (!float.TryParse(budgetStr, out var budget))
                return "Budžet mora biti broj.";

            else if (budget <= 0)
                return "Budžet mora biti pozitivan.";

            return null;
        }

        private static string ValidateExpectedNumberOfGuests(object numberObject, object _)
        {
            var numberStr = numberObject as string;
            if (string.IsNullOrWhiteSpace(numberStr))
                return "Morate zadati očekivani broj gostiju.";

            if (!int.TryParse(numberStr, out var number))
                return "Broj gostiju mora biti ceo broj.";

            else if (number <= 0)
                return "Broj gostiju mora biti pozitivan.";

            return null;
        }

        private static string ValidateDateTimeTo(object dateTime1Object, object dateTime2Object)
        {
            try
            {
                var dateTimeFrom = Convert.ToDateTime(dateTime1Object);
                var dateTimeTo = Convert.ToDateTime(dateTime2Object);

                if (dateTimeTo < DateTime.Now.AddDays(2))
                    return "Mora biti najmanje za 48 sati kasnije.";

                else if (dateTimeTo < dateTimeFrom)
                    return "Ne može biti ranije od početka.";
            }
            catch
            {
                return "Nevalidan datum.";   // should not happen
            }
            return null;
        }

        private static string ValidateDateTimeFrom(object dateTime1Object, object dateTime2Object)
        {
            try
            {
                var dateTimeFrom = Convert.ToDateTime(dateTime1Object);
                var dateTimeTo = Convert.ToDateTime(dateTime2Object);

                if (dateTimeFrom < DateTime.Now.AddDays(2))
                    return "Mora biti najmanje za 48 sati kasnije.";

                else if (dateTimeTo < dateTimeFrom)
                    return "Ne može biti kasnije od završetka.";
            }
            catch
            {
                return "Nevalidan datum.";   // should not happen
            }
            return null;
        }

        private static string ValidateCelebrationType(object type, object _)
        {
            if(string.IsNullOrWhiteSpace(type as string))
                return "Morate zadati tip proslave.";

            return null;
        }

        private static string ValidateContent(object content, object _)
        {
            if (string.IsNullOrWhiteSpace(content as string))
                return "Morate zadati detalje.";

            return null;
        }

        private static string ValidateDetailTitle(object title, object _)
        {
            if (string.IsNullOrWhiteSpace(title as string))
                return "Morate zadati naslov zahteva.";

            return null;
        }

        private static string ValidateProposalTitle(object title, object _)
        {
            if (string.IsNullOrWhiteSpace(title as string))
                return "Morate zadati naslov predloga.";

            return null;
        }

    }
}