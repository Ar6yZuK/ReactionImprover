using Godot;

public partial class Game : Node2D
{
	public bool IsRunning { get; private set; }

	[Signal] public delegate void OnStoppedEventHandler();
	[Signal] public delegate void OnStartEventHandler();

	public virtual void Stop()
	{
		IsRunning = false;
		// Todo: Remove reload scene from this. Add menu with buttons for user
		GetTree().ReloadCurrentScene();
		EmitSignal(SignalName.OnStopped);
	}
	public virtual void Start()
	{
		IsRunning = true;
		EmitSignal(SignalName.OnStart);
	}
}