using System;
using System.Collections.Generic;
using System.Text;

namespace Organizator_Proslava.Model
{
    public class Celebration : BaseObservableEntity
    {
        private string _type;
        public string Type { get => _type; set => OnPropertyChanged(ref _type, value); }

        private Guid? _clientId;
        public Guid? ClientId { get => _clientId; set => OnPropertyChanged(ref _clientId, value); }

        private Client _client;
        public virtual Client Client { get => _client; set => OnPropertyChanged(ref _client, value); }

        private Guid? _organizerId;
        public Guid? OrganizerId { get => _organizerId; set => OnPropertyChanged(ref _organizerId, value); }

        private Organizer _organizer;
        public virtual Organizer Organizer { get => _organizer; set => OnPropertyChanged(ref _organizer, value); }

        private Guid? _addressId;
        public Guid? AddressId { get => _addressId; set => OnPropertyChanged(ref _addressId, value); }

        private Address _address;
        public virtual Address Address { get => _address; set => OnPropertyChanged(ref _address, value); }

        private DateTime _dateTimeFrom = DateTime.Now.AddDays(2);
        public DateTime DateTimeFrom { get => _dateTimeFrom; set => OnPropertyChanged(ref _dateTimeFrom, value); }

        private DateTime _dateTimeTo = DateTime.Now.AddDays(2);
        public DateTime DateTimeTo { get => _dateTimeTo; set => OnPropertyChanged(ref _dateTimeTo, value); }

        private int _expectedNumberOfGuests;
        public int ExpectedNumberOfGuests { get => _expectedNumberOfGuests; set => OnPropertyChanged(ref _expectedNumberOfGuests, value); }

        private float _budgetFrom;
        public float BudgetFrom { get => _budgetFrom; set => OnPropertyChanged(ref _budgetFrom, value); }

        private float _budgetTo;
        public float BudgetTo { get => _budgetTo; set => OnPropertyChanged(ref _budgetTo, value); }

        private bool _isBudgetFixed;
        public bool IsBudgetFixed { get => _isBudgetFixed; set => OnPropertyChanged(ref _isBudgetFixed, value); }

        private IEnumerable<CelebrationDetail> _celebrationDetails;

        public virtual IEnumerable<CelebrationDetail> CelebrationDetails
        {
            get => _celebrationDetails;
            set => OnPropertyChanged(ref _celebrationDetails, value);
        }

        public override string ToString()
        {
            var builder = new StringBuilder();
            builder.Append($"Ja, {_client?.FullName} želim da organizujem {_type}. ");

            if (Organizer == null)
            {
                builder.Append("Organizator će mi biti naknadno dodeljen. ");
            }
            else
            {
                builder.Append($"Organizator ove proslave je {_organizer?.FullName}. ");
            }

            if (Address == null)
            {
                builder.Append("Želim da mi organizator predloži pogodnu lokaciju. ");
            }
            else
            {
                builder.Append($"Želim da se organizuje na adresi {_address?.WholeAddress}. ");
            }

            builder.Append($"Očekujem {_expectedNumberOfGuests} gostiju. ");

            var isFixedString = _isBudgetFixed ? "jeste" : "nije";
            builder.Append($"Budžet se kreće od {_budgetFrom} do {_budgetTo} i {isFixedString} konačan. ");

            if (_dateTimeFrom.Date == _dateTimeTo.Date)
            {
                builder.Append($"Organizuje se na {_dateTimeTo:d-M-yyyy} od {_dateTimeFrom:H:mm} do {_dateTimeTo:H:mm}. ");
            }
            else
            {
                builder.Append($"Organizuje se od {_dateTimeFrom:d-M-yyyy} u {_dateTimeFrom:H:mm} do {_dateTimeTo:d-M-yyyy} u {_dateTimeTo:H:mm}. ");
            }
            builder.AppendLine("\n");
            builder.AppendLine("Moji zahtevi su: ");
            foreach (var detail in CelebrationDetails)
            {
                builder.AppendLine(detail.ToString());
            }

            return builder.ToString();
        }
    }

    public class CelebrationDetail : BaseObservableEntity
    {
        private Guid _celebrationId;
        public Guid CelebrationId { get => _celebrationId; set => OnPropertyChanged(ref _celebrationId, value); }

        private string _title;
        public string Title { get => _title; set => OnPropertyChanged(ref _title, value); }

        private string _content;
        public string Content { get => _content; set => OnPropertyChanged(ref _content, value); }

        public override string ToString()
        {
            var builder = new StringBuilder();
            builder.AppendLine(_title);
            builder.AppendLine("====================");
            builder.AppendLine(_content);

            return builder.ToString();
        }
    }
}