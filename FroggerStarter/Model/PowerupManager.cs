using System;
using System.Collections;
using System.Collections.Generic;
using Windows.UI.Xaml;

namespace FroggerStarter.Model
{
    public class PowerupManager : IEnumerable<Powerup>
    {
        private const int PercentChanceOfPlacingPowerup = 100;

        private readonly int xBound;
        private readonly int upperYBound;
        private readonly int lowerYBound;
        private DispatcherTimer placePowerupTimer;
        private List<Powerup> powerups;

        public bool IsInvincibilityActive { get; private set; }
        private DispatcherTimer invincibilityTimer;


        public PowerupManager(int xBound, int lowerYBound, int upperYBound)
        {
            this.xBound = xBound;
            this.upperYBound = upperYBound;
            this.lowerYBound = lowerYBound;
            this.IsInvincibilityActive = false;
            this.setupPlacePowerupTimer();
            this.setupPowerups();
            this.setupInvincibilityTimer();
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
            var random = new Random();
            var newRandom = random.Next(1, 100);
            if (newRandom <= PercentChanceOfPlacingPowerup)
            {
                this.placeRandomPowerup();
            }
        }

        private void setupPowerups()
        {
            this.powerups = new List<Powerup>
            {
                new TemporaryInvincibilityPowerup(),
                new ExtraTimePowerup()
            };

            foreach (var powerup in this.powerups)
            {
                powerup.Sprite.Visibility = Visibility.Collapsed;
            }
        }

        private void setupInvincibilityTimer()
        {
            this.invincibilityTimer = new DispatcherTimer();
            this.invincibilityTimer.Tick += this.endInvincibility;
            this.invincibilityTimer.Interval = new TimeSpan(0, 0, 0, 3);
            this.invincibilityTimer.Start();
        }

        private void endInvincibility(object sender, object e)
        {
            this.IsInvincibilityActive = false;
        }

        private void placeRandomPowerup()
        {
            var random = new Random();
            var randomPowerupIndex = random.Next(0, this.powerups.Count);

            this.powerups[randomPowerupIndex].X = random.Next(0, this.xBound);
            this.powerups[randomPowerupIndex].Y = random.Next(this.upperYBound, this.lowerYBound);
            this.powerups[randomPowerupIndex].Sprite.Visibility = Visibility.Visible;
        }

        public void PickedUp(Powerup powerup)
        {
            powerup.Sprite.Visibility = Visibility.Collapsed;

            if (powerup is TemporaryInvincibilityPowerup)
            {
                this.IsInvincibilityActive = true;
                this.invincibilityTimer.Start();
            }
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
