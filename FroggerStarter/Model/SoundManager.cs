using System;
using Windows.Media.Core;
using Windows.Media.Playback;

namespace FroggerStarter.Model
{
    /// <summary>Manages playing sound effects</summary>
    public class SoundManager
    {
        private const string VehicleCollisionSoundEffect = "ms-appx:///Resources/CarCrash.mp3";
        private const string HittingWallSoundEffect = "ms-appx:///Resources/HittingWall.wav";
        private const string TimeRunningOutSoundEffect = "ms-appx:///Resources/TimerTick.mp3";
        private const string FilledHomeSoundEffect = "ms-appx:///Resources/FilledHome.wav";
        private const string LevelCompletedSoundEffect = "ms-appx:///Resources/LevelCompleted.wav";
        private const string GameOverSoundEffect = "ms-appx:///Resources/GameOver.wav";
        private const string PowerUpTakenSoundEffect = "ms-appx:///Resources/PowerUpTaken.wav";
        private const string InvincibilityActiveSoundEffect = "ms-appx:///Resources/InvincibilityActive.mp3";
        private const string SplashSoundEffect = "ms-appx:///Resources/Splash.wav";

        /// <summary>Plays the vehicle collision sound.</summary>
        public void PlayVehicleCollisionSound()
        {
            play(VehicleCollisionSoundEffect);
        }

        /// <summary>Plays the hitting wall sound.</summary>
        public void PlayHittingWallSound()
        {
            play(HittingWallSoundEffect);
        }

        /// <summary>Plays the time running out sound.</summary>
        public void PlayTimeRunningOutSound()
        {
            play(TimeRunningOutSoundEffect);
        }

        /// <summary>Plays the filled home sound.</summary>
        public void PlayFilledHomeSound()
        {
            play(FilledHomeSoundEffect);
        }

        /// <summary>Plays the game over sound.</summary>
        public void PlayGameOverSound()
        {
            play(GameOverSoundEffect);
        }

        /// <summary>Plays the level completed sound.</summary>
        public void PlayLevelCompletedSound()
        {
            play(LevelCompletedSoundEffect);
        }

        /// <summary>Plays the power up taken sound.</summary>
        public void PlayPowerUpTakenSound()
        {
            play(PowerUpTakenSoundEffect);
        }

        /// <summary>Plays the invincibility active sound.</summary>
        public void PlayInvincibilityActiveSound()
        {
            play(InvincibilityActiveSoundEffect);
        }

        /// <summary>Players the splash sound.</summary>
        public void PlayerSplashSound()
        {
            play(SplashSoundEffect);
        }

        private static void play(string fileName)
        {
            var mediaPlayer = new MediaPlayer {
                Source = MediaSource.CreateFromUri(new Uri($"{fileName}", UriKind.RelativeOrAbsolute))
            };
            mediaPlayer.Play();
        }
    }
}
