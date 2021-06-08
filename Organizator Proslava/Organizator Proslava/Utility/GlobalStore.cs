using Organizator_Proslava.Model;
using Organizator_Proslava.UserCommands;
using System;
using System.Collections.Generic;

namespace Organizator_Proslava.Utility
{
    public static class GlobalStore
    {
        private static readonly Dictionary<string, object> _storedObjects;

        static GlobalStore()
        {
            _storedObjects = new Dictionary<string, object>
            {
                ["userCommands"] = new UserCommandManager(),
            };

            // User
            _storedObjects["loggedUser"] = new BaseUser
            {
                FirstName = "Albert",
                LastName = "Makan",
                Id = new Guid("08d92296-c0b7-40e9-8f6f-901a58b4d887"),
                Role = Role.User
            };

            //Organizer
            //_storedObjects["loggedUser"] = new BaseUser
            //{
            //    FirstName = "Jovana",
            //    LastName = "Jovanovic",
            //    Id = new Guid("08d91f80-1277-4f3a-87ec-6c54321bfcb1"),
            //    Role = Role.Organizer
            //};
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