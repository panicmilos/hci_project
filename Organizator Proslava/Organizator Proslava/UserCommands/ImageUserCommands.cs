using System;
using System.Windows;
using System.Windows.Controls;

namespace Organizator_Proslava.UserCommands
{
    public class AddImage : IUserCommand
    {
        private readonly string _image;
        private Action<string> _addImage;
        private Action<string> _deleteImage;

        public AddImage(string image, Action<string> addImage, Action<string> deleteImage)
        {
            _image = image;
            _addImage = addImage;
            _deleteImage = deleteImage;
        }

        public void Redo()
        {
            _addImage(_image);
        }

        public void Undo()
        {
            _deleteImage(_image);
        }
    }

    public class DeleteImage : IUserCommand
    {
        private readonly IUserCommand _addCommand;

        public DeleteImage(string image, Action<string> addImage, Action<string> deleteImage)
        {
            _addCommand = new AddImage(image, addImage, deleteImage);
        }

        public void Redo()
        {
            _addCommand.Undo();
        }

        public void Undo()
        {
            _addCommand.Redo();
        }
    }

    public class MoveImage : IUserCommand
    {
        private readonly UIElement _image;
        private readonly Point _oldPosition;
        private readonly Point _newPosition;

        public MoveImage(UIElement image, Point oldPosition, Point newPosition)
        {
            _image = image;
            _oldPosition = oldPosition;
            _newPosition = newPosition;
        }

        public void Redo()
        {
            Canvas.SetTop(_image, _newPosition.Y);
            Canvas.SetLeft(_image, _newPosition.X);
        }

        public void Undo()
        {
            Canvas.SetTop(_image, _oldPosition.Y);
            Canvas.SetLeft(_image, _oldPosition.X);
        }
    }
}