using System;
using Windows.Media.Core;
using Windows.Media.Playback;

namespace FroggerStarter.Model
{
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
        private const string TimeOutSoundEffect = "ms-appx:///Resources/TimeOut.mp3";

        public void PlayVehicleCollisionSound()
        {
            play(VehicleCollisionSoundEffect);
        }

        public void PlayHittingWallSound()
        {
            play(HittingWallSoundEffect);
        }

        public void PlayTimeRunningOutSound()
        {
            play(TimeRunningOutSoundEffect);
        }

        public void PlayFilledHomeSound()
        {
            play(FilledHomeSoundEffect);
        }

        public void PlayGameOverSound()
        {
            play(GameOverSoundEffect);
        }

        public void PlayLevelCompletedSound()
        {
            play(LevelCompletedSoundEffect);
        }

        public void PlayPowerUpTakenSound()
        {
            play(PowerUpTakenSoundEffect);
        }

        public void PlayInvincibilityActiveSound()
        {
            play(InvincibilityActiveSoundEffect);
        }

        public void PlayTimeOutSound()
        {
            play(TimeOutSoundEffect);
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
