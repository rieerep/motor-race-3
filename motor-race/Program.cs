using System.Diagnostics;
using System.Security.Cryptography.X509Certificates;

namespace motor_race
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
			Car carOne = new Car(1, "Mazda Autozam", 0, 0, 120);
			Car carTwo = new Car(2, "Land Cruiser", 0, 0, 120);

			// Varje bilobjekt ska köras i en egen tråd
			var carRaceOne = await Race(carOne);
			var carRaceTwo = await Race(carTwo);
			var raceStatusTask = RaceStatus(carOne);

			Console.WriteLine(carRaceOne);
			//Console.WriteLine(raceStatusTask);
        }

		public static async Task<Car> Race(Car car)
		{
			//Random r = new Random();
			//int rInt = r.Next(0, 49);
			
			int tick = 30;
			int raceDistance = 10000;

			Console.WriteLine("Race starts!");

			while (true)
			{
				await Wait(tick);
				await RandomEvents(car);

				//if (rInt < 48)
				//{
				//	Console.WriteLine("Motorfel!");
				//	car.speed--;
				//}
				int speed = car.speed;
				var distanceTraveled = car.speed * tick;
				var timeTraveled = (distanceTraveled * speed) / 1000;
				// car.distanceTraveled += ((speed / 3600) * tick);
				car.distanceTraveled += (33.33M * tick);
				Console.WriteLine($"Car: {car.name} Distance traveled: {car.distanceTraveled} Speed: {car.speed} Race time:{timeTraveled}");
				// Console.WriteLine(raceStatusTask); // Hur skickar jag in raceStatusTask i metoden 'Race'?

				if (car.distanceTraveled >= raceDistance)
				{
					Console.WriteLine("You reached the finish line");
					return car;
				}
			}
		}
		private static async Task Wait(double delay = 1)
		{
			await Task.Delay(TimeSpan.FromSeconds(delay / 10));

			// Lägga in slumpgenererad händelse här?
			// Tärningsmodell - händelse baserad på vad man slår
			Console.WriteLine("30 seconds passed");
		}
		public async static Task RaceStatus(Car car)
		{
			while (true)
			{
				await Task.Delay(TimeSpan.FromSeconds(1));
				Console.WriteLine($"{car.name} has reached {car.distanceTraveled} and has been driving for {car.time} seconds");
			}
            
        }

		public static void PrintCar (Car car)
		{
			Console.WriteLine($"car name: {car.name}");
		}

		private static async Task RandomEvents(Car car)
		{
			Random r = new Random();
			int rInt = r.Next(0, 49);
			if (rInt == 15) 
			{
				Console.WriteLine("Slut på bensin!\nBehöver tanka, stannar 30 sekunder");
				await Wait(30);
				car.speed--;
			}
			else if (rInt < 1)
			{
                Console.WriteLine("Punktering!\nBehöver byta däck, stannar 20 sekunder\r\n!");
                await Wait(20);
                car.speed--;
            }	
			else if (rInt < 9 || > 15)
			{
                Console.WriteLine("Fågel på vindrutan!\nBehöver tvätta vindrutan, stannar 10 sekunder\r\n");
                await Wait(10);
                car.speed--;
            }
			else if (rInt < 10)
			{
                Console.WriteLine("Motorfel!\nHastigheten på bilen sänks med 1km/h." +
					"");
                await Wait(30);
                car.speed--;
            }
		}
    }
}