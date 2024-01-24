using System.Collections.Generic;

namespace Source.CodeBase
{
    public static class GameDatabase
    {
        private static Dictionary<string, object> _database;

        public static bool Push(string key, object data)
        {
            return _database.TryAdd(key, data);
        }

        public static bool Update(string key, object data)
        {
            if (_database.ContainsKey(key))
            {
                _database[key] = data;
                return true;
            }
            
            return false;
        }
        
        public static bool Remove(string key)
        {
            if (_database.ContainsKey(key))
            {
                _database.Remove(key);
                return true;
            }
            
            return false;
        }
    }
}