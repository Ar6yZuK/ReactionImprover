using Godot;

[Tool]
public partial class RandomPositionTargetSpawner : TargetSpawner
{
	private readonly RandomNumberGenerator _rand = new();

	private Rect2 _rect;
	private bool _autoPosition = true;

	[Export] bool AutoPosition
	{
		get => _autoPosition;
		set
		{
			if(_autoPosition = value)
#pragma warning disable CA2245
				Rect = Rect;
#pragma warning restore CA2245
		}
	}

	[Export] Rect2 Rect 
	{ 
		get => _rect;
		set 
		{
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