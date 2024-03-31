using Godot;

public partial class FreeAfterTime : Node
{
	[Export] private double DieAfterSeconds { get; set; }
	public override async void _Ready()
	{
		await ToSignal(GetTree().CreateTimer(DieAfterSeconds), SceneTreeTimer.SignalName.Timeout);
		QueueFree();
	}
}