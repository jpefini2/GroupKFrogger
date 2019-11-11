using FroggerStarter.View.Sprites;

namespace FroggerStarter.Model
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="FroggerStarter.Model.Powerup" />
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
