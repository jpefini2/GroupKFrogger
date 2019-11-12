using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Windows.UI.Xaml;

namespace FroggerStarter.Model
{
    /// <summary>
    /// Manages a collection of traffic lanes
    /// </summary>
    /// <seealso cref="Vehicle" />
    /// <seealso cref="Vehicle" />
    public abstract class LaneManager : IEnumerable<Vehicle>
    {
        /// <summary>
        /// The y
        /// </summary>
        protected readonly int Y;
        /// <summary>
        /// The lane length
        /// </summary>
        protected readonly int LaneLength;
        /// <summary>
        /// The lane width
        /// </summary>
        protected readonly int LaneWidth;
        /// <summary>
        /// The lanes
        /// </summary>
        protected readonly List<Lane> Lanes;
        /// <summary>
        /// The vehicle factory
        /// </summary>
        protected VehicleFactory VehicleFactory;

        /// <summary>Initializes a new instance of the <see cref="LaneManager"/> class.</summary>
        /// <param name="y">The y.</param>
        /// <param name="laneLength">Length of the lane.</param>
        /// <param name="laneWidth">Width of the lane.</param>
        /// <exception cref="ArgumentOutOfRangeException">y
        /// or
        /// laneLength
        /// or
        /// laneWidth</exception>
        protected LaneManager(int y, int laneLength, int laneWidth)
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

            this.Y = y;
            this.LaneLength = laneLength;
            this.LaneWidth = laneWidth;
            this.Lanes = new List<Lane>();
            this.VehicleFactory = new VehicleFactory();
        }

        /// <summary>Adds the lane.</summary>
        /// <param name="laneSettings">The lane settings.</param>
        public virtual void AddLane(LaneSettings laneSettings)
        {
            var laneY = this.Y + this.Lanes.Count * this.LaneWidth;
            var lane = new Lane(laneY, this.LaneLength, this.LaneWidth, laneSettings.TrafficDirection);
            this.fillLaneWithVehicles(laneSettings, lane);
            this.Lanes.Add(lane);
        }

        private void fillLaneWithVehicles(LaneSettings laneSettings, Lane lane)
        {
            var vehicles = new Vehicle[laneSettings.TrafficAmount];
            for (var i = 0; i < laneSettings.TrafficAmount; i++)
            {
                var vehicle = this.VehicleFactory.MakeVehicle(laneSettings.TrafficType, laneSettings.TrafficDirection, laneSettings.StartingTrafficSpeed);
                vehicles[i] = vehicle;
            }
            lane.SetAndSpaceVehicles(vehicles);
        }

        /// <summary>Checks if any vehicle in the road is colliding with the given collisionBox</summary>
        /// <param name="collisionBox">The bounding box.</param>
        /// <returns></returns>
        public bool VehiclesAreCollidingWith(Rectangle collisionBox)
        {
            var isHit = false;
            foreach (var vehicle in this)
            {
                if (collisionBox.IntersectsWith(vehicle.CollisionBox) && vehicle.Sprite.Visibility == Visibility.Visible)
                {
                    isHit = true;
                }
            }
            return isHit;
        }

        /// <summary>Moves the traffic in all lanes</summary>
        public void MoveTraffic()
        {
            this.moveTrafficForwardInAllLanes();
        }

        private void moveTrafficForwardInAllLanes()
        {
            foreach (var lane in this.Lanes)
            {
                lane.MoveTraffic();
            }
        }

        /// <summary>Returns an enumerator that iterates through the collection.</summary>
        /// <returns>An enumerator that can be used to iterate through the collection.</returns>
        public IEnumerator<Vehicle> GetEnumerator()
        {
            return this.Lanes.SelectMany(lane => lane).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}
