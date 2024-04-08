using Godot;
using ReactionImprover.Scripts.Extensions;

public partial class RandomPositionTargetSpawner : Node2D, ITargetSpawner
{
	private RectDrawer _drawer;
	private RandomNumberGenerator _rand = null!;

	private TargetSpawner _targetSpawner;

	public override void _Ready()
	{
		_drawer = GetNode<RectDrawer>("CanvasLayer/Drawer");
		_rand = new();
		_targetSpawner = this.GetTargetSpawner();
	}

	public Target Spawn()
	{
		var target = _targetSpawner.Spawn();
		Transform2D transformator = _targetSpawner.ParentForSpawned.GetCanvasTransform().AffineInverse();
		Rect2 rect = _drawer.GetRect();
		var localizedRect = transformator.TransformToLocal(rect);
		// Rect with { Size } to spawn so that the texture of target does not go beyond the rect
		localizedRect = Slice(localizedRect, target.Size);
		Vector2 randomPosition = _rand.GetRandomVector(localizedRect);
		target.Position = randomPosition;

		return target;

		static Rect2 Slice(Rect2 rect, Vector2 size)
			=> rect with { Size = rect.Size - size };
	}

	protected override void Dispose(bool disposing)
	{
		if (!disposing)
			return;

		_rand?.Dispose();
	}
}
