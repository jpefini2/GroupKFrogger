using FroggerStarter.View.Sprites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FroggerStarter.Model
{
    class TurboCar : Vehicle
    {
        public TurboCar(Direction orientation) : base(orientation)
        {
            var sprite = new TurboCarSprite();
            this.RotateSprite(sprite);
            this.Sprite = sprite;
        }
    }
}
