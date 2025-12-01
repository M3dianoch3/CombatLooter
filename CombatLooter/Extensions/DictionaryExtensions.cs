using System.Numerics;

namespace CombatLooter.Extensions
{
    public static class DictionaryExtensions
    {
        /// <summary>
        /// Adds a key-value pair to the dictionary. If the key already exists, adds the new value to the existing value instead of overwriting it.
        /// </summary>
        /// <typeparam name="TKey">The type of the dictionary key</typeparam>
        /// <param name="dictionary">The dictionary to add to</param>
        /// <param name="key">The key to add or update</param>
        /// <param name="value">The value to add</param>
        public static void AddOrSum<TKey>(this Dictionary<TKey, double> dictionary, TKey key, double value) where TKey : notnull
        {
            if (dictionary.ContainsKey(key))
            {
                dictionary[key] += value;
            }
            else
            {
                dictionary.Add(key, value);
            }
        }

        /// <summary>
        /// Adds a key-value pair to the dictionary. If the key already exists, adds the new value to the existing value instead of overwriting it.
        /// Works with any numeric type that implements addition.
        /// </summary>
        /// <typeparam name="TKey">The type of the dictionary key</typeparam>
        /// <typeparam name="TValue">The numeric type of the dictionary value</typeparam>
        /// <param name="dictionary">The dictionary to add to</param>
        /// <param name="key">The key to add or update</param>
        /// <param name="value">The value to add</param>
        public static void AddOrSum<TKey, TValue>(this Dictionary<TKey, TValue> dictionary, TKey key, TValue value)
            where TKey : notnull
            where TValue : INumber<TValue>
        {
            if (dictionary.ContainsKey(key))
            {
                dictionary[key] += value;
            }
            else
            {
                dictionary.Add(key, value);
            }
        }
    }
}