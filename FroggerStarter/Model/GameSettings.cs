using System.Collections;
using System.Collections.Generic;

namespace FroggerStarter.Model
{
    /// <summary>The settings for a game of Frogger</summary>
    /// <seealso>
    ///     <cref>System.Collections.Generic.IEnumerable{FroggerStarter.Model.LaneSettings}</cref>
    /// </seealso>
    public class GameSettings : IEnumerable<LaneSettings>
    {
        /// <summary>Gets the number of starting lives.</summary>
        /// <value>The number of starting lives.</value>
        public int NumberOfStartingLives { get; }

        /// <summary>Gets the number of frog homes.</summary>
        /// <value>The number of frog homes.</value>
        public int NumberOfFrogHomes { get; }

        /// <summary>Gets the time limit.</summary>
        /// <value>The time limit.</value>
        public int TimeLimit { get; }

        /// <summary>Gets the score modifier.</summary>
        /// <value>The score modifier.</value>
        public int ScoreModifier { get; }

    private readonly List<LaneSettings> laneSettings;

        /// <summary>Initializes a new instance of the <see cref="GameSettings"/> class.</summary>
        public GameSettings()
    {
        this.NumberOfStartingLives = 4;
        this.NumberOfFrogHomes = 5;
        this.TimeLimit = 20;
        this.ScoreModifier = 100;
        this.laneSettings = new List<LaneSettings> {
            new LaneSettings(Direction.Right, VehicleType.Car, 5, 6),
            new LaneSettings(Direction.Left, VehicleType.Truck, 3, 5),
            new LaneSettings(Direction.Left, VehicleType.TurboCar, 4, 5),
            new LaneSettings(Direction.Right, VehicleType.Truck, 2, 3),
            new LaneSettings(Direction.Left, VehicleType.Car, 3, 2)
        };

    }

        /// <summary>Returns an enumerator that iterates through the collection.</summary>
        /// <returns>An enumerator that can be used to iterate through the collection.</returns>
        public IEnumerator<LaneSettings> GetEnumerator()
        {
            return this.laneSettings.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}
