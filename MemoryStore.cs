using System;
using System.Collections.Generic;

namespace CybersecurityChatbotGUI
{
    public class MemoryStore
    {
        private readonly Dictionary<string, string> _memory;

        public string UserName { get; set; }
        public string FavouriteTopic { get; set; }

        public MemoryStore()    
        {
            _memory = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
            UserName = string.Empty;
            FavouriteTopic = string.Empty;
        }

        public void Store(string key, string value)
        {
            if (string.IsNullOrWhiteSpace(key))
                return;

            _memory[key] = value;

            if (key.Equals("name", StringComparison.OrdinalIgnoreCase))
            {
                UserName = value;
            }
            else if (key.Equals("topic", StringComparison.OrdinalIgnoreCase) ||
                     key.Equals("favouritetopic", StringComparison.OrdinalIgnoreCase))
            {
                FavouriteTopic = value;
            }
        }

        public string Recall(string key)
        {
            if (string.IsNullOrWhiteSpace(key))
                return null;

            _memory.TryGetValue(key, out string value);
            return value;
        }

        public bool HasKey(string key)
        {
            return _memory.ContainsKey(key);
        }

        public string GetPersonalisedOpener()
        {
            if (!string.IsNullOrWhiteSpace(UserName) && !string.IsNullOrWhiteSpace(FavouriteTopic))
            {
                return $"By the way {UserName}, since you're interested in {FavouriteTopic}, ";
            }
            else if (!string.IsNullOrWhiteSpace(UserName))
            {
                return $"Thanks for chatting with me, {UserName}! ";
            }
            else if (!string.IsNullOrWhiteSpace(FavouriteTopic))
            {
                return $"Since you care about {FavouriteTopic}, ";
            }
            else
            {
                return string.Empty;
            }
        }

        public void ClearAll()
        {
            _memory.Clear();
            UserName = string.Empty;
            FavouriteTopic = string.Empty;
        }
    }
}