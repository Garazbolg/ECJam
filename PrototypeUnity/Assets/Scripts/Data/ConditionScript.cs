using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Misc;

namespace Assets.Scripts.Data
{
	/// <summary>
	/// Describes a condition (predicat) to be checked
	/// Lines are &&
	/// exemple : 
	/// 
	///	A | B
	///	C
	///	A | D
	///	
	/// Gets you : (A || B) && C && (A || D)
	/// </summary>
	[Serializable]
	public class ConditionScript
	{
		public string script;

		public bool Fulfill(GameData gameData){
			if (string.IsNullOrEmpty(script)) return true;
			string[] lines = script.Split('\n');
			foreach(string line in lines){
				bool res = false;

				string[] statements = line.Split('|');

				foreach(string statement in statements){
					res = res || Check(statement.Split(" ".ToArray(), StringSplitOptions.RemoveEmptyEntries), gameData);
				}

				if(!res)
					return false;
			}
			return true;
		}

		private bool Check(string[] words, GameData gameData){
			switch(words[0]){
				case "!":
					return !Check(words.Next(), gameData);

				case "season":
					return words[1].CompareTo(gameData.cycles[1].GetCurrentStageName().ToLower()) == 0;

				case "day":
				case "night":
					return gameData.cycles[0].GetCurrentStageName().ToLower().StartsWith(words[0]);
			}
			return false;
		}
	}
}
