using Windows.UI.Xaml;
using FroggerStarter.View.Sprites;

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

        public BaseSprite[] DeathSprites { get; private set; }

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

        public override void MoveRight()
        {
            base.MoveRight();
            this.updateDeathSpritesLocation();
        }

        public override void MoveLeft()
        {
            base.MoveLeft();
            this.updateDeathSpritesLocation();
        }

        public override void MoveUp()
        {
            base.MoveUp();
            this.updateDeathSpritesLocation();
        }

        public override void MoveDown()
        {
            base.MoveDown();
            this.updateDeathSpritesLocation();
        }

        private void updateDeathSpritesLocation()
        {
            foreach (var deathSprite in this.DeathSprites)
            {
                deathSprite.RenderAt(this.X, this.Y);
            }
        }

        #endregion
    }
}