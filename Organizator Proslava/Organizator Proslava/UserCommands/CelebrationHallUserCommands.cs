using Organizator_Proslava.Model.CelebrationHalls;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Organizator_Proslava.UserCommands
{
    public class AddTable : IUserCommand
    {
        private readonly Action _undo;
        private readonly Action _redo;

        public AddTable(Action undo, Action redo)
        {
            _undo = undo;
            _redo = redo;
        }

        public void Redo()
        {
            _redo?.Invoke();
        }

        public void Undo()
        {
            _undo?.Invoke();
        }
    }

    public class RemoveTable : IUserCommand
    {
        private readonly IUserCommand _addCommand;

        public RemoveTable(Action undo, Action redo)
        {
            _addCommand = new AddTable(undo, redo);
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

    public class AddCelebrationHall : IUserCommand
    {
        private readonly CelebrationHall _celebrationHall;
        private readonly ObservableCollection<CelebrationHall> _halls;
        private readonly List<CelebrationHall> _celebrationHalls;

        public AddCelebrationHall(CelebrationHall celebrationHall, ObservableCollection<CelebrationHall> halls, List<CelebrationHall> celebrationHalls)
        {
            _celebrationHall = celebrationHall;
            _halls = halls;
            _celebrationHalls = celebrationHalls;
        }

        public void Redo()
        {
            _halls.Add(_celebrationHall);
            _celebrationHalls.Add(_celebrationHall);
        }

        public void Undo()
        {
            _halls.Remove(_celebrationHall);
            _celebrationHalls.Remove(_celebrationHall);
        }
    }

    public class UpdateCelebrationHall : IUserCommand
    {
        private readonly CelebrationHall _celebrationHall;
        private readonly CelebrationHall _oldCelebrationHall;
        private readonly CelebrationHall _newCelebrationHall;

        public UpdateCelebrationHall(CelebrationHall celebrationHall, CelebrationHall oldCelebrationHall, CelebrationHall newCelebrationHall)
        {
            _celebrationHall = celebrationHall;
            _oldCelebrationHall = oldCelebrationHall;
            _newCelebrationHall = newCelebrationHall;
        }

        public void Redo()
        {
            _celebrationHall.Name = _newCelebrationHall.Name;
            _celebrationHall.PlaceableEntities = _newCelebrationHall.PlaceableEntities;
        }

        public void Undo()
        {
            _celebrationHall.Name = _oldCelebrationHall.Name;
            _celebrationHall.PlaceableEntities = _oldCelebrationHall.PlaceableEntities;
        }
    }

    public class RemoveCelebrationHall : IUserCommand
    {
        private readonly IUserCommand _addCommand;

        public RemoveCelebrationHall(CelebrationHall celebrationHall, ObservableCollection<CelebrationHall> halls, List<CelebrationHall> celebrationHalls)
        {
            _addCommand = new AddCelebrationHall(celebrationHall, halls, celebrationHalls);
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
}