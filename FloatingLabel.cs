using Godot;

public partial class FloatingLabel : Label
{
	public override void _Ready()
	{
		GetNode<AnimationPlayer>(nameof(AnimationPlayer)).Play("FloatUpward");
	}

	// For animation FloatUpward
	private void Destroy()
		=> this.QueueFree();
}
