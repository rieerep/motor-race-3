using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;

namespace motor_race
{
	internal class Car
	{
        public int carId { get; set; }
        public string name { get; set; }
        // DistanceTraveled is measuered in meters
        public decimal distanceTraveled { get; set; } = 0;

        // Time is measured in seconds
        public decimal time { get; set; }
        public decimal speed { get; set; } = 33.33M;

        public decimal KilometersPerHour()
        {
            return this.speed * 3.6M;
        }
		public decimal TimeRemaining(decimal raceDistance)
		{
			return (raceDistance - distanceTraveled) / speed;
		}

		public Car(int carId, string name, decimal distanceTraveled, decimal time)
        {
            this.carId = carId;
            this.name = name;
            this.distanceTraveled = distanceTraveled;
            this.speed = speed;
            this.time = time;
        }

    }
}
