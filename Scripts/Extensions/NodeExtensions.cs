using Godot;

public static class NodeExtensions
{
	public static Game GetGame(this Node obj1)
		=> obj1.GetNode<Game>("/root/Game");
}