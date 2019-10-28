using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FroggerStarter.Model
{
    public class FrogHomeManager : IEnumerable<FrogHome>
    {
        private const int widthOfFrogHome = 50;

        private int y;
        private int widthOfHomeRow;
        private IList<FrogHome> frogHomes;

        public FrogHomeManager(int y, int widthOfHomeRow, int numberOfHomes)
        {
            this.y = y;
            this.widthOfHomeRow = widthOfHomeRow;
            this.createAndPlaceHomes(numberOfHomes);
        }

        private void createAndPlaceHomes(int numberOfHomes)
        {
            this.frogHomes = new List<FrogHome>();

            var widthOfAllHomesEndToEnd = numberOfHomes * widthOfFrogHome;
            var gapBetweenHomes = (this.widthOfHomeRow - widthOfAllHomesEndToEnd) / numberOfHomes;
            var x = gapBetweenHomes / 2;

            for (int i = 0; i < numberOfHomes; i++)
            {
                var frogHome = new FrogHome();
                frogHome.X = x;
                frogHome.Y = this.y;
                frogHome.FilledSprite.RenderAt(x, y);
                this.frogHomes.Add(frogHome);

                x += widthOfFrogHome + gapBetweenHomes;
            }
        }

        public bool isCollidingWithEmptyHome(Rectangle collisionBox)
        {
            bool isColliding = false;
            foreach (var frogHome in this)
            {
                if (frogHome.CollisionBox.IntersectsWith(collisionBox) && !frogHome.IsFilled)
                {
                    isColliding = true;
                    frogHome.FillHome();
                    break;
                }
            }

            return isColliding;
        }

        public void FillHomesIntersectingWith(Rectangle collisionBox)
        {
            foreach (var frogHome in this)
            {
                if (frogHome.CollisionBox.IntersectsWith(collisionBox))
                {
                    frogHome.FillHome();
                    break;
                }
            }
        }

        public IEnumerator<FrogHome> GetEnumerator()
        {
            return this.frogHomes.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}
