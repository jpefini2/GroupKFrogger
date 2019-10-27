
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;

namespace FroggerStarter.Model
{
    /// <summary>A lane of traffic as a collection of vehicles with a direction</summary>
    public class Lane : IEnumerable<Vehicle>
    {
        private readonly int y;
        private readonly int width;
        private readonly int length;
        private readonly Direction trafficDirection;
        private List<Vehicle> vehicles;

        /// <summary>Initializes a new instance of the <see cref="Lane"/> class.</summary>
        /// <param name="y">The y.</param>
        /// <param name="length">The length.</param>
        /// <param name="width">The width.</param>
        /// <param name="trafficDirection">The traffic direction.</param>
        /// <exception cref="ArgumentOutOfRangeException">y cannot be less than 0
        /// or
        /// length cannot be less than 0
        /// or
        /// width cannot be less than 0</exception>
        public Lane(int y, int length, int width, Direction trafficDirection)
        {
            if (y < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(y));
            }

            if (length < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(length));
            }

            if (width < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(width));
            }

            this.vehicles = new List<Vehicle>();
            this.y = y;
            this.width = width;
            this.length = length;
            this.trafficDirection = trafficDirection;
            
        }

        /// <summary>Moves all the stored vehicles forward</summary>
        public void MoveTraffic()
        {
            foreach (var vehicle in this.vehicles)
            {
                vehicle.MoveForward();

                if (!this.vehicleIsOffRoad(vehicle))
                {
                    continue;
                }

                if (this.trafficDirection == Direction.Left)
                {
                    vehicle.X = this.length;
                }
                else
                {
                    vehicle.X = (int) (0 - vehicle.Width);
                }
            }
        }

        private bool vehicleIsOffRoad(GameObject vehicle)
        {
            var laneCollision = new Rectangle(0, this.y, this.length, this.width);
            return !vehicle.CollisionBox.IntersectsWith(laneCollision);
        }

        /// <summary>Adds the given array of vehicles to this.vehicles</summary>
        /// <param name="vehicles">The vehicles to add</param>
        public void AddVehicles(Vehicle[] vehicles)
        {
            var gapBetweenVehicles = (this.length - getLengthOfAllVehiclesEndToEnd(vehicles)) / vehicles.Length;
            var vehicleX = gapBetweenVehicles / 2;
            var vehicleY = this.y + (this.width - vehicles[0].Height) / 2;

            foreach (var vehicle in vehicles)
            {
                vehicle.X = vehicleX;
                vehicle.Y = (int) vehicleY;
                this.vehicles.Add(vehicle);

                vehicleX += (int) (vehicle.Width + gapBetweenVehicles);
            }
        }

        private static int getLengthOfAllVehiclesEndToEnd(IEnumerable<Vehicle> vehicles)
        {
            var totalLength = 0;
            foreach (var vehicle in vehicles)
            {
                totalLength += (int) vehicle.Width;
            }
            return totalLength;
        }

        /// <summary>Sets the speed of all vehicles in this.vehicles</summary>
        /// <param name="speed">The speed.</param>
        public void SetTrafficSpeed(int speed)
        {
            foreach (var vehicle in this.vehicles)
            {
                vehicle.SetSpeed(speed, 0);
            }
        }

        /// <summary>Speeds up all vehicles in this.vehicles.</summary>
        public void SpeedUpTraffic()
        {
            foreach (var vehicle in this)
            {
                var newXSpeed = vehicle.SpeedX + 2;
                vehicle.SetSpeed(newXSpeed, 0);
            }
        }



        public IEnumerator<Vehicle> GetEnumerator()
        {
            return this.vehicles.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}
