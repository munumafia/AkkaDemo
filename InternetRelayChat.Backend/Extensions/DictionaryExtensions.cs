using System.Collections.Generic;

namespace InternetRelayChat.Backend.Extensions
{
    /// <summary>
    /// Extension methods for working with dictionaries
    /// </summary>
    public static class DictionaryExtensions
    {
        /// <summary>
        /// Attempts to retrieve the value for a key, returning the default value of entries in the dictionary 
        /// if a value for the specified key doesn't exist in the dictionary
        /// </summary>
        /// <typeparam name="TKey">The key type of the dictionary</typeparam>
        /// <typeparam name="TValue">The value type of the dictionary</typeparam>
        /// <param name="dict">The dictionary to call the extension method on</param>
        /// <param name="key">The key to retrieve the value of</param>
        /// <returns>The value of the key or default(TValue) if the key doesn't exist</returns>
        public static TValue TryGetValue<TKey, TValue>(this IDictionary<TKey, TValue> dict, TKey key)
        {
            return !dict.ContainsKey(key) ? default(TValue) : dict[key];
        }
    }
}
