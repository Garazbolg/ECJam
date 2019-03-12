using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Data
{
	[Serializable]
	public class Character
	{
		public static int MinSpeed = 35;
		public static int MaxSpeed = 40;


		public int Speed;
		public float Age;
		public float AgeOfDeath;
		public int DaysHungry;
		public bool Selfish;

		public Color hat;
		public Color vest;

		public Character(int food){
			// TODO : Define the rules for stats
			Speed = MinSpeed + Mathf.RoundToInt((1-Mathf.Clamp01(food /100f)) * (MaxSpeed-MinSpeed));
			Age = 0;
			AgeOfDeath = (1 + food/100f) * GameManager.instance.secondsInAYear;
			DaysHungry = 0;
			Selfish = food > 50;
			hat = UnityEngine.Random.ColorHSV(0, 1, 1, 1);
			vest = UnityEngine.Random.ColorHSV(0, 1, 0.5f, 0.5f);
		}

		public Character(){
			Speed = 10;
			Age = 20;
			AgeOfDeath = 70;
			DaysHungry = 0;
			Selfish = false;
			hat = Color.red;
			vest = Color.red;
		}
	}
}
