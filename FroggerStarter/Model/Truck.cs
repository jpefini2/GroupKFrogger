using FroggerStarter.View.Sprites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FroggerStarter.Model
{
    class Truck : Vehicle
    {

        public Truck(Direction orientation) : base(orientation)
        {
            TruckSprite sprite = new TruckSprite();
            this.RotateSprite(sprite);
            this.Sprite = sprite;
        }
    }
}
