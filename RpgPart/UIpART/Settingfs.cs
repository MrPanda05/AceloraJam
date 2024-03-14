using Godot;
using SingleToons;
using System;

namespace RpgPart.UI 
{ 
	public partial class Settingfs : Control
	{
        [Export] public HSlider master, music, soundfx;
        [Export] public Control toBeEnable;

        public static Action OnMyUpdate;

        public float ScaleDecibels(float value)
        {
            float scale = 20.0f;
            float divisor = 50.0f;
            return scale * (float)Math.Log10(value / divisor);
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
        public void MakeSure()
        {
            master.Value = (double)SaveSystem.GetValue("masterVolume");
            soundfx.Value = (double)SaveSystem.GetValue("soundFXVolume");
            music.Value = (double)SaveSystem.GetValue("musicVolume");
        }
        public void OnMasterVolumeValueChanged(float value)
		{
            SaveSystem.Update("masterVolume", Mathf.RoundToInt(value));
            AudioServer.SetBusVolumeDb(0, ScaleDecibels(value));
            OnMyUpdate?.Invoke();
        }
        public void OnSoundFxVolumeValueChanged(float value)
        {
            SaveSystem.Update("soundFXVolume", Mathf.RoundToInt(value));
            AudioServer.SetBusVolumeDb(2, ScaleDecibels(value));
            OnMyUpdate?.Invoke();
        }
        public void OnMusicVolumeValueChanged(float value)
        {
            SaveSystem.Update("musicVolume", Mathf.RoundToInt(value));
            AudioServer.SetBusVolumeDb(1, ScaleDecibels(value));
            OnMyUpdate?.Invoke();
        }
        public void OnReturnButtonButtonDown()
        {
            toBeEnable.Visible = true;
            Visible = false;
        }
    }
}
