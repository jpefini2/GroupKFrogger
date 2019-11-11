using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Media.Core;
using Windows.Media.Playback;

namespace FroggerStarter.Model
{
    class SoundManager
    {
        private static readonly String vehicleCollisonSoundEffect = "ms-appx:///Resources/CarCrash.mp3";
        private static readonly String hittingWallSoundEffect = "ms-appx:///Resources/HittingWall.wav";
        private static readonly String timeRunningOutSoundEffect = "ms-appx:///Resources/TimerTick.mp3";
        private static readonly String filledHomeSoundEffect = "ms-appx:///Resources/FilledHome.wav";
        private static readonly String gameOverSoundEffect = "ms-appx:///Resources/GameOver.wav";

        public void PlayVehicleCollisionSound()
        {
            this.Play(vehicleCollisonSoundEffect);
        }

        public void PlayHittingWallSound()
        {
            this.Play(hittingWallSoundEffect);
        }

        public void PlayTimeRunningOutSound()
        {
            this.Play(timeRunningOutSoundEffect);
        }

        public void PlayFilledHomeSound()
        {
            this.Play(filledHomeSoundEffect);
        }

        public void PlayGameOverSound()
        {
            this.Play(gameOverSoundEffect);
        }

        private void Play(string fileName)
        {
            var mediaPlayer = new MediaPlayer();
            mediaPlayer.Source = MediaSource.CreateFromUri(new Uri($"{fileName}", UriKind.RelativeOrAbsolute));
            mediaPlayer.Play();
        }
    }
}
