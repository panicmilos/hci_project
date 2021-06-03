using Organizator_Proslava.Dialogs.Service;
using Organizator_Proslava.Model.CelebrationHalls;
using Organizator_Proslava.Utility;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing.Printing;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Organizator_Proslava.Dialogs.Custom.Collaborators
{
    internal class BorderTextBoxDTO
    {
        public TextBox TextBox { get; set; }
        public Border Border { get; set; }
    }

    /// <summary>
    /// Interaction logic for PlacingGuestsDialogView.xaml
    /// </summary>
    public partial class PlacingGuestsDialogView : UserControl
    {
        public readonly ICommand AddGuest;

        private PlacingGuestsDialogViewModel PlacingGuestsViewModel { get => DataContext as PlacingGuestsDialogViewModel; }

        private readonly IList<Border> _borders;
        private readonly IDictionary<Border, TextBox> _bordersWithTextBoxes;

        public PlacingGuestsDialogView()
        {
            InitializeComponent();

            AddTableImage();

            _borders = new List<Border>();
            _bordersWithTextBoxes = new Dictionary<Border, TextBox>();

            foreach (var guest in GlobalStore.ReadAndRemoveObject<List<Guest>>("guests"))
            {
                var borderTextBox = AddBorderToCanvas(guest.Name);
                AddBindings(guest, borderTextBox);
            }

            EventBus.RegisterHandler("AddNewGuest", Guest_Click);
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

        private void Guest_Click()
        {
            var borderTextBox = AddBorderToCanvas("Ime gosta");
            var guest = new Guest
            {
                Name = "Ime gosta",
                PositionX = Canvas.GetLeft(borderTextBox.Border),
                PositionY = Canvas.GetTop(borderTextBox.Border)
            };

            AddBindings(guest, borderTextBox);
            PlacingGuestsViewModel.Add.Execute(guest);
        }

        private void AddBindings(Guest guest, BorderTextBoxDTO borderTextBox)
        {
            Binding text = new Binding
            {
                Source = guest,
                Path = new PropertyPath("Name"),
                Mode = BindingMode.TwoWay
            };
            borderTextBox.TextBox.SetBinding(TextBox.TextProperty, text);

            Binding positionX = new Binding
            {
                Source = guest,
                Path = new PropertyPath("PositionX"),
                Mode = BindingMode.TwoWay
            };
            borderTextBox.Border.SetBinding(Canvas.LeftProperty, positionX);

            Binding positionY = new Binding
            {
                Source = guest,
                Path = new PropertyPath("PositionY"),
                Mode = BindingMode.TwoWay
            };
            borderTextBox.Border.SetBinding(Canvas.TopProperty, positionY);
        }

        private BorderTextBoxDTO AddBorderToCanvas(string text)
        {
            var textBox = new TextBox
            {
                Text = text,
                Style = (Style)Application.Current.Resources["guestNameTextBox"],
            };
            textBox.GotFocus += TextBox_GotFocus;
            textBox.LostFocus += TextBox_LostFocus;

            var border = new Border
            {
                BorderBrush = new SolidColorBrush(Color.FromRgb(54, 116, 123)),
                BorderThickness = new Thickness(3),
                Cursor = Cursors.Hand
            };
            border.Child = textBox;

            border.PreviewMouseLeftButtonDown += Border_PreviewMouseLeftButtonDown;
            AddContextMenu(border);

            Canvas.SetLeft(border, (SecondCanvas.ActualWidth / 2) - (border.ActualWidth / 2));
            Canvas.SetTop(border, (SecondCanvas.ActualHeight / 2) - (border.ActualHeight / 2));

            _borders.Add(border);
            _bordersWithTextBoxes.Add(border, textBox);
            SecondCanvas.Children.Add(border);

            return new BorderTextBoxDTO
            {
                TextBox = textBox,
                Border = border
            };
        }

        private void TextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            var textBox = sender as TextBox;
            if (textBox.Text == String.Empty)
            {
                textBox.Text = "Ime gosta";
            }
        }

        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            var textBox = sender as TextBox;
            if (textBox.Text == "Ime gosta")
            {
                textBox.Text = String.Empty;
            }
        }

        private void AddContextMenu(Border border)
        {
            var contextMenu = new ContextMenu();
            var deleteMenuItem = new MenuItem()
            {
                Header = "Ukloni",
            };

            deleteMenuItem.Click += (object deleteSender, RoutedEventArgs deleteE) =>
            {
                var indexOfBorder = _borders.IndexOf(border);
                _borders.RemoveAt(indexOfBorder);
                _bordersWithTextBoxes.Remove(border);
                PlacingGuestsViewModel.Remove.Execute(indexOfBorder);
                SecondCanvas.Children.Remove(border);
            };

            contextMenu.Items.Add(deleteMenuItem);
            border.ContextMenu = contextMenu;
        }

        #region DragAndDrop

        private UIElement dragObject = null;
        private Point offset;

        private void Border_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.OriginalSource.ToString() == "System.Windows.Controls.TextBoxView")
            {
                return;
            }

            dragObject = sender as UIElement;
            offset = e.GetPosition(SecondCanvas);
            offset.Y -= Canvas.GetTop(dragObject);
            offset.X -= Canvas.GetLeft(dragObject);
        }

        private void SecondCanvas_PreviewMouseMove(object sender, MouseEventArgs e)
        {
            if (dragObject == null)
            {
                return;
            }

            var position = e.GetPosition(sender as IInputElement);
            Canvas.SetTop(dragObject, position.Y - offset.Y);
            Canvas.SetLeft(dragObject, position.X - offset.X);
        }

        private void SecondCanvas_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            dragObject = null;
        }

        #endregion DragAndDrop
    }
}