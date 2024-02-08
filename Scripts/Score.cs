using Godot;

public partial class Score : Node
{
	public int Value { get; private set; }

	[Signal]
	public delegate void ScoreChangedEventHandler(int newValue);

	public override void _Ready()
	{
		Game game = this.GetGame();
		//game.OnStopped += GameStopped;
	}

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

	private void GameStopped()
	{
		// Commented this because on Game.Stop() reloading scene
		// ClearScore();
	}
	private void EmitScoreChanged()
		=> EmitSignal(SignalName.ScoreChanged, Value);
}