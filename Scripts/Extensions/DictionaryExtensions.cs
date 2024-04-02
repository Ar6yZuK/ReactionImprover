using System;
using System.Collections.Generic;

namespace ReactionImprover.Scripts.Extensions;
public static class DictionaryExtensions
{
	public static TValue GetOrSet<TKey, TValue>(this Dictionary<TKey, TValue> obj1, TKey key, Func<TValue> resultProvider) where TKey : notnull
		=> obj1.TryGetValue(key, out var result) ? result : obj1[key] = resultProvider();
}