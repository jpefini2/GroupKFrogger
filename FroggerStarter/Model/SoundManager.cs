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

        public void PlayVehicleCollisionSound()
        {
            this.Play(vehicleCollisonSoundEffect);
        }

        private void Play(string fileName)
        {
            var mediaPlayer = new MediaPlayer();
            mediaPlayer.Source = MediaSource.CreateFromUri(new Uri($"{fileName}", UriKind.RelativeOrAbsolute));
            mediaPlayer.Play();
        }
    }
}
