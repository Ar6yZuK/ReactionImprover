using Godot;

public partial class TargetSpawnedAudioPlayer : AudioStreamPlayer2D
{
	private void SubscribeToTarget(Target _)
	{
		Play();
	}
}
