using FroggerStarter.View.Sprites;

namespace FroggerStarter.Model
{
    public class TurboCar : Car
    {
        public TurboCar(Direction orientation, int speed) : base(orientation, speed * 2)
        {
            var sprite = new TurboCarSprite();
            RotateSprite(sprite);
            Sprite = sprite;
        }
    }
}
