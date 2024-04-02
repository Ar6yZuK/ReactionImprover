using Godot;
using System;
using System.Linq.Expressions;

namespace ReactionImprover.Scripts.Extensions;
public static class ConfigFileExtensions
{
	public static void SetValueWithSave<T>(this ConfigFile<T> obj1, Expression<Func<T, Variant>> propertyAccessExpression, Variant value)
	{
		obj1.SetValue(propertyAccessExpression, value);
		obj1.Save();
	}
}
