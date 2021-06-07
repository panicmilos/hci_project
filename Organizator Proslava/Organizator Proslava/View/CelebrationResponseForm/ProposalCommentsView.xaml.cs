using System.Windows;
using System.Windows.Controls;

namespace Organizator_Proslava.View.CelebrationResponseForm
{
    /// <summary>
    /// Interaction logic for ProposalCommentsView.xaml
    /// </summary>
    public partial class ProposalCommentsView : UserControl
    {
        public ProposalCommentsView()
        {
            InitializeComponent();
            scroll.ScrollToBottom();
        }

        private void CommentClick(object sender, RoutedEventArgs e)
        {
            newcomment.Text = null;
            scroll.ScrollToBottom();
        }
    }
}
