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

            /*_storedObjects["loggedUser"] = new BaseUser
            {
                FirstName = "Milos",
                LastName = "Panic",
                Id = new Guid("e0c1f7b7-1d73-43fd-98cc-c904295afb62")
            };*/
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