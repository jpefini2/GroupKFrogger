using FroggerStarter.View.Sprites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FroggerStarter.Model
{
    class TurboCar : Car
    {
        public TurboCar(Direction orientation, int speed) : base(orientation, speed * 2)
        {
            var sprite = new TurboCarSprite();
            this.RotateSprite(sprite);
            this.Sprite = sprite;
        }
    }
}
