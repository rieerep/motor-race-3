using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace motor_race
{
	internal class Car
	{
        public int carId { get; set; }
        public string name { get; set; }
        public decimal distanceTraveled { get; set; } = 0;
        public decimal time { get; set; }
        public int speed { get; set; } = 120;

        public int MetersPerSec()
        {
            return this.speed / 3600;
        }

        public Car(int carId, string name, decimal distanceTraveled, decimal time, int speed)
        {
            this.carId = carId;
            this.name = name;
            this.distanceTraveled = distanceTraveled;
            this.speed = speed;
        }

    }
}
