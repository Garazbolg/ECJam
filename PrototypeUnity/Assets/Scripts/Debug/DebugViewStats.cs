using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(TMPro.TextMeshProUGUI))]
public class DebugViewStats : MonoBehaviour
{
	TMPro.TextMeshProUGUI textMesh;

	// Start is called before the first frame update
	void Start()
	{
		textMesh = GetComponent<TMPro.TextMeshProUGUI>();
	}

	// Update is called once per frame
	void Update()
	{
		string debugText = "";

		debugText += "Max Days hungry : " + GameManager.instance.gameData.maxDaysHungry + "\n\n";


		debugText += "Food Supplies : " + GameManager.instance.gameData.foodSupply + "\n";
		debugText += "Seed Amount : " + GameManager.instance.gameData.seedAmount + "\n";
		debugText += "Seconds before next Kid : " + GameManager.instance.secondsBeforeNextKid.ToString("N2") + "\n";
		debugText += "Number of werewolves per night : " + GameManager.instance.gameData.numberOfWWToSpawn + "\n\n";

		if(GameManager.instance.gameData.playerCharacter != null)
			debugText += "Character Stats : \n  Speed : " + GameManager.instance.gameData.playerCharacter.Speed + "\n  Selfish : " + GameManager.instance.gameData.playerCharacter.Selfish + "\n  Age : " + (GameManager.instance.gameData.playerCharacter.Age/GameManager.instance.secondsInAYear).ToString("N2") + "\n  Age Of Death : " + (GameManager.instance.gameData.playerCharacter.AgeOfDeath / GameManager.instance.secondsInAYear).ToString("N2") + "\n\n";

		debugText += "Selected seed ID : " + CropsManager.instance.cropTypes[GameManager.instance.gameData.selectedSeed].name + "\n\n";

		debugText += "Children ( "+GameManager.instance.gameData.characters.Count+" ): \n";
		foreach(Assets.Scripts.Data.Character c in GameManager.instance.gameData.characters){
			debugText += "  ( " + c.Speed +", " + (c.Age/GameManager.instance.secondsInAYear).ToString("N2") + " / " + (c.AgeOfDeath / GameManager.instance.secondsInAYear).ToString("N2") + ", " + c.DaysHungry + ")\n";
		}

		textMesh.text = debugText;
	}
}