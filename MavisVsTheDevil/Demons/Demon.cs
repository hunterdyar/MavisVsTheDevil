namespace MavisVsTheDevil.Demons;

public class Demon
{
	public string Name;
	public string Struggle;
	public string CauseOfDeath;
	//word lists
	//modifier 
	private static Demon[] LastDemons = new Demon[5];
	private static int LDIndex;
	
	private static Demon RandomDemon()
	{
		return Demons[Program.random.Next(Demons.Length)];
	}

	public static Demon GetRandomDemon()
	{
		var d = RandomDemon();
		while (LastDemons.Contains(d))
		{
			d = RandomDemon();
		}
		//got one!
		LDIndex += 1;
		LDIndex %= LastDemons.Length;
		LastDemons[LDIndex] = d;
		return d;
	}

	public static Demon[] Demons = new[]
	{
		new Demon()
		{
			Name = "Zaboomafoo",
			Struggle = "You Did Not Care For The Animals",
			CauseOfDeath = "Starvation",
		},
		new Demon()
		{
			Name = "Angelica Anaconda",
			Struggle = "You Did Not Remember The Family",
			CauseOfDeath = "Hit By a Ford F-150",
		},
		new Demon()
		{
			Name = "Ruff Ruffman",
			Struggle = "You Did Not Enjoy Life To The Fullest",
			CauseOfDeath = "Lyme Disease",
		},
		new Demon()
		{
			Name = "Prometheus & Bob",
			Struggle = "You Did Not Embrace Hyperbolic Whimsy",
			CauseOfDeath = "Scurvy"
		},
		new Demon()
		{
			Name = "Zoombini",
			Struggle = "We Just Wanted To Live In Comfort",
			CauseOfDeath = "Wildfire"
		},
		new Demon()
		{
			Name = "Daddles",
			Struggle = "You Did Not Watch The Show",
			CauseOfDeath = "Blunt Force Trauma via Cricket Bat"
		},
		new Demon()
		{
			Name = "Mazlo",
			Struggle = "You Did Not Observe The Warnings",
			CauseOfDeath = "Cat And/Or Dog Bite"
		},
		new Demon()
		{
			Name = "Carmen Sandiego",
			Struggle = "You Did Not Want To Be Found",
			CauseOfDeath = "Titan Submersible Implosion",
		},
		new Demon()
		{
			Name = "Morty Maxwell",
			Struggle = "You Did Not Follow The Simple Machines Into Complexity",
			CauseOfDeath = "Cancer"
		},
		new Demon()
		{
			Name = "The Carriage from Oregon Trail",
			Struggle = "You Did Not Allocate The Resources",
			CauseOfDeath = "Super Dysentery"
		},
		new Demon()
		{
			Name = "Time Rider In American History",
			Struggle = "You Did Not Remember",
			CauseOfDeath = "Dread",
		},
		new Demon()
		{
			Name = "I.M. Meen",
			Struggle = "You Did Not Escape The Labyrinth",
			CauseOfDeath = "Better Grammar"
		},
		new Demon()
		{
			Name = "Gus Oddman",
			Struggle = "They were pushing the power plants to their limits!",
			CauseOfDeath = "Five Tornado"
		},
		new Demon()
		{
			Name = "Pablo Sanchez",
			Struggle = "You Demanded So Much From Me",
			CauseOfDeath = "Picked a Fight With Multiple Professional Baseball Players"
		},
		new Demon()
		{
			Name = "Martha Speaks",
			Struggle = "You Should Never Have Cursed Me With Knowledge",
			CauseOfDeath = "Foreign Body Airway Obstruction"
		}
	};


}