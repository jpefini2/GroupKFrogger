
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using Windows.UI.Xaml;

namespace FroggerStarter.Model
{
    /// <summary>A lane of traffic as a collection of newVehicles with a direction</summary>
    public class Lane : IEnumerable<Vehicle>
    {
        private readonly int y;
        private readonly int width;
        private readonly int length;
        private readonly Direction trafficDirection;
        private readonly List<Vehicle> vehicles;

        private Vehicle vehicleScheduledToReveal;
        private DispatcherTimer scheduledVehicleOffRoadTimer;

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
            this.vehicleScheduledToReveal = null;
            this.setupScheduledVehicleOffRoadTimer();
        }

        private void setupScheduledVehicleOffRoadTimer()
        {
            this.scheduledVehicleOffRoadTimer = new DispatcherTimer();
            this.scheduledVehicleOffRoadTimer.Tick += this.scheduledVehicleOffRoadTimerOnTick;
            this.scheduledVehicleOffRoadTimer.Interval = new TimeSpan(0, 0, 0, 0, 1);
        }

        private void scheduledVehicleOffRoadTimerOnTick(object sender, object e)
        {
            if (this.vehicleIsOffRoad(this.vehicleScheduledToReveal))
            {
                this.vehicleScheduledToReveal.Sprite.Visibility = Visibility.Visible;
                this.scheduledVehicleOffRoadTimer.Stop();
            }
        }

        /// <summary>Reveals a random vehicle hidden in this lane, if some are still hidden</summary>
        public void RevealRandomVehicle()
        {
            var random = new Random();
            var hiddenVehicles = new List<Vehicle>();
            foreach (var vehicle in this)
            {
                if (vehicle.Sprite.Visibility == Visibility.Collapsed)
                {
                    hiddenVehicles.Add(vehicle);
                }
            }

            if (hiddenVehicles.Count > 0)
            {
                var randomIndex = random.Next(hiddenVehicles.Count - 1);
                this.vehicleScheduledToReveal = hiddenVehicles[randomIndex];
                this.scheduledVehicleOffRoadTimer.Start();
            }
        }

        /// <summary>Moves all the stored newVehicles forward</summary>
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

        /// <summary>Sets this.vehicles to the given array of newVehicles and spaces them out</summary>
        /// <param name="newVehicles">The newVehicles to add</param>
        public void SetAndSpaceVehicles(Vehicle[] newVehicles)
        {
            this.vehicles.Clear();

            var gapBetweenVehicles = (this.length - getLengthOfAllVehiclesEndToEnd(newVehicles)) / newVehicles.Length;
            var vehicleX = gapBetweenVehicles / 2;
            var vehicleY = this.y + (this.width - newVehicles[0].Height) / 2;

            foreach (var vehicle in newVehicles)
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

        /// <summary>Hides all newVehicles.</summary>
        public void HideAllVehicles()
        {
            foreach (var vehicle in this)
            {
                vehicle.Sprite.Visibility = Visibility.Collapsed;
            }
        }



        /// <summary>Returns an enumerator that iterates through the collection.</summary>
        /// <returns>An enumerator that can be used to iterate through the collection.</returns>
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
