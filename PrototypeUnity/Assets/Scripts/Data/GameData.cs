using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Data
{
	[Serializable]
	public class GameData
	{
		public int numberOfWWToSpawn = 1;
		public Cycle[] cycles;

		public int foodSupply = 30;
		public int seedAmount = 5;

		public int maxDaysHungry = 3;

		public int selectedSeed = 0;

		public bool generateSockets = true;
		public int socketDensity = 10;

		public SeedSocket[] sockets;

		public bool hadKids = false;
		public Character playerCharacter;
		public List<Character> characters;
	}
}
