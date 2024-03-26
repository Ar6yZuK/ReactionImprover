using Godot;

[Tool]
public partial class RandomPositionTargetSpawner : TargetSpawner
{
	private readonly RandomNumberGenerator _rand = new();

	private bool _autoPosition = true;
	private Rect2 _rect;

	[Export] bool AutoPosition
	{
		get => _autoPosition;
		set
		{
			if(_autoPosition = value) Rect = _rect;
		}
	}

	[Export] Rect2 Rect
	{ 
		get => _rect;
		set 
		{
			// Centering from the current position
			if(AutoPosition)
				value.Position = Vector2.Zero - value.Size / 2;

			_rect = value;
			QueueRedraw();
		}
	}
	
	public override void _Draw()
	{
		DrawRect(Rect, Colors.Black, false);
	}

	public override Target Spawn()
	{
		var target = base.Spawn();

		// Rect with { Size } to spawn so that the texture of target does not go beyond the rect
		Vector2 randomPosition = _rand.GetRandomVector(Rect with { Size = Rect.Size - target.Size });
		target.Position = randomPosition;

		return target;
	}

	protected override void Dispose(bool disposing)
	{
		if (!disposing)
			return;

		_rand.Dispose();
	}
}