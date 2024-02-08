using Godot;
using System.Collections.Generic;

public partial class ScoreLabel : Label
{
	private static readonly Dictionary<long, string> _scoreTextCache = new();

	private void OnScoreChanged(long newValue)
	{
		if (!_scoreTextCache.TryGetValue(newValue, out string? value))
			_scoreTextCache.Add(newValue, value = $"Score: {newValue}");

		Text = value;
	}
}