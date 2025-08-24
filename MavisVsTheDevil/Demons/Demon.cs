namespace MavisVsTheDevil.Demons;

public class Demon
{
	public string Name;
	public string Struggle;
	public string CauseOfDeath;
	private string WordListName;
	private string[] DemonWordList = [];
	public string imagePath;

	public static Action<Demon> OnDemonChosen;
	//word lists
	//modifier 
	private static Demon[] LastDemons = new Demon[5];
	private static int LDIndex;
	
	private static Demon RandomDemon()
	{
		var valid = Demons.Where(x => !string.IsNullOrEmpty(x.imagePath)).ToList();
		return valid[Program.random.Next(valid.Count)];
	}

	public bool GetWordList(out string wordlistName, out string[] wordList)
	{
		if (DemonWordList.Length == 0)
		{
			wordlistName = "";
			wordList = null;
			return false;
		}
		else
		{
			wordlistName = WordListName;
			wordList = DemonWordList;
			return true;
		}
	}

	public static Demon GetRandomDemon()
	{
		var d = RandomDemon();
		int escape = 0;
		while (LastDemons.Contains(d) && escape < 100)
		{
			d = RandomDemon();
			escape++;
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
			imagePath = "zaboomafoo.png",
		},
		new Demon()
		{
			Name = "Angela Anaconda",
			Struggle = "You Did Not Remember The Family",
			CauseOfDeath = "Hit By a Ford F-150",
			imagePath = "angelaanaconda.png",
		},
		new Demon()
		{
			Name = "Ruff Ruffman",
			Struggle = "You Did Not Enjoy Life To The Fullest",
			CauseOfDeath = "Lyme Disease",
			imagePath = "ruffruffman.png"
		},
		new Demon()
		{
			Name = "Prometheus & Bob",
			Struggle = "You cannot understand them",
			CauseOfDeath = "Scurvy",
			imagePath = "prometheusandbob.png",
		},
		new Demon()
		{
			Name = "Zoombini",
			Struggle = "We Just Wanted To Live In Comfort",
			CauseOfDeath = "Wildfire",
			imagePath = "zoombinis.png"
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
			Struggle = "I Could Have Left Whenever I Wanted",
			CauseOfDeath = "Cat And/Or Dog Bite"
		},
		new Demon()
		{
			Name = "Carmen Sandiego",
			Struggle = "I Did Not Want To Be Found",
			CauseOfDeath = "Titan Submersible Implosion",
			DemonWordList = Wordlist.Wordlist.PLACES,
			WordListName = "Where She Fled To",
			imagePath = "carmensandiego.png"
		},
		new Demon()
		{
			Name = "Morty Maxwell",
			Struggle = "You Did Not Follow The Simple Machines Into Complexity",
			CauseOfDeath = "Cancer",
			imagePath = "mortymaxwell.png"
		},
		new Demon()
		{
			Name = "The Carriage from Oregon Trail",
			Struggle = "You Did Not Allocate The Resources",
			CauseOfDeath = "Super Dysentery",
			DemonWordList = Wordlist.Wordlist.TRAILDEATHS,
			WordListName = "How I Watched Them Go",
			imagePath = "oregontrail.png"
		},
		new Demon()
		{
			Name = "Time Rider In American History",
			Struggle = "I Never Wanted To Ignore The Sins",
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
			CauseOfDeath = "Five Tornados",
			imagePath = "gusoddman.png",
		},
		new Demon()
		{
			Name = "Pablo Sanchez",
			Struggle = "You Demanded So Much From Me",
			CauseOfDeath = "Picked a Fight With Multiple Professional Baseball Players",
			DemonWordList = Wordlist.Wordlist.BACKYARDPLAYERS,
			WordListName = "Those Who I Left Behind",
			imagePath = "pablo_sliding.png",
			
		},
		new Demon()
		{
			Name = "Martha Speaks",
			Struggle = "You Should Never Have Cursed Me With Knowledge",
			CauseOfDeath = "Foreign Body Airway Obstruction",
			imagePath = "marthaspeaks.png",
		},
		new Demon()
		{
			Name = "Putt-Putt",
			Struggle = "I couldn't remove the Al Gore bumper sticker",
			CauseOfDeath = "Cracked Piston Rod",
			imagePath = "putt-putt.png"
		}
	};


}