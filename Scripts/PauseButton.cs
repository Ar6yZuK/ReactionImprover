using Godot;

public partial class PauseButton : Button
{
	private Game _game = null!;

	public override void _Ready()
		=> _game = this.GetGame();
	public override void _Pressed()
		=> _game.Stop();
}