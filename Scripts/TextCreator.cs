using Godot;
using ReactionImprover.Scripts.Extensions;
using System;
using System.Collections.Generic;

public partial class TextCreator : Node2D
{
	private static readonly TimeSpan OneMinute = TimeSpan.FromMinutes(1d);
	private static readonly TimeSpan OneSecond = TimeSpan.FromSeconds(1d);

	[Export] private PackedScene _labelPrefab = null!;

	private CanvasLayer _canvas = null!;

	public override void _Ready()
	{
		_canvas = this.GetParentRecursive<CanvasLayer>();
	}
	public override void _EnterTree()
	{
		this.GetTargetSpawner()
			.ConnectOnTargetPressed(CreateTextOnCanvas);
	}

	// Pool. FloatingLabel is frees when float up, but parent not frees
	private readonly List<Node2D> _parents = [];
	public void CreateTextOnCanvas(Target target)
	{
		// TODO: Make label pool
		var label = _labelPrefab.Instantiate<Label>();
		var (text, color) = CreateText(target.TimeOfLife);
		label.Text = text;
		// Colorize text
		label.Set("theme_override_colors/font_color", color);

		// Create parent node to animation be relative
		Node2D? parent = GetParent(out bool containsOnCanvas);

		parent.Position = target.GlobalPosition;
		parent.AddChild(label);

		// Add to canvas
		if (!containsOnCanvas)
			_canvas.AddChild(parent);

		static (string, Color) CreateText(in TimeSpan time)
		{
			if (time < OneSecond)
				return ($"0.{time.Milliseconds}s :)", Colors.Green);

			if (time < OneMinute)
				return ($"{time.Seconds}.{time.Milliseconds}s", Colors.White);

			// Over a minute
			return ($"{time.Minutes}:{time.Seconds} :(", Colors.Red);
		}
		Node2D GetParent(out bool containsOnCanvas)
		{
			var parent = _parents.Find(x => x.GetChildCount() == 0);
			containsOnCanvas = parent is not null;
			if (!containsOnCanvas)
				_parents.Add(parent = new Node2D());

			return parent!;
		}
	}
}