using Godot;
using System;

namespace ReactionImprover.Scripts.Extensions;
public static class ErrorExtensions
{
	/// <returns><paramref name="error"/></returns>
	public static Error ThrowIfError(this Error error, Func<Error, string>? message = null)
		=> error is not Error.Ok ? throw new Exception(GetMessage(error, message)) : error;
	/// <returns><paramref name="error"/></returns>
	public static Error DebugLogIfError(this Error error, Func<Error, string>? message = null)
	{
		if (error is not Error.Ok)
			GD.Print(GetMessage(error, message));

		return error;
	}

	private static string GetMessage(in Error error, Func<Error, string>? messageProvider)
		=> messageProvider is not null ? messageProvider(error) : error.ToString();
}