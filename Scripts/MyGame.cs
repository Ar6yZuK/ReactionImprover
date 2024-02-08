using Godot;

partial class MyGame : Game
{
	public override void Stop()
	{
		GD.Print("Stop on MyGame");
		base.Stop();
	}
}