using Godot;

public partial class PauseMenu : Panel
{
	private bool _lifted;

	public override void _GuiInput(InputEvent @event)
	{
		if (@event is InputEventMouseButton mouseClickEvent)
		{
			_lifted = mouseClickEvent.IsPressed();
			return;
		}
	}

	public override void _Input(InputEvent @event)
	{
		if (_lifted && @event is InputEventMouseMotion mouseDragEvent)
			Position += mouseDragEvent.Relative;
	}

	private void SetPaused(bool value)
	{
		Visible = value;
		GetTree().Paused = value;
	}

	private void PauseHover(InputEvent _)
	{
		if (_ is InputEventMouse) Pause();
	}

	private void ResumeHover(InputEvent _)
	{
		if (_ is InputEventMouse) Resume();
	}

	public void Pause()
	{
		this.SetAnchorsAndOffsetsPreset(LayoutPreset.Center, LayoutPresetMode.KeepSize);
		SetPaused(true);
	}
	public void Resume() => SetPaused(false);
	public void Restart()
	{
		Resume();
		GetTree().ReloadCurrentScene();
	}

	public void Quit() => GetTree().Quit();
}
