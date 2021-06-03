using Organizator_Proslava.Model;
using System;
using System.Collections.Generic;

namespace Organizator_Proslava.Utility
{
    public static class GlobalStore
    {
        private static Dictionary<string, object> _storedObjects;

        static GlobalStore()
        {
            _storedObjects = new Dictionary<string, object>();

            _storedObjects["loggedUser"] = new BaseUser
            {
                FirstName = "Milos",
                LastName = "Panic",
                Id = new Guid("08d91f80-1277-4f3a-87ec-6c54321bfcb1")
            };
        }

        public static void AddObject(string key, object @object)
        {
            if (_storedObjects.ContainsKey(key))
            {
                _storedObjects[key] = @object;
                return;
            } //Check if this is neccessery;

            _storedObjects.Add(key, @object);
        }

        public static T ReadObject<T>(string key)
        {
            return (T)_storedObjects[key];
        }

        public static T ReadAndRemoveObject<T>(string key)
        {
            T storedObject = ReadObject<T>(key);
            RemoveObject(key);

            return storedObject;
        }

        public static void RemoveObject(string key)
        {
            _storedObjects.Remove(key);
        }
    }
}