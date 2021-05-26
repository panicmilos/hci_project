using System;
using System.Collections.Generic;

namespace Organizator_Proslava.Utility
{
    public static class EventBus
    {
        private static readonly IDictionary<string, IList<Action>> _eventHandlers;
        private static readonly IDictionary<string, IList<Action<object>>> _eventHandlersWithParameter;

        static EventBus()
        {
            _eventHandlers = new Dictionary<string, IList<Action>>();
            _eventHandlersWithParameter = new Dictionary<string, IList<Action<object>>>();
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
            if (!_eventHandlers.ContainsKey(eventName)) return;

            foreach (var handler in _eventHandlers[eventName])
            {
                handler.Invoke();
            }
        }

        public static void RegisterHandler(string eventName, Action<object> handler)
        {
            if (!_eventHandlersWithParameter.TryGetValue(eventName, out IList<Action<object>> handlers))
            {
                _eventHandlersWithParameter[eventName] = new List<Action<object>>();
                handlers = _eventHandlersWithParameter[eventName];
            }
            handlers.Add(handler);
        }

        public static void FireEvent(string eventName, object parameter)
        {
            if (!_eventHandlersWithParameter.ContainsKey(eventName)) return;

            foreach (var handler in _eventHandlersWithParameter[eventName])
            {
                handler.Invoke(parameter);
            }
        }
    }
}