using Windows.UI.Xaml;
using FroggerStarter.View.Sprites;
using System;

namespace FroggerStarter.Model
{
    /// <summary>
    ///     Defines the frog model
    /// </summary>
    /// <seealso cref="FroggerStarter.Model.GameObject" />
    public class Frog : GameObject
    {
        #region Data members

        private const int SpeedXDirection = 50;
        private const int SpeedYDirection = 50;

        private DispatcherTimer animationTimer;

        public BaseSprite[] DeathSprites { get; private set; }

        public bool IsDying { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="Frog" /> class.
        /// </summary>
        public Frog()
        {
            Sprite = new FrogSprite();
            SetSpeed(SpeedXDirection, SpeedYDirection);

            this.setupDeathAnimation();

            this.IsDying = false;
            this.animationTimer = new DispatcherTimer();
            this.animationTimer.Tick += this.switchDeathSprite;
            this.animationTimer.Interval = new TimeSpan(0, 0, 0, 0, 500);
        }

        private void setupDeathAnimation()
        {
            this.DeathSprites = new BaseSprite[4];

            var deathSprite1 = new FrogDeathSprite1();
            deathSprite1.Visibility = Visibility.Collapsed;

            var deathSprite2 = new FrogDeathSprite2();
            deathSprite2.Visibility = Visibility.Collapsed;

            var deathSprite3 = new FrogDeathSprite3();
            deathSprite3.Visibility = Visibility.Collapsed;

            var deathSprite4 = new FrogDeathSprite4();
            deathSprite4.Visibility = Visibility.Collapsed;

            this.DeathSprites[0] = deathSprite1;
            this.DeathSprites[1] = deathSprite2;
            this.DeathSprites[2] = deathSprite3;
            this.DeathSprites[3] = deathSprite4;
        }

        public void PlayDeathAnimation()
        {
            this.IsDying = true;
            this.Sprite.Visibility = Visibility.Collapsed;
            this.DeathSprites[0].Visibility = Visibility.Visible;
            this.animationTimer.Start();
        }

        private void switchDeathSprite(object sender, object e)
        {
            if (this.DeathSprites[0].Visibility == Visibility.Visible)
            {
                this.DeathSprites[0].Visibility = Visibility.Collapsed;
                this.DeathSprites[1].Visibility = Visibility.Visible;
            }
            else if (this.DeathSprites[1].Visibility == Visibility.Visible)
            {
                this.DeathSprites[1].Visibility = Visibility.Collapsed;
                this.DeathSprites[2].Visibility = Visibility.Visible;
            }
            else if (this.DeathSprites[2].Visibility == Visibility.Visible)
            {
                this.DeathSprites[2].Visibility = Visibility.Collapsed;
                this.DeathSprites[3].Visibility = Visibility.Visible;
            }
            else
            {
                this.DeathSprites[3].Visibility = Visibility.Collapsed;
                this.Sprite.Visibility = Visibility.Visible;
                this.animationTimer.Stop();
                this.IsDying = false;
                this.UpdateDeathSpritesLocation();
            }
        }

        public override void MoveRight()
        {
            base.MoveRight();
            this.UpdateDeathSpritesLocation();
        }

        public override void MoveLeft()
        {
            base.MoveLeft();
            this.UpdateDeathSpritesLocation();
        }

        public override void MoveUp()
        {
            base.MoveUp();
            this.UpdateDeathSpritesLocation();
        }

        public override void MoveDown()
        {
            base.MoveDown();
            this.UpdateDeathSpritesLocation();
        }

        public void UpdateDeathSpritesLocation()
        {
            foreach (var deathSprite in this.DeathSprites)
            {
                deathSprite.RenderAt(this.X, this.Y);
            }
        }

        #endregion
    }
}