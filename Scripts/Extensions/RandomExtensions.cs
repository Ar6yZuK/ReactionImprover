using Godot;

public static class RandomExtensions
{
	public static Vector2 GetRandomVector(this RandomNumberGenerator rand, Rect2 rect)
		=> new(rand.RandfRange(rect.Position.X, rect.End.X), rand.RandfRange(rect.Position.Y, rect.End.Y));
}