using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Assets.Scripts.Data;

public class CharacterViewButton : MonoBehaviour
{
	public Character character;
	
	public Slider age;
	public Slider speed;
	public Slider selfish;
	public Slider hunger;

	public Image hat;
	public Image vest;

	public void Init(Character c)
	{
		character = c;

		age.value = c.Age / c.AgeOfDeath;
		speed.value = (c.Speed - Character.MinSpeed * 1f) / (Character.MaxSpeed - Character.MinSpeed);
		selfish.value = c.Selfish ? 1 : 0;
		hunger.value = c.DaysHungry * 1f / GameManager.instance.gameData.maxDaysHungry;
		hat.color = c.hat;
		vest.color = c.vest;

		Debug.Log("Character Button : " + 
		(c.Age / c.AgeOfDeath) + ", " +
		((c.Speed - Character.MinSpeed * 1f) / (Character.MaxSpeed - Character.MinSpeed)) + ", " +
		(c.Selfish ? 1 : 0) + ", " + 
		(c.DaysHungry * 1f / GameManager.instance.gameData.maxDaysHungry));
	}

	public void SelectThis(){
		Debug.Log("Selected");
		GameManager.instance.gameData.playerCharacter = character;
	}
}
