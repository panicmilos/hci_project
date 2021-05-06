using System;
using System.Collections.Generic;

namespace Organizator_Proslava.Utility
{
    public static class EventBus
    {
        private static readonly IDictionary<string, IList<Action>> _eventHandlers;

        static EventBus()
        {
            _eventHandlers = new Dictionary<string, IList<Action>>();
        }

        public static void RegisterHandler(string eventName, Action handler)
        {
            if (!_eventHandlers.TryGetValue(eventName, out IList<Action> handlers))
            {
                _eventHandlers[eventName] = new List<Action>();
                handlers = _eventHandlers[eventName];
            }
            handlers.Add(handler);
        }

        public static void FireEvent(string eventName)
        {
            foreach (var handler in _eventHandlers[eventName])
            {
                handler.Invoke();
            }
        }
    }
}