using System.Collections.Generic;
using UnityEngine;

namespace Bitools
{
    public class BoolDictionary
    {
        Dictionary<string, bool> dict = new Dictionary<string, bool>();

        public bool AllValueisFalse()
        {
            if (dict.Count == 0) return false;
            foreach (bool value in dict.Values)
            {
                if (value) return false;
            }
            return true;
        }
        public bool AllValueisFalse(bool nullValue)
        {
            if (dict.Count == 0) return nullValue;
            foreach (bool value in dict.Values)
            {
                if (value) return false;
            }
            return true;
        }

        public bool AllValueisTrue()
        {
            if (dict.Count == 0) return false;
            foreach (bool value in dict.Values)
            {
                if (!value) return false;
            }
            return true;
        }
        public bool AllValueisTrue(bool nullValue)
        {
            if (dict.Count == 0) return nullValue;
            foreach (bool value in dict.Values)
            {
                if (!value) return false;
            }
            return true;
        }

        public void Add(string key, bool value)
        {
            if (dict.ContainsKey(key))
            {
                Debug.LogError(key + " has already included.");
                return;
            }
            dict.Add(key, value);
        }

        public void Set(string key, bool value)
        {
            if (!dict.ContainsKey(key))
            {
                dict.Add(key, value);
                return;
            }
            dict[key] = value;
        }
    }

}
