using Godot;
using ReactionImprover.Scripts.Extensions;
using System.Collections.Generic;
using System.Linq;

public partial class AverageTimeCounter : Node
{
	[Signal] public delegate void AverageTimeChangedEventHandler(double averageTimeS);

	public readonly List<double> _clickTimes = [];
	public override void _EnterTree()
	{
		this.GetTargetSpawner()
			.ConnectOnTargetPressed(
				(Target t) => this.EmitSignal(SignalName.AverageTimeChanged, AddAndCalculateAverage(t)));
	}
	private double AddAndCalculateAverage(Target target)
	{
		_clickTimes.Add(target.TimeOfLife.TotalSeconds);

		return _clickTimes.Average();
	}
}