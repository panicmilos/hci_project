using Organizator_Proslava.Utility;
using System;

namespace Organizator_Proslava.Model
{
    public class BaseObservableEntity : ObservableEntity, IBaseEntity
    {
        private Guid _id;
        public Guid Id { get => _id; set => OnPropertyChanged(ref _id, value); }

        private DateTime _createdAt = DateTime.Now;
        public DateTime CreatedAt { get => _createdAt; set => OnPropertyChanged(ref _createdAt, value); }

        private bool _isActive = true;
        public bool IsActive { get => _isActive; set => OnPropertyChanged(ref _isActive, value); }
    }
}