using Godot;

public partial class TargetPressedAudioPlayer : AudioStreamPlayer2D
{
	private void SubscribeToTarget(Target spawnedTarget)
	{
		spawnedTarget.Connect(Target.SignalName.OnTargetPressed, Callable.From<Target>(delegate { Play(); }));
	}
}
