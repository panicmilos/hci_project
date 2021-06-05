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
                Id = new Guid("08d92296-c0b7-40e9-8f6f-901a58b4d887"),
                Role = Role.User
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