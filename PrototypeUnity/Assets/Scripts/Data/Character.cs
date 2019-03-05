using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Data
{
	[Serializable]
	public class Character
	{
		//public int Resolve;
		public int Speed;
		public float Age;
		public float AgeOfDeath;
		public int DaysHungry;
		public bool Selfish;

		public Character(int food){
			// TODO : Define the rules for stats
			//Resolve = 20;
			Speed = 10;
			Age = 0;
			AgeOfDeath = 70 * GameManager.instance.secondsInAYear;
			DaysHungry = 0;
			Selfish = false;
		}

		public Character(){
			//Resolve = 20;
			Speed = 10;
			Age = 20;
			AgeOfDeath = 70;
			DaysHungry = 0;
			Selfish = false;
		}
	}
}
