﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FroggerStarter.View.Sprites;

namespace FroggerStarter.Model
{
    class Car : Vehicle
    {

        public Car(Direction orientation) : base(orientation)
        {
            CarSprite sprite = new CarSprite();
            this.RotateSprite(sprite);
            this.Sprite = sprite;
        }
    }
}