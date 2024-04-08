using Godot;

public static class RectExtensions
{
	public static Rect2 TransformToLocal(this Transform2D obj1, Rect2 rectToTransform)
		=> new(obj1.BasisXform(rectToTransform.Position), obj1.BasisXform(rectToTransform.Size));
}