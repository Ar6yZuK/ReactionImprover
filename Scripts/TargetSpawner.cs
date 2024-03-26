using Godot;
public interface ITargetSpawner
{
	Target Spawn();
}
public partial class TargetSpawner : Node2D, ITargetSpawner
{
	[Signal] public delegate void OnTargetSpawnedEventHandler(Target target);
	[Export] private PackedScene _targetPrefab = null!;

	public virtual Target Spawn()
	{
		var target = _targetPrefab.Instantiate<Target>();
		AddChild(target);
		EmitSignal(SignalName.OnTargetSpawned, target);
		return target;
	}
}
