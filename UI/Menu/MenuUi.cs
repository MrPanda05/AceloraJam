using Godot;
using System;


namespace UIgame
{
	public partial class MenuUi : Control
	{
		[Export] public Control Settings, Buttons, TittleScreen;

		[Export] public HSlider master, music, soundfx;

        [Export] public AudioStreamPlayer clickSound;

        public bool isOnTittleScreen = true;

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

		public void OnMasterValueChanged(float value)
		{
            AudioServer.SetBusVolumeDb(0, ScaleDecibels(value));
        }
        public void OnSoundFxValueChanged(float value)
        {
            AudioServer.SetBusVolumeDb(2, ScaleDecibels(value));
        }
        public void OnMusicValueChanged(float value)
        {
            AudioServer.SetBusVolumeDb(1, ScaleDecibels(value));
        }

        public override void _Ready()
        {
            AudioServer.SetBusVolumeDb(0, ScaleDecibels(25));
            AudioServer.SetBusVolumeDb(2, ScaleDecibels(25));
            AudioServer.SetBusVolumeDb(1, ScaleDecibels(25));
        }
    }
}
