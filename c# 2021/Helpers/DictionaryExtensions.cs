namespace c__2021;

public static class DictionaryExtensions
{
    public static TValue AddOrUpdate<TKey, TValue>(this IDictionary<TKey, TValue> dict, TKey key, Func<TValue, TValue> updateFunc, TValue addValue) where TKey : notnull
    {
        if (dict.TryGetValue(key, out var value))
        {
            return dict[key] = updateFunc(value);
        }

        dict.Add(key, addValue);
        return addValue;
    }
}