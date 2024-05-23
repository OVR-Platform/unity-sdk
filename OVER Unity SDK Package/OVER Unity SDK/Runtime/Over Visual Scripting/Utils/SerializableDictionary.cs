using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OverSDK
{
    [System.Serializable]
    public class SerializableKeyValuePair<TKey, TValue>
    {
        public TKey Key;
        public TValue Value;

        public SerializableKeyValuePair(TKey key, TValue value)
        {
            Key = key;
            Value = value;
        }
    }

    [System.Serializable]
    public class SerializableDictionary<TKey, TValue> : ISerializationCallbackReceiver
    {
        [SerializeField] private List<SerializableKeyValuePair<TKey, TValue>> list = new List<SerializableKeyValuePair<TKey, TValue>>();
        private Dictionary<TKey, TValue> dictionary = new Dictionary<TKey, TValue>();

        public TValue this[TKey key]
        {
            get { return dictionary[key]; }
            set { dictionary[key] = value; }
        }

        public int Count
        {
            get { return dictionary.Count; }
        }

        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
        {
            return dictionary.GetEnumerator();
        }

        public void OnBeforeSerialize()
        {
            list.Clear();
            foreach (var kvp in dictionary)
            {
                list.Add(new SerializableKeyValuePair<TKey, TValue>(kvp.Key, kvp.Value));
            }
        }

        public void OnAfterDeserialize()
        {
            dictionary = new Dictionary<TKey, TValue>();
            foreach (var kvp in list)
            {
                dictionary[kvp.Key] = kvp.Value;
            }
        }

        // Adds an element with the provided key and value to the dictionary.
        public void Add(TKey key, TValue value)
        {
            dictionary.Add(key, value);
        }

        // Removes all keys and values from the dictionary.
        public void Clear()
        {
            dictionary.Clear();
        }

        // Determines whether the dictionary contains the specified key.
        public bool ContainsKey(TKey key)
        {
            return dictionary.ContainsKey(key);
        }

        // Gets the value associated with the specified key.
        public bool TryGetValue(TKey key, out TValue value)
        {
            return dictionary.TryGetValue(key, out value);
        }

        // Removes the value with the specified key from the dictionary.
        public bool Remove(TKey key)
        {
            return dictionary.Remove(key);
        }
    }
}
