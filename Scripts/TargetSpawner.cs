using Godot;
public interface ITargetSpawner
{
	Target Spawn();
}
public partial class TargetSpawner : Node2D, ITargetSpawner
{
	[Signal] public delegate void OnTargetSpawnedEventHandler(Target target);

	[Export] private PackedScene _targetPrefab;

	[Export] public Node2D ParentForSpawned { get; set; }

	public Target Spawn()
	{
		var target = _targetPrefab.Instantiate<Target>();

		// Target emits OnTargetSpawned when _Ready
		ParentForSpawned.AddChild(target);
		return target;
	}
}
