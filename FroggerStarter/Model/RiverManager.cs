using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FroggerStarter.Model
{
    /// <summary>Manages a River</summary>
    /// <seealso cref="FroggerStarter.Model.LaneManager" />
    public class RiverManager : LaneManager
    {
        /// <summary>Gets or sets the collision box.</summary>
        /// <value>The collision box.</value>
        public Rectangle CollisionBox { get; set; }

        /// <summary>Initializes a new instance of the <see cref="RiverManager"/> class.</summary>
        /// <param name="y">The y.</param>
        /// <param name="laneLength">Length of the lane.</param>
        /// <param name="laneWidth">Width of the lane.</param>
        /// <exception cref="ArgumentOutOfRangeException">y
        /// or
        /// laneLength
        /// or
        /// laneWidth</exception>
        public RiverManager(int y, int laneLength, int laneWidth) : base(y, laneLength, laneWidth)
        {
            if (y < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(y));
            }
            if (laneLength < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(laneLength));
            }
            if (laneWidth < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(laneWidth));
            }

            this.CollisionBox = new Rectangle(0, y, laneLength, 0);
        }

        /// <summary>Adds the lane.</summary>
        /// <param name="laneSettings">The lane settings.</param>
        public void AddLane(LaneSettings laneSettings)
        {
            var laneY = this.y + this.lanes.Count * this.laneWidth;
            var lane = new Lane(laneY, this.laneLength, this.laneWidth, laneSettings.TrafficDirection);
            this.fillLaneWithVehicles(laneSettings, lane);
            this.lanes.Add(lane);

            this.CollisionBox = new Rectangle(0, this.y, this.laneLength, this.lanes.Count * this.laneWidth);
        }

        private void fillLaneWithVehicles(LaneSettings laneSettings, Lane lane)
        {
            var vehicles = new Vehicle[laneSettings.TrafficAmount];
            for (var i = 0; i < laneSettings.TrafficAmount; i++)
            {
                var vehicle = this.vehicleFactory.MakeVehicle(laneSettings.TrafficType, laneSettings.TrafficDirection, laneSettings.StartingTrafficSpeed);
                vehicles[i] = vehicle;
            }
            lane.SetAndSpaceVehicles(vehicles);
        }

        /// <summary>Moves the traffic in all lanes</summary>
        public void MoveTraffic()
        {
            this.moveTrafficForwardInAllLanes();
        }

        private void moveTrafficForwardInAllLanes()
        {
            foreach (var lane in this.lanes)
            {
                lane.MoveTraffic();
            }
        }
    }
}
