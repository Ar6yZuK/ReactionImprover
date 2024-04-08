using Godot;

public partial class ScoreLabel : Label
{
	private void OnScoreChanged(long newValue)
	{
		Text = newValue.ToString();
	}
}