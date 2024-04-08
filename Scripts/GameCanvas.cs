using Godot;

public partial class GameCanvas : CanvasLayer
{
	private const float ScaleOnAndroid = 6f;

	public override void _Ready()
	{
		bool isAndroid
			//= true
			= OS.GetName() is "Android"
			;

		if (isAndroid)
			Scale = new Vector2(ScaleOnAndroid, ScaleOnAndroid);
	}
}