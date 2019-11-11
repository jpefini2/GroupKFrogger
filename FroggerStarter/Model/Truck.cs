using FroggerStarter.View.Sprites;

namespace FroggerStarter.Model
{
    public class Truck : Vehicle
    {

        public Truck(Direction orientation, int speed) : base(orientation, speed)
        {
            var sprite = new TruckSprite();
            RotateSprite(sprite);
            Sprite = sprite;
        }
    }
}
