using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Media.Core;
using Windows.Media.Playback;

namespace FroggerStarter.Model
{
    /// <summary>Manages playing sound effects</summary>
    public class SoundManager
    {
        private static readonly String vehicleCollisonSoundEffect = "ms-appx:///Resources/CarCrash.mp3";
        private static readonly String hittingWallSoundEffect = "ms-appx:///Resources/HittingWall.wav";
        private static readonly String timeRunningOutSoundEffect = "ms-appx:///Resources/TimerTick.mp3";
        private static readonly String filledHomeSoundEffect = "ms-appx:///Resources/FilledHome.wav";
        private static readonly String levelCompletedSoundEffect = "ms-appx:///Resources/LevelCompleted.wav";
        private static readonly String gameOverSoundEffect = "ms-appx:///Resources/GameOver.wav";
        private static readonly String powerUpTakenSoundEffect = "ms-appx:///Resources/PowerUpTaken.wav";
        private static readonly String invincibilityActiveSoundEffect = "ms-appx:///Resources/InvincibilityActive.mp3";
        private static readonly String splashSoundEffect = "ms-appx:///Resources/Splash.wav";

        /// <summary>Plays the vehicle collision sound.</summary>
        public void PlayVehicleCollisionSound()
        {
            this.Play(vehicleCollisonSoundEffect);
        }

        /// <summary>Plays the hitting wall sound.</summary>
        public void PlayHittingWallSound()
        {
            this.Play(hittingWallSoundEffect);
        }

        /// <summary>Plays the time running out sound.</summary>
        public void PlayTimeRunningOutSound()
        {
            this.Play(timeRunningOutSoundEffect);
        }

        /// <summary>Plays the filled home sound.</summary>
        public void PlayFilledHomeSound()
        {
            this.Play(filledHomeSoundEffect);
        }

        /// <summary>Plays the game over sound.</summary>
        public void PlayGameOverSound()
        {
            this.Play(gameOverSoundEffect);
        }

        /// <summary>Plays the level completed sound.</summary>
        public void PlayLevelCompletedSound()
        {
            this.Play(levelCompletedSoundEffect);
        }

        /// <summary>Plays the power up taken sound.</summary>
        public void PlayPowerUpTakenSound()
        {
            this.Play(powerUpTakenSoundEffect);
        }

        /// <summary>Plays the invincibility active sound.</summary>
        public void PlayInvincibilityActiveSound()
        {
            this.Play(invincibilityActiveSoundEffect);
        }

        /// <summary>Players the splash sound.</summary>
        public void PlayerSplashSound()
        {
            this.Play(splashSoundEffect);
        }

        private void Play(string fileName)
        {
            var mediaPlayer = new MediaPlayer();
            mediaPlayer.Source = MediaSource.CreateFromUri(new Uri($"{fileName}", UriKind.RelativeOrAbsolute));
            mediaPlayer.Play();
        }
    }
}
