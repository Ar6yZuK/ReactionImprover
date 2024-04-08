using Godot;
using ReactionImprover.Scripts.Extensions;

public partial class TargetPressedAudioPlayer : AudioStreamPlayer2D
{
	public override void _EnterTree()
	{
		this.GetTargetSpawner()
			.ConnectOnTargetPressed(target => Play());
	}
}
