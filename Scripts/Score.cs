using Godot;
using ReactionImprover.Scripts.Extensions;

public partial class Score : Node
{
	public int Value { get; private set; }

	[Signal] public delegate void ScoreChangedEventHandler(int newValue);

	public override void _EnterTree()
	{
		this.GetTargetSpawner()
			.ConnectOnTargetPressed(AddScore);
	}

	public void AddScore(ITarget amount)
	{
		Value += amount.ScoreForHit;
		EmitScoreChanged();
	}
	
	private void EmitScoreChanged()
		=> EmitSignal(SignalName.ScoreChanged, Value);
}