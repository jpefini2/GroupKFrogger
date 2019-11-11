using FroggerStarter.View.Sprites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FroggerStarter.Model
{
    public class Turtles : Vehicle
    {
        public Turtles(Direction orientation, int speed) : base(orientation, speed)
        {
            TurtleSprite sprite = new TurtleSprite();
            this.RotateSprite(sprite);
            this.Sprite = sprite;
        }
    }
}
