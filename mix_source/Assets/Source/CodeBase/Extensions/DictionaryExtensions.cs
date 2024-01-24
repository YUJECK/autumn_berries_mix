
using System.Collections.Generic;

namespace deKraken.Extensions
{
    public static class DictionaryExtensions
    {
        public static TValue[] ToValueArray<TKey, TValue>(this Dictionary<TKey, TValue> dictionary)
        {
            List<TValue> valueList = new();

            foreach (var pair in dictionary)
            {
                if (pair.Value != null)
                {
                    valueList.Add(pair.Value);
                }
            }

            return valueList.ToArray();
        }
        public static TKey[] ToKeyArray<TKey, TValue>(this Dictionary<TKey, TValue> dictionary)
        {
            List<TKey> keyList = new();

            foreach (var pair in dictionary)
            {
                if (pair.Key != null)
                {
                    keyList.Add(pair.Key);
                }
            }

            return keyList.ToArray();
        }
    }
}