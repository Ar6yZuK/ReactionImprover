using Godot;
using System.Linq.Expressions;
using System;


public partial class ConfigFile<TSource> : ConfigFile
{
	private readonly string _path;
	private static readonly string _typeNameCached = typeof(TSource).Name;

	public ConfigFile() : this($"{_typeNameCached}.RIsettings") { }
	public ConfigFile(string filePath) => _path = filePath;

	public void SetValue(Expression<Func<TSource, Variant>> propertyAccessExpression, Variant value)
	{
		string memberName = GetMemberName(propertyAccessExpression);

		this.SetValue(_typeNameCached, memberName, value);
	}
	public Variant GetValue(Expression<Func<TSource, Variant>> propertyAccessExpression, Variant @default = default)
	{
		string memberName = GetMemberName(propertyAccessExpression);

		return this.GetValue(_typeNameCached, memberName, @default);
	}
	public void Save() => Save(_path);

	private static string GetMemberName(Expression<Func<TSource, Variant>> expression)
	{
		var expressionBody = expression.Body;

		if (expressionBody is MemberExpression memberExpression)
			return memberExpression.Member.Name;
		
		if (expressionBody is UnaryExpression { Operand: MemberExpression memberExpression2 })
			return memberExpression2.Member.Name;

		throw new InvalidOperationException("Invalid expression. Should have property that returns.");
	}
}