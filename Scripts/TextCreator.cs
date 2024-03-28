using Godot;
using System;

public partial class TextCreator : Node2D
{
	private static readonly TimeSpan OneMinute = TimeSpan.FromMinutes(1d);
	private static readonly TimeSpan OneSecond = TimeSpan.FromSeconds(1d);

	[Export] private PackedScene _labelPrefab = null!;

	private CanvasLayer _canvas = null!;

	public override void _Ready()
	{
		_canvas = GetNode<CanvasLayer>("/root/GameScene/CanvasLayer");
	}

	public void CreateTextOnCanvas(Target target) // Why i cant use ITarget target :C
	{
		// TODO: Make label pool
		var label = _labelPrefab.Instantiate<Label>();
		label.Text = CreateText(target.TimeOfLife);

		// Create parent node to animation be relative
		var parent = new Node2D { Position = target.GlobalPosition };
		parent.AddChild(label);

		// Add to canvas
		_canvas.AddChild(parent);

		static string CreateText(in TimeSpan time)
		{
			if (time < OneSecond)
				return $"0.{time.Milliseconds}S :)";

			if (time < OneMinute)
				return $"{time.Seconds}.{time.Milliseconds}S";

			// Over a minute
			return $"{time.Minutes}:{time.Seconds} :(";
		}
	}

	private void SubscribeToTargetPressed(Target target)
	{
		target.Connect(Target.SignalName.OnTargetPressed, Callable.From<Target>(CreateTextOnCanvas));
	}
}