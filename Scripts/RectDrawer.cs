using Godot;

[Tool]
public partial class RectDrawer : Control
{
	public override void _Ready()
	{
		Connect(SignalName.ItemRectChanged, Callable.From(QueueRedraw));
	}
	public override void _Draw()
	{
		DrawRect(GetRect() with { Position = Vector2.Zero }, Colors.Black, filled:false);
	}
}