using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;

namespace FroggerStarter.Model
{
    public class PowerupManager : IEnumerable<Powerup>
    {
        private const int PercentChanceOfPlaceingPowerup = 30;

        private int xBound;
        private int upperYBound;
        private int lowerYBound;

        private DispatcherTimer placePowerupTimer;
        private List<Powerup> powerups;

        public PowerupManager(int xBound, int lowerYBound, int upperYBound)
        {
            this.xBound = xBound;
            this.upperYBound = upperYBound;
            this.lowerYBound = lowerYBound;
            setupPlacePowerupTimer();
            setupPowerups();
        }

        private void setupPlacePowerupTimer()
        {
            this.placePowerupTimer = new DispatcherTimer();
            this.placePowerupTimer.Tick += this.placePowerupTimerOnTick;
            this.placePowerupTimer.Interval = new TimeSpan(0, 0, 0, 5);
            this.placePowerupTimer.Start();
        }

        private void placePowerupTimerOnTick(object sender, object e)
        {
            Random random = new Random();
            int newRandom = random.Next(1, 100);
            if (newRandom <= PercentChanceOfPlaceingPowerup)
            {
                this.placeRandomPowerup();
            }
        }

        private void setupPowerups()
        {
            this.powerups = new List<Powerup>
            {
                new ExtraTimePowerup()
            };

            foreach (var powerup in this.powerups)
            {
                powerup.Sprite.Visibility = Visibility.Collapsed;
            }
        }

        private void placeRandomPowerup()
        {
            Random random = new Random();
            int randomPowerupIndex = random.Next(0, this.powerups.Count - 1);

            this.powerups[randomPowerupIndex].X = random.Next(0, this.xBound);
            this.powerups[randomPowerupIndex].Y = random.Next(this.upperYBound, this.lowerYBound);
            this.powerups[randomPowerupIndex].Sprite.Visibility = Visibility.Visible;
        }

        /// <summary>Returns an enumerator that iterates through the collection.</summary>
        /// <returns>An enumerator that can be used to iterate through the collection.</returns>
        public IEnumerator<Powerup> GetEnumerator()
        {
            return this.powerups.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}
