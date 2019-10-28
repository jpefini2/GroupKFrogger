using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FroggerStarter.Model
{
    public class GameSettings : IEnumerable<LaneSettings>
    {
    public int NumberOfStartingLives { get; }

    public int NumberOfFrogHomes { get; }

    public int TimeLimit { get; }

    private List<LaneSettings> laneSettings;

        public GameSettings()
    {
        this.NumberOfStartingLives = 4;
        this.NumberOfFrogHomes = 5;
        this.TimeLimit = 20;
        this.laneSettings = new List<LaneSettings>();

        this.laneSettings.Add(new LaneSettings(Direction.Right, VehicleType.Car, 3, 6));
        this.laneSettings.Add(new LaneSettings(Direction.Left, VehicleType.Truck, 2, 5));
        this.laneSettings.Add(new LaneSettings(Direction.Left, VehicleType.Car, 3, 4));
        this.laneSettings.Add(new LaneSettings(Direction.Right, VehicleType.Truck, 3, 3));
        this.laneSettings.Add(new LaneSettings(Direction.Left, VehicleType.Car, 2, 2));
    }

        public IEnumerator<LaneSettings> GetEnumerator()
        {
            return this.laneSettings.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
