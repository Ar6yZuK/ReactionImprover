using Godot;
using ReactionImprover.Scripts.Extensions;

public partial class TargetSpawnedAudioPlayer : AudioStreamPlayer2D
{
	public override void _EnterTree()
	{
		this.GetTargetSpawner()
			.ConnectOnTargetSpawned(target => Play());
	}
}
