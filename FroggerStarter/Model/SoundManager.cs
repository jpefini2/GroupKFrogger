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
        private static readonly String vehicleCollisonSoundEffect = "ms-appx:///Resources/Car-crash-sound-effect.mp3";
        private static readonly String hittingWallSoundEffect = "ms-appx:///Resources/Realistic_Punch-Mark_DiAngelo-1609462330.wav";
        private static readonly String timeRunningOutSoundEffect = "ms-appx:///Resources/Tick-DeepFrozenApps-397275646.mp3";

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

        private void Play(string fileName)
        {
            var mediaPlayer = new MediaPlayer();
            mediaPlayer.Source = MediaSource.CreateFromUri(new Uri($"{fileName}", UriKind.RelativeOrAbsolute));
            mediaPlayer.Play();
        }
    }
}
