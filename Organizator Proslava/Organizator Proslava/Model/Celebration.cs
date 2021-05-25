using System;

namespace Organizator_Proslava.Model
{
    public class Celebration : BaseObservableEntity
    {
        private string _type;
        public string Type { get => _type; set => OnPropertyChanged(ref _type, value); }
        
        private Organizer _organizer;
        public Organizer Organizer { get => _organizer; set => OnPropertyChanged(ref _organizer, value); }
        
        private Address _address;
        public Address Address { get => _address; set => OnPropertyChanged(ref _address, value); }

        private DateTime _dateTimeFrom = DateTime.Now;
        public DateTime DateTimeFrom { get => _dateTimeFrom; set => OnPropertyChanged(ref _dateTimeFrom, value); }

        private DateTime _dateTimeTo = DateTime.Now;
        public DateTime DateTimeTo { get => _dateTimeTo; set => OnPropertyChanged(ref _dateTimeTo, value); }

        private int _expectedNumberOfGuests;
        public int ExpectedNumberOfGuests { get => _expectedNumberOfGuests; set => OnPropertyChanged(ref _expectedNumberOfGuests, value); }

        private float _budgetFrom;
        public float BudgetFrom { get => _budgetFrom; set => OnPropertyChanged(ref _budgetFrom, value); }
        
        private float _budgetTo;
        public float BudgetTo { get => _budgetTo; set => OnPropertyChanged(ref _budgetTo, value); }

        private bool _isBudgetFixed;
        public bool IsBudgetFixed { get => _isBudgetFixed; set => OnPropertyChanged(ref _isBudgetFixed, value); }
    }

    public class CelebrationDetail : BaseObservableEntity
    {
        private Celebration _celebration;
        public Celebration Celebration { get => _celebration; set => OnPropertyChanged(ref _celebration, value); }

        private string _title;
        public string Title { get => _title; set => OnPropertyChanged(ref _title, value); }
        
        private string _content;
        public string Content { get => _content; set => OnPropertyChanged(ref _content, value); }
    }
}