using Godot;
using ReactionImprover.Scripts.Extensions;
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

	private TimeSpan? _timeOfLifeFrozen;
	/// <summary>
	/// On pressed it will be freeze
	/// </summary>
	[System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE0030", Justification = "I think this code is more optimized")]
	public TimeSpan TimeOfLife => _timeOfLifeFrozen.HasValue ? _timeOfLifeFrozen.Value : DateTime.Now - _creationTime;
	public virtual int ScoreForHit { get; } = 1;

	public override void _Ready()
	{
		this.GetTargetSpawner()
			.EmitSignal(TargetSpawner.SignalName.OnTargetSpawned, this);

		_creationTime = DateTime.Now;
	}

	public override void _Pressed()
	{
		_timeOfLifeFrozen = TimeOfLife;
		EmitSignal(SignalName.OnTargetPressed, this);
		this.QueueFree(); // Maybe add flag if FreeOnPressed
	}
}