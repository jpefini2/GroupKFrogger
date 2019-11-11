using System;
using System.Collections;
using System.Collections.Generic;

namespace FroggerStarter.Model
{
    /// <summary>The settings for a game of Frogger</summary>
    /// <seealso>
    ///     <cref>System.Collections.Generic.IEnumerable{FroggerStarter.Model.LaneSettings}</cref>
    /// </seealso>
    public class GameSettings
    {
        /// <summary>Gets the number of starting lives.</summary>
        /// <value>The number of starting lives.</value>
        public int NumberOfStartingLives { get; }

        /// <summary>Gets the number of frog homes.</summary>
        /// <value>The number of frog homes.</value>
        public int NumberOfFrogHomes { get; }

        public int NumberOfLevels { get; }

        /// <summary>Gets the time limit.</summary>
        /// <value>The time limit.</value>
        public int TimeLimit { get; }

        /// <summary>Gets the score modifier.</summary>
        /// <value>The score modifier.</value>
        public int ScoreModifier { get; }

        public List<LevelSettings> LevelSettings { get; }

        /// <summary>Initializes a new instance of the <see cref="GameSettings"/> class.</summary>
        public GameSettings()
        {
            this.NumberOfStartingLives = 4;
            this.NumberOfFrogHomes = 5;
            this.NumberOfLevels = 3;
            this.TimeLimit = 20;
            this.ScoreModifier = 100;
            this.LevelSettings = new List<LevelSettings>();
            

            this.setupLevelSettings();
        }

        private void setupLevelSettings()
        {
            for (int i = 0; i < this.NumberOfLevels; i++)
            {
                this.LevelSettings.Add(new LevelSettings());
            }

            this.fillRoadSettingsForAllLevels();
            this.fillRiverSettingsForAllLevels();
        }

        private void fillRiverSettingsForAllLevels()
        {
            List<LaneSettings> level1RoadSettings = new List<LaneSettings> {
                new LaneSettings(Direction.Right, VehicleType.Car, 5, 6),
                new LaneSettings(Direction.Left, VehicleType.Truck, 3, 5),
                new LaneSettings(Direction.Left, VehicleType.TurboCar, 4, 5),
                new LaneSettings(Direction.Right, VehicleType.Truck, 2, 3),
                new LaneSettings(Direction.Left, VehicleType.Car, 3, 2)
            };
            this.LevelSettings[0].RoadLaneSettings = level1RoadSettings;

            List<LaneSettings> level2RoadSettings = new List<LaneSettings> {
                new LaneSettings(Direction.Left, VehicleType.Truck, 4, 8),
                new LaneSettings(Direction.Right, VehicleType.Truck, 3, 7),
                new LaneSettings(Direction.Right, VehicleType.TurboCar, 4, 7),
                new LaneSettings(Direction.Left, VehicleType.Car, 4, 5),
                new LaneSettings(Direction.Right, VehicleType.TurboCar, 4, 4)
            };
            this.LevelSettings[1].RoadLaneSettings = level2RoadSettings;

            List<LaneSettings> level3RoadSettings = new List<LaneSettings> {
                new LaneSettings(Direction.Left, VehicleType.Truck, 4, 10),
                new LaneSettings(Direction.Right, VehicleType.Truck, 4, 9),
                new LaneSettings(Direction.Right, VehicleType.TurboCar, 4, 9),
                new LaneSettings(Direction.Left, VehicleType.Car, 5, 7),
                new LaneSettings(Direction.Left, VehicleType.TurboCar, 5, 6)
            };
            this.LevelSettings[2].RoadLaneSettings = level3RoadSettings;
        }

        private void fillRoadSettingsForAllLevels()
        {
            List<LaneSettings> level1RiverSettings = new List<LaneSettings> {
                new LaneSettings(Direction.Right, VehicleType.Log, 2, 6),
                new LaneSettings(Direction.Left, VehicleType.Turtles, 3, 5),
                new LaneSettings(Direction.Right, VehicleType.Log, 1, 4),
                new LaneSettings(Direction.Right, VehicleType.Log, 2, 3),
                new LaneSettings(Direction.Left, VehicleType.Turtles, 2, 2)
            };
            this.LevelSettings[0].RiverLaneSettings = level1RiverSettings;

            List<LaneSettings> level2RiverSettings = new List<LaneSettings> {
                new LaneSettings(Direction.Left, VehicleType.Turtles, 3, 7),
                new LaneSettings(Direction.Right, VehicleType.Log, 2, 6),
                new LaneSettings(Direction.Left, VehicleType.Log, 3, 5),
                new LaneSettings(Direction.Left, VehicleType.Turtles, 3, 4),
                new LaneSettings(Direction.Right, VehicleType.Log, 2, 3)
            };
            this.LevelSettings[1].RiverLaneSettings = level2RiverSettings;

            List<LaneSettings> level3RiverSettings = new List<LaneSettings> {
                new LaneSettings(Direction.Left, VehicleType.Log, 2, 8),
                new LaneSettings(Direction.Left, VehicleType.Turtles, 2, 7),
                new LaneSettings(Direction.Right, VehicleType.Turtles, 3, 6),
                new LaneSettings(Direction.Right, VehicleType.Log, 2, 5),
                new LaneSettings(Direction.Left, VehicleType.Log, 3, 4)
            };
            this.LevelSettings[2].RiverLaneSettings = level3RiverSettings;
        }
    }
}
