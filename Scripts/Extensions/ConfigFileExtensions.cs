using Godot;
using System;
using System.Linq.Expressions;

namespace ReactionImprover.Scripts.Extensions;
public static class ConfigFileExtensions
{
	public static void SetValueWithSave<T>(this MyConfigFile obj1, Expression<Func<T, Variant>> propertyAccessExpression, Variant value)
	{
		obj1.SetValue(propertyAccessExpression, value);
		obj1.Save()
			 .DebugLogIfError((Error savingError) =>
				{
					string memberName = MyConfigFile.GetMemberName(propertyAccessExpression);
					return $"Saving {obj1._path} for property {memberName} has error: {savingError}";
				});
	}
}
