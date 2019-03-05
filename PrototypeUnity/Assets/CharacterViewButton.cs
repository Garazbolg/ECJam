using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterViewButton : MonoBehaviour
{
	public Assets.Scripts.Data.Character character;

	public UnityEngine.UI.Text text;

	public void Init(Assets.Scripts.Data.Character c)
	{
		character = c;
		string t = "";

		t += "Age: " + (c.Age/GameManager.instance.secondsInAYear).ToString("N2");
		t += "\nSpeed: " + c.Speed;
		t += "\nSelfish: " + c.Selfish;
		t += "\nHunger: " + c.DaysHungry + "/" + GameManager.instance.gameData.maxDaysHungry;

		text.text = t;
	}

	public void Select(){
		Debug.Log("Selected");
		GameManager.instance.gameData.playerCharacter = character;
	}
}
