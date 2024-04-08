using Godot;
using System;

namespace ReactionImprover.Scripts.Extensions;
public static class NodeExtensions
{
	public static TargetSpawner GetTargetSpawner(this Node obj1)
		=> (TargetSpawner)obj1.GetTree().Root.FindChild("TargetSpawner", owned: false);

	// Maybe don't throw on connection error. But now after connections no have code.

	/// <summary>
	/// Connect to this in <see cref="Node._EnterTree"/>
	/// </summary>
	public static Error ConnectOnTargetSpawned(this TargetSpawner obj1, Action<Target> actionToConnect)
		=> obj1.Connect(TargetSpawner.SignalName.OnTargetSpawned, Callable.From(actionToConnect))
				.ThrowIfError(error => $"Occur an error when connect on spawned");

	/// <inheritdoc cref="ConnectOnTargetSpawned(TargetSpawner, Action{Target})"/>
	public static Error ConnectOnTargetPressed(this TargetSpawner obj1, Action<Target> actionToConnect)
		=> obj1.ConnectOnTargetSpawned(
			(Target target) =>
			{
				target.Connect(Target.SignalName.OnTargetPressed, Callable.From(actionToConnect))
					   .ThrowIfError(error => $"Occur an error when connect on pressed: {error}");
			});

	public static T? GetParentRecursive<T>(this Node root)
		where T : Node
	{
		var parentNode = root.GetParent();

		if (parentNode is null)
			return null;

		if (parentNode is T parent)
			return parent;

		return parentNode.GetParentRecursive<T>();
	}

}
