﻿using Organizator_Proslava.Model.CelebrationHalls;
using System.Collections.Generic;

namespace Organizator_Proslava.Model.Collaborators
{
    public class Collaborator : BaseUser
    {
        private string _phoneNumber;
        public string PhoneNumber { get => _phoneNumber; set => OnPropertyChanged(ref _phoneNumber, value); }

        private Address _address;
        public virtual Address Address { get => _address; set => OnPropertyChanged(ref _address, value); }

        private CollaboratorServiceBook _collaboratorServiceBook;
        public virtual CollaboratorServiceBook CollaboratorServiceBook { get => _collaboratorServiceBook; set => OnPropertyChanged(ref _collaboratorServiceBook, value); }

        private List<CelebrationHall> _celebrationHalls;
        public virtual List<CelebrationHall> CelebrationHalls { get { return _celebrationHalls; } set { _celebrationHalls = value; OnPropertyChanged("CelebrationHalls"); } }

        private List<string> _images;
        public virtual List<string> Images { get { return _images; } set { _images = value; OnPropertyChanged("Images"); } }
    }
}