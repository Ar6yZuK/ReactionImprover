using Godot;
using System.Linq.Expressions;
using System;
using System.Collections.Generic;
using ReactionImprover.Scripts.Extensions;

public partial class MyConfigFile(string filePath) : ConfigFile
{
	public MyConfigFile() : this("user://.RIsettings") { }

	private readonly Dictionary<object, string> _cachedTypeNames = [];
	internal readonly string _path = filePath;

	public void SetValue<TSource>(Expression<Func<TSource, Variant>> propertyAccessExpression, Variant value)
	{
		string memberName = GetMemberName(propertyAccessExpression);

		this.SetValue(GetSectionName(propertyAccessExpression), memberName, value);
	}

	public Variant GetValue<TSource>(Expression<Func<TSource, Variant>> propertyAccessExpression, Variant @default = default)
	{
		string memberName = GetMemberName(propertyAccessExpression);

		return this.GetValue(GetSectionName(propertyAccessExpression), memberName, @default);
	}
	public Error Load() => Load(_path);
	public Error Save() => Save(_path);

	private string GetSectionName<TSource>(Expression<Func<TSource, Variant>> propertyAccessExpression)
		=> _cachedTypeNames.GetOrSet(propertyAccessExpression, () => typeof(TSource).Name);

	internal static string GetMemberName<TSource>(Expression<Func<TSource, Variant>> expression)
	{
		var expressionBody = expression.Body;

		if (expressionBody is MemberExpression memberExpression)
			return memberExpression.Member.Name;
		
		if (expressionBody is UnaryExpression { Operand: MemberExpression memberExpression2 })
			return memberExpression2.Member.Name;

		throw new InvalidOperationException("Invalid expression. Should have property that returns.");
	}
}