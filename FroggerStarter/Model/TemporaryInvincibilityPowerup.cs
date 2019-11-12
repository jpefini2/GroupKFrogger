using FroggerStarter.View.Sprites;

namespace FroggerStarter.Model
{
    /// <summary>Temporary invincibility powerup</summary>
    /// <seealso cref="FroggerStarter.Model.Powerup" />
    public class TemporaryInvincibilityPowerup : Powerup
    {
        /// <summary>Initializes a new instance of the <see cref="TemporaryInvincibilityPowerup"/> class.</summary>
        public TemporaryInvincibilityPowerup()
        {
            Sprite = new TemporaryInvincibilityPowerupSprite();
        }
    }
}
