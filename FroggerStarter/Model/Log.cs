using FroggerStarter.View.Sprites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FroggerStarter.Model
{
    public class Log : Vehicle
    {
        public Log(Direction orientation, int speed) : base(orientation, speed)
        {
            LogSprite sprite = new LogSprite();
            this.RotateSprite(sprite);
            this.Sprite = sprite;
        }
    }
}
