using Godot;

public partial class Score : Node
{
	public int Value { get; private set; }

	[Signal] public delegate void ScoreChangedEventHandler(int newValue);

	public void AddScore(ITarget amount)
	{
		Value += amount.ScoreForHit;
		EmitScoreChanged();
	}
	public void ClearScore()
	{
		Value = 0;
		EmitScoreChanged();
	}

	private void SubscribeToTarget(Target spawnedTarget)
		=> spawnedTarget.Connect(Target.SignalName.OnTargetPressed, Callable.From<Target>(AddScore));

	private void EmitScoreChanged()
		=> EmitSignal(SignalName.ScoreChanged, Value);
}