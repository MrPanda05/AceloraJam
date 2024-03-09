using Commons.Components;
using Godot;
using System;


namespace SingleToons
{
    /// <summary>
    /// This singletoon is used for when the objects wants to play a sound, but it will be destroyed
    /// </summary>
    public partial class AudioManager : Node
    {
        public static AudioManager Instance { get; private set; }

        [Export] private SoundPool rocksSounds, playerDeath, MonsterHit
            ;
        [Export] private AudioStreamPlayer healSound;

        public override void _Ready()
        {
            Instance = this;
        }

        public void PlayRocksHit()
        {
            rocksSounds.PlayRandomSound();
        }
        public void PlayDeathSounds()
        {
            playerDeath.PlayRandomSound();
        }
        public void PlayMonsterhitSounds()
        {
            MonsterHit.PlayRandomSound();
        }

        public void PlayHealSound()
        {
            healSound.Play();
        }
    }
}
