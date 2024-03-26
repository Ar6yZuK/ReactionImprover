using Godot;

public interface ITarget
{
	int ScoreForHit { get; }
}
public partial class Target : BaseButton, ITarget
{
	[Signal] public delegate void OnTargetPressedEventHandler(Target target);

	public virtual int ScoreForHit { get; } = 1;

	public override void _Pressed()
	{
		EmitSignal(SignalName.OnTargetPressed, this);
		this.QueueFree(); // Maybe add flag if FreeOnPressed
	}
}