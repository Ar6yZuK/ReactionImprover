using Godot;
using System;

// TODO: Maybe delete ITarget because godot not support interfaces
public interface ITarget
{
	int ScoreForHit { get; }
	TimeSpan TimeOfLife { get; }
}
public partial class Target : BaseButton, ITarget
{
	[Signal] public delegate void OnTargetPressedEventHandler(Target target);

	private DateTime _creationTime;

	public TimeSpan TimeOfLife => DateTime.Now - _creationTime;
	public virtual int ScoreForHit { get; } = 1;

	public override void _Ready()
	{
		_creationTime = DateTime.Now;
	}

	public override void _Pressed()
	{
		EmitSignal(SignalName.OnTargetPressed, this);
		this.QueueFree(); // Maybe add flag if FreeOnPressed
	}
}