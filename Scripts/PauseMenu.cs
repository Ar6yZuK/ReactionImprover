using Godot;

public partial class PauseMenu : Panel
{
	private SettingsMenu _settings;

	#region Drag control
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
	#endregion

	public override void _Ready()
	{
		_settings = GetNode<SettingsMenu>("../Settings");
	}

	private void SetPaused(bool value)
	{
		Visible = value;
		GetTree().Paused = value;
	}

	private void PauseHover(InputEvent _) { if (_ is InputEventMouse) Pause(); }
	private void ResumeHover(InputEvent _) { if (_ is InputEventMouse) Resume(); }

	public void Pause()
	{
		this.SetAnchorsAndOffsetsPreset(LayoutPreset.Center, LayoutPresetMode.KeepSize);
		_settings.SetAnchorsAndOffsetsPreset(LayoutPreset.Center, LayoutPresetMode.KeepSize);
		SetPaused(true);
	}
	public void Resume()
	{
		_settings.Hide();
		SetPaused(false);
	}
	public void Restart()
	{
		Resume();
		GetTree().ReloadCurrentScene();
	}
	public void Settings() => _settings.Visible = !_settings.Visible;
	public void Quit() => GetTree().Quit();
}
