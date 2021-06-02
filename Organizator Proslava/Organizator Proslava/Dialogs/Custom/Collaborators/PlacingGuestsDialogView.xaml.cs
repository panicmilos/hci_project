using Organizator_Proslava.Model.CelebrationHalls;
using Organizator_Proslava.Utility;
using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace Organizator_Proslava.Dialogs.Custom.Collaborators
{
    /// <summary>
    /// Interaction logic for PlacingGuestsDialogView.xaml
    /// </summary>
    public partial class PlacingGuestsDialogView : UserControl
    {
        private PlacingGuestsDialogViewModel PlacingGuestsViewModel { get => DataContext as PlacingGuestsDialogViewModel; }
        private readonly IList<TextBox> _textBoxes;

        private IDictionary<UIElement, Guest> _textBoxesWithGuests;

        public PlacingGuestsDialogView()
        {
            InitializeComponent();

            AddTableImage();

            _textBoxes = new List<TextBox>();
            _textBoxesWithGuests = new Dictionary<UIElement, Guest>();

            foreach (var guest in GlobalStore.ReadObject<List<Guest>>("guests"))
            {
                var textBox = AddTextBoxToCanvas(guest.Name);
                AddBindings(guest, textBox);
                _textBoxesWithGuests.Add(textBox, guest);
            }
            GlobalStore.RemoveObject("guests");
        }

        private void AddTableImage()
        {
            var imageName = GlobalStore.ReadAndRemoveObject<string>("tableImageName");
            var image = new Image
            {
                Source = new BitmapImage(new Uri($"pack://siteoforigin:,,,/Resources/{imageName}")),
                Margin = imageName == "6people.png" ? new Thickness(65, 55, 0, 0) : new Thickness(40, 30, 0, 0)
            };

            SecondCanvas.Children.Add(image);
        }

        public void Guest_Click(object sender, RoutedEventArgs e)
        {
            var textBox = AddTextBoxToCanvas("Ime Gosta");
            var guest = new Guest
            {
                Name = "Ime Gosta",
                PositionX = Canvas.GetLeft(textBox),
                PositionY = Canvas.GetTop(textBox)
            };

            AddBindings(guest, textBox);
            _textBoxesWithGuests.Add(textBox, guest);
            PlacingGuestsViewModel.Add.Execute(guest);
        }

        private void AddBindings(Guest guest, TextBox textBox)
        {
            Binding text = new Binding
            {
                Source = guest,
                Path = new PropertyPath("Name"),
                Mode = BindingMode.TwoWay
            };
            textBox.SetBinding(TextBox.TextProperty, text);

            Binding positionX = new Binding
            {
                Source = guest,
                Path = new PropertyPath("PositionX"),
                Mode = BindingMode.TwoWay
            };
            textBox.SetBinding(Canvas.LeftProperty, positionX);

            Binding positionY = new Binding
            {
                Source = guest,
                Path = new PropertyPath("PositionY"),
                Mode = BindingMode.TwoWay
            };
            textBox.SetBinding(Canvas.TopProperty, positionY);
        }

        private TextBox AddTextBoxToCanvas(string text)
        {
            var textBox = new TextBox
            {
                Text = text,
                Width = 80,
                TextWrapping = TextWrapping.Wrap
            };

            textBox.PreviewMouseLeftButtonDown += TextBox_PreviewMouseLeftButtonDown;

            Canvas.SetLeft(textBox, (SecondCanvas.ActualWidth / 2) - (textBox.ActualWidth / 2));
            Canvas.SetTop(textBox, (SecondCanvas.ActualHeight / 2) - (textBox.ActualHeight / 2));

            _textBoxes.Add(textBox);
            AddContextMenu(textBox);
            SecondCanvas.Children.Add(textBox);

            return textBox;
        }

        private void AddContextMenu(TextBox textBox)
        {
            var contextMenu = new ContextMenu();
            var deleteMenuItem = new MenuItem()
            {
                Header = "Ukloni",
            };

            deleteMenuItem.Click += (object deleteSender, RoutedEventArgs deleteE) =>
            {
                var index = _textBoxes.IndexOf(textBox);
                _textBoxes.RemoveAt(index);
                PlacingGuestsViewModel.Remove.Execute(index);
                _textBoxesWithGuests.Remove(textBox);
                SecondCanvas.Children.Remove(textBox);
            };

            contextMenu.Items.Add(deleteMenuItem);
            textBox.ContextMenu = contextMenu;
        }

        #region DragAndDrop

        private UIElement dragObject = null;
        private Point offset;

        private void TextBox_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            dragObject = sender as UIElement;
            offset = e.GetPosition(SecondCanvas);
            offset.Y -= Canvas.GetTop(dragObject);
            offset.X -= Canvas.GetLeft(dragObject);
        }

        private void MainCanvas_PreviewMouseMove(object sender, MouseEventArgs e)
        {
            if (dragObject == null)
            {
                return;
            }

            var position = e.GetPosition(sender as IInputElement);
            Canvas.SetTop(dragObject, position.Y - offset.Y);
            Canvas.SetLeft(dragObject, position.X - offset.X);
        }

        private void MainCanvas_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            dragObject = null;
        }

        #endregion DragAndDrop
    }
}