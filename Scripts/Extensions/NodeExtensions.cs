using Godot;

public static class NodeExtensions
{
	public static Game GetGame(this Node obj1)
		=> obj1.GetNode<Game>("/root/Game");
	public static Score GetScore(this Node obj1)
		=> obj1.GetNode<Score>("/root/Scene/Score");
}