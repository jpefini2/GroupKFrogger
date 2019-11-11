using FroggerStarter.View.Sprites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FroggerStarter.Model
{
    class ExtraTimePowerup : Powerup
    {
        public ExtraTimePowerup()
        {
            this.Sprite = new ExtraTimePowerupSprite();
        }
    }
}
