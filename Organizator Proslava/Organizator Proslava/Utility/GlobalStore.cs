using System.Collections.Generic;

namespace Organizator_Proslava.Utility
{
    public static class GlobalStore
    {
        private static Dictionary<string, object> _storedObjects;

        static GlobalStore()
        {
            _storedObjects = new Dictionary<string, object>();
        }

        public static void AddObject(string key, object @object)
        {
            _storedObjects.Add(key, @object);
        }

        public static T ReadObject<T>(string key)
        {
            return (T)_storedObjects[key];
        }

        public static void RemoveObject(string key)
        {
            _storedObjects.Remove(key);
        }
    }
}