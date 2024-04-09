using Godot;

public partial class AverageTimeLabel : Label
{
	private void OnAverageTimeChanged(float averageTimeS)
	{
		this.Text = averageTimeS.ToString("0.###");
	}
}
