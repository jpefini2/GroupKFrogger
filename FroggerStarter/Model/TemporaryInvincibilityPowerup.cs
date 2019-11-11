using FroggerStarter.View.Sprites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FroggerStarter.Model
{
    class TemporaryInvincibilityPowerup : Powerup
    {
        public TemporaryInvincibilityPowerup()
        {
            this.Sprite = new TemporaryInvincibilityPowerupSprite();
        }
    }
}
