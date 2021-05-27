using System;

namespace Organizator_Proslava.Model.CelebrationResponses
{
    public class CelebrationProposal : BaseObservableEntity
    {
        private Guid _celebrationReponseId;
        public Guid CelebrationReponseId { get => _celebrationReponseId; set => OnPropertyChanged(ref _celebrationReponseId, value); }

        private CelebrationResponse _celebrationResponse;
        public virtual CelebrationResponse CelebrationResponse { get => _celebrationResponse; set => OnPropertyChanged(ref _celebrationResponse, value); }

        private Guid _celebrationDetailId;
        public Guid CelebrationDetailId { get => _celebrationDetailId; set => OnPropertyChanged(ref _celebrationDetailId, value); }

        private CelebrationDetail _celebrationDetail;
        public virtual CelebrationDetail CelebrationDetail { get => _celebrationDetail; set => OnPropertyChanged(ref _celebrationDetail, value); }

        private string _title;
        public string Title { get => _title; set => OnPropertyChanged(ref _title, value); }

        private string _content;
        public string Content { get => _content; set => OnPropertyChanged(ref _content, value); }
    }
}