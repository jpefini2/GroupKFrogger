using System.Collections;
using System.Collections.Generic;
using System.Drawing;

namespace FroggerStarter.Model
{
    /// <summary>A collection of frog homes</summary>
    /// <seealso>
    ///     <cref>System.Collections.Generic.IEnumerable{FroggerStarter.Model.FrogHome}</cref>
    /// </seealso>
    public class FrogHomeManager : IEnumerable<FrogHome>
    {
        private const int WidthOfFrogHome = 50;

        private readonly int y;
        private readonly int widthOfHomeRow;
        private IList<FrogHome> frogHomes;

        /// <summary>
        /// Initializes a new instance of the <see cref="FrogHomeManager" /> class.
        /// </summary>
        /// <param name="y">The y.</param>
        /// <param name="widthOfHomeRow">The width of home row.</param>
        /// <param name="numberOfHomes">The number of homes.</param>
        public FrogHomeManager(int y, int widthOfHomeRow, int numberOfHomes)
        {
            this.y = y;
            this.widthOfHomeRow = widthOfHomeRow;
            this.createAndPlaceHomes(numberOfHomes);
        }

        /// <summary>
        /// Creates the and place homes.
        /// </summary>
        /// <param name="numberOfHomes">The number of homes.</param>
        private void createAndPlaceHomes(int numberOfHomes)
        {
            this.frogHomes = new List<FrogHome>();

            var widthOfAllHomesEndToEnd = numberOfHomes * WidthOfFrogHome;
            var gapBetweenHomes = (this.widthOfHomeRow - widthOfAllHomesEndToEnd) / numberOfHomes;
            var x = gapBetweenHomes / 2;

            for (var i = 0; i < numberOfHomes; i++)
            {
                var frogHome = new FrogHome {X = x, Y = this.y};
                frogHome.FilledSprite.RenderAt(x, this.y);
                this.frogHomes.Add(frogHome);

                x += WidthOfFrogHome + gapBetweenHomes;
            }
        }

        /// <summary>
        /// Determines whether the specified collisionBox is colliding with an empty home stored here
        /// </summary>
        /// <param name="collisionBox">The collision box.</param>
        /// <returns>
        ///   <c>true</c> if [the specified collision box] [is colliding with empty home]; otherwise, <c>false</c>.
        /// </returns>
        public bool IsCollidingWithEmptyHome(Rectangle collisionBox)
        {
            var isColliding = false;
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

        /// <summary>
        /// Fills the home(s) intersecting with the specified collisionBox
        /// </summary>
        /// <param name="collisionBox">The collision box.</param>
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

        /// <summary>
        /// Determines whether all the homes stored here are filled
        /// </summary>
        /// <returns>
        /// true if all are filled; otherwise, false
        /// </returns>
        public bool AllHomesAreFilled()
        {
            var allHomesFilled = true;
            foreach (var frogHome in this)
            {
                if (!frogHome.IsFilled)
                {
                    allHomesFilled = false;
                    break;
                }
            }

            return allHomesFilled;
        }

        /// <summary>
        /// Empties all homes.
        /// </summary>
        public void EmptyAllHomes()
        {
            foreach (var home in this)
            {
                home.EmptyHome();
            }
        }

        /// <summary>
        /// Returns an enumerator that iterates through the collection.
        /// </summary>
        /// <returns>
        /// An enumerator that can be used to iterate through the collection.
        /// </returns>
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
