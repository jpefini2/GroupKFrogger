using FroggerStarter.View.Sprites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FroggerStarter.Model
{
    /// <summary>Temporary invincibility powerup</summary>
    /// <seealso cref="FroggerStarter.Model.Powerup" />
    public class TemporaryInvincibilityPowerup : Powerup
    {
        /// <summary>Initializes a new instance of the <see cref="TemporaryInvincibilityPowerup"/> class.</summary>
        public TemporaryInvincibilityPowerup()
        {
            this.Sprite = new TemporaryInvincibilityPowerupSprite();
        }
    }
}
