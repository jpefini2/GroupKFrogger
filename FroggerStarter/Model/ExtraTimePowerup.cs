using FroggerStarter.View.Sprites;

namespace FroggerStarter.Model
{
    public class ExtraTimePowerup : Powerup
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ExtraTimePowerup"/> class.
        /// </summary>
        public ExtraTimePowerup()
        {
            Sprite = new ExtraTimePowerupSprite();
        }
    }
}
