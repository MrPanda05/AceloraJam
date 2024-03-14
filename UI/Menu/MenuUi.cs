using Godot;
using SingleToons;
using System;


namespace UIgame
{
	public partial class MenuUi : Control
	{
		[Export] public Control Settings, Buttons, TittleScreen;

		[Export] public HSlider master, music, soundfx;

        [Export] public AudioStreamPlayer clickSound;

        public bool isOnTittleScreen = true;

        public static Action OnMyUpdate;

		public async void OnReturnButtonDown()
		{
			await ToSignal(GetTree().CreateTimer(0.1f), SceneTreeTimer.SignalName.Timeout);
            GetTree().Paused = false;
			Visible = false;
            clickSound.Play();

        }
        public void OnSettingsButtonTittleDown()
        {
            Settings.Visible = true;
            TittleScreen.Visible = false;
            clickSound.Play();

        }


        public void OnQuitButtonDown()
		{
            clickSound.Play();

            GetTree().Quit();
		}
		public void OnSettingsButtonDown()
		{
			Settings.Visible = true;
			Buttons.Visible = false;
            clickSound.Play();


        }
        public void OnStartButtonDown()
        {
            isOnTittleScreen = false;
            Visible = false;
            TittleScreen.Visible = false;
            GetTree().Paused = false;
            clickSound.Play();

        }

        public void OnReturnBustButtonDown()
        {
            if (isOnTittleScreen)
            {
                Settings.Visible = false;
                TittleScreen.Visible = true;
                clickSound.Play();

                return;
            }
            Buttons.Visible = true;
            Settings.Visible = false;
            clickSound.Play();

        }
        public float ScaleDecibels(float value)
        {
            float scale = 20.0f;
            float divisor = 50.0f;
            return scale * (float)Math.Log10(value / divisor);
        }
        public void MakeSure()
        {
            master.Value = (double)SaveSystem.GetValue("masterVolume");
            soundfx.Value = (double)SaveSystem.GetValue("soundFXVolume");
            music.Value = (double)SaveSystem.GetValue("musicVolume");
        }
		public void OnMasterValueChanged(float value)
		{
            SaveSystem.Update("masterVolume", Mathf.RoundToInt(value));
            AudioServer.SetBusVolumeDb(0, ScaleDecibels(value));
            OnMyUpdate?.Invoke();
        }
        public void OnSoundFxValueChanged(float value)
        {
            SaveSystem.Update("soundFXVolume", Mathf.RoundToInt(value));
            AudioServer.SetBusVolumeDb(2, ScaleDecibels(value));
            OnMyUpdate?.Invoke();
        }
        public void OnMusicValueChanged(float value)
        {
            SaveSystem.Update("musicVolume", Mathf.RoundToInt(value));
            AudioServer.SetBusVolumeDb(1, ScaleDecibels(value));
            OnMyUpdate?.Invoke();
        }

        public override void _Ready()
        {
            SaveSystem.Add("masterVolume", 25);
            SaveSystem.Add("musicVolume", 25);
            SaveSystem.Add("soundFXVolume", 25);
            master.Value = (double)SaveSystem.GetValue("masterVolume");
            soundfx.Value = (double)SaveSystem.GetValue("soundFXVolume");
            music.Value = (double)SaveSystem.GetValue("musicVolume");
            OnMyUpdate += MakeSure;
        }
        public override void _ExitTree()
        {
            OnMyUpdate -= MakeSure;
        }
    }
}
