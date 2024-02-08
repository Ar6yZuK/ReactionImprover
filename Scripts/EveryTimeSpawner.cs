using Godot;

public partial class EveryTimeSpawner : ITargetSpawner
{
	[Export] ITargetSpawner _spawner = null!;

	public Target Spawn()
	{
		return _spawner.Spawn();
	}
}
