using System.Diagnostics;
using System.Security.Cryptography.X509Certificates;

namespace motor_race
{
    internal class Program
    {
        public static async Task Main(string[] args)
        {
			

            // decimal decNum1 = 119.98M;
            // decimal ceiling1 = Math.Ceiling(decNum1);
            // Console.WriteLine(ceiling1);
            // TODO Välkommen till racet, tryck på knapp för att starta.
            Console.WriteLine("Välkommen till biltävlingen.\nTryck på valfri knapp för att starta");
			Console.ReadKey();
			Console.WriteLine("Tryck på valfri knapp under loppets gång för att se hur det går.");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("...");
            Thread.Sleep(1000);
            Console.WriteLine("...");
            Console.ForegroundColor = ConsoleColor.Green;
            Thread.Sleep(1000);
            Console.WriteLine("...");
            Console.ForegroundColor = ConsoleColor.White;


            // Instansierar två bilobjekt av klassen Car
            Car carOne = new Car(1, "Mazda Autozam", 0, 0);
			Car carTwo = new Car(2, "Land Cruiser", 0, 0);

			// Varje bilobjekt ska köras i en egen tråd

			var carOneTask = Race(carOne);
			var carTwoTask = Race(carTwo);
			var statusTask = RaceStatus(new List<Car> { carOne, carTwo });
			var carRaces = new List<Task> { carOneTask, carTwoTask, statusTask };
            bool raceHasWinner = false;
            while (carRaces.Count > 0)
            {
				
                Task finishedTask = await Task.WhenAny(carRaces);
				if (finishedTask == carOneTask)
				{
					if (!raceHasWinner)
					{
						raceHasWinner = true;
						Console.WriteLine("Mazda finished first");
					}
					else
						Console.WriteLine("Mazda finished");

				}
				else if (finishedTask == carTwoTask)
				{
					if (!raceHasWinner)
					{
						raceHasWinner = true;
						Console.WriteLine("Land Cruiser finished first");
					}
					else
						Console.WriteLine("Land Cruiser finished");
				}

                await finishedTask;
                carRaces.Remove(finishedTask);
            }


            //Console.WriteLine(carRaceOne);

        }

		public async static Task<Car> Race(Car car)
		{

			// Om timeremaining är mindre än 30 - await time remaining istället för 30
			
			int intervalTick = 30;
			int raceDistance = 10000;
			//double timeremaining;
			//double distanceRemaining;

			decimal timeremaining = (raceDistance - car.distanceTraveled);
			//

			

			if (car.distanceTraveled < intervalTick)
			{
				await Wait(timeremaining)
			};

			while (true)
			{
				
				await Wait(intervalTick);
				int eventDelay = await RandomEvents(car);

				//if (rInt < 48)
				//{
				//	Console.WriteLine("Motorfel!");
				//	car.speed--;
				//}
				decimal speed = car.speed;
				var distanceTraveled = speed * intervalTick;
				//var timeTraveled = intervalTick;
				// car.distanceTraveled += ((speed / 3600) * tick);
				car.distanceTraveled += distanceTraveled;
				car.time += intervalTick + eventDelay;
                
				// Console.WriteLine(raceStatusTask); // Hur skickar jag in raceStatusTask i metoden 'Race'?

				if (car.distanceTraveled >= raceDistance)
				{
					// Console.WriteLine("You reached the finish line");
					return car;
				}
			}
		}
		private static async Task Wait(double delay = 1)
		{
			await Task.Delay(TimeSpan.FromSeconds(delay / 10));

			// Lägga in slumpgenererad händelse här?
			// Tärningsmodell - händelse baserad på vad man slår
			//Console.WriteLine("30 seconds passed");
		}
		public async static Task RaceStatus(List<Car> cars)
		{
			while (true)
			{

                DateTime start = DateTime.Now;

                bool gotKey = false;

                while ((DateTime.Now - start).TotalSeconds < 2)
                {
                    if (Console.KeyAvailable)
                    {
                        gotKey = true;
                        break;
                    }
                }

                if (gotKey)
                {
                    Console.ReadKey();
                    cars.ForEach(car =>
                    {
                        Console.WriteLine($"Bil: {car.name} Körd sträcka: {Math.Round(car.distanceTraveled, 2)} meter Hastighet: {Math.Round(car.KilometersPerHour())} km/h Tid:{car.time} sekunder");
                        // console.writeline($"{car.name} has reached {car.distancetraveled} and has been driving for {car.time} seconds");
                    });
					gotKey = false;
                }

                await Task.Delay(1);

				var finishedCars = cars.Where(car => car.distanceTraveled >= 10000).Count();
				if (finishedCars == cars.Count())
				{
					Console.WriteLine("Race over");
					return;
				}
				//var distanceRemaining = 0;
			}
            
        }

		public static void PrintCar (Car car)
		{
			Console.WriteLine($"car name: {car.name}");
		}

		private static async Task<int> RandomEvents(Car car)
		{
			Random r = new Random();
			int rInt = r.Next(50) + 1;
			if (rInt == 1) 
			{
				Console.WriteLine($"{car.name} fick slut på bensin! Behöver tanka, stannar 30 sekunder");
				await Wait(30);
				return 30;
			}
			else if (rInt <= 3)
			{
                Console.WriteLine($"{car.name} fick punktering! Behöver byta däck, stannar 20 sekunder!");
                await Wait(20);
				return 20;
            }	
			else if (rInt <= 8)
			{
                Console.WriteLine($"{car.name} fick en fågel på vindrutan! Behöver tvätta vindrutan, stannar 10 sekunder");
                await Wait(10);
				return 10;
            }
			else if (rInt <= 18)
			{
                Console.WriteLine($"{car.name} fick motorfel! Hastigheten på bilen sänks med 1km/h.");
                car.speed = car.speed - (1 / 3.6M);
				// 1 m/s = 3.6 km/h
				// 1 km/h = 1 / 3.6 m/s
            }
            return 0;
        }
    }
}