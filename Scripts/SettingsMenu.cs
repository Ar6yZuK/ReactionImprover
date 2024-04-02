using Godot;
using ReactionImprover.Scripts.Extensions;
using System;
using System.Linq.Expressions;

public partial class SettingsMenu : Control
{
	private readonly static Expression<Func<Volumes, Variant>> _masterVolumeProperty = v => v.MasterVolume;
	private readonly static Expression<Func<Volumes, Variant>> _spawnVolumeProperty = v => v.SpawnVolume;
	private readonly static Expression<Func<Volumes, Variant>> _popVolumeProperty = v => v.PopVolume;

	private TargetSpawnedAudioPlayer _spawnAudioPlayer;
	private TargetPressedAudioPlayer _popAudioPlayer;

	private int _master;
	private int _spawn;
	private int _pop;

	private ConfigFile<Volumes> _volumesPreferences;

	#region Drag control
	private bool _lifted;
	// TODO: Maybe make IDraggable for Settings and PauseMenu
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
		_master = AudioServer.GetBusIndex("Master");
		_spawn = AudioServer.GetBusIndex("Spawn");
		_pop = AudioServer.GetBusIndex("Pop");

		_popAudioPlayer = GetNode<TargetPressedAudioPlayer>("/root/GameScene/OnTargetPressedAudioPlayer");
		_spawnAudioPlayer = GetNode<TargetSpawnedAudioPlayer>("/root/GameScene/OnTargetSpawnedAudioPlayer");

		_volumesPreferences = new();

		InstantiateFromConfigFile();

		void InstantiateFromConfigFile()
		{
			SetMasterDB(_volumesPreferences.GetValue(_masterVolumeProperty, 1.03f).AsSingle());
			SetVolumeFor(_pop, _popVolumeProperty);
			SetVolumeFor(_spawn, _spawnVolumeProperty);

			// Set without Play
			void SetVolumeFor(int busIndex, Expression<Func<Volumes, Variant>> propertyAccessExpression)
			{
				float amount = _volumesPreferences.GetValue(propertyAccessExpression, Mathf.DbToLinear(AudioServer.GetBusVolumeDb(busIndex))).AsSingle();
				this.SetVolumeFor(busIndex, propertyAccessExpression, amount);
			}
		}
	}

	private static void Play(AudioStreamPlayer2D audioPlayer) => audioPlayer.Play(0.0f);
	public void PlayPop() => Play(_popAudioPlayer);
	public void PlaySpawn() => Play(_spawnAudioPlayer);

	public void SetMasterDB(float amount)
	{
		SetVolumeFor(_master, v => v.MasterVolume, amount);
	}
	public void SetPopDB(float amount)
	{
		SetVolumeFor(_pop, v => v.PopVolume, amount);
		PlayPop();
	}
	public void SetSpawnDB(float amount)
	{
		SetVolumeFor(_spawn, v => v.SpawnVolume, amount);
		PlaySpawn();
	}

	/// <param name="amount">from 0.0 to 2.0</param>
	/// <param name="amountOnMute">such as -30~ db</param>
	private void SetVolumeFor(int busIndex, Expression<Func<Volumes, Variant>> propertyAccessExpression, float amount, float amountOnMute = 0.03f)
	{
		AudioServer.SetBusMute(busIndex, amount <= amountOnMute);

		AudioServer.SetBusVolumeDb(busIndex, Mathf.LinearToDb(amount));
		_volumesPreferences.SetValueWithSave(propertyAccessExpression, amount);
	}
}
