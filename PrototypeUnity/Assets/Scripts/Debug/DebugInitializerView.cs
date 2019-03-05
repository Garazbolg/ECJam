using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DebugInitializerView : MonoBehaviour
{
	public TMP_InputField dayDuration;
	public TMP_InputField monthDuration;
	public TMP_InputField yearDuration;

	public TMP_InputField startingFood;
	public TMP_InputField startingSeed;
	public TMP_InputField maxHunger;
	public Toggle generate;
	public TMP_InputField socketDensity;
	public TMP_InputField startingWWCount;
	
	public TMP_InputField charAge;
	public Toggle selfish;
	public TMP_InputField charSpeed;
	public TMP_InputField charAOD;

	private bool init = false;

	public void Start()
	{
		
		dayDuration.text = DebugInitializer.dnCycleDurationInSeconds.ToString();
		monthDuration.text = DebugInitializer.lunarCycleDurationInDays.ToString();
		yearDuration.text = DebugInitializer.seasonCycleDurationInDays.ToString();

		startingFood.text = DebugInitializer.startingFood.ToString();
		startingSeed.text = DebugInitializer.startingSeed.ToString();
		maxHunger.text = DebugInitializer.maxHunger.ToString();
		generate.isOn = DebugInitializer.generateSockets;
		socketDensity.text = DebugInitializer.socketDensity.ToString();
		startingWWCount.text = DebugInitializer.startingWWCount.ToString();

		DebugInitializer.character = new Assets.Scripts.Data.Character();

		charAge.text = DebugInitializer.character.Age.ToString();
		charSpeed.text = DebugInitializer.character.Speed.ToString();
		selfish.isOn = DebugInitializer.character.Selfish;
		charAOD.text = DebugInitializer.character.AgeOfDeath.ToString();
		init = true;
	}

	// Update is called once per frame
	public void UpdateValues(string value)
    {
		if (!init) return;
		DebugInitializer.dnCycleDurationInSeconds = int.Parse(dayDuration.text);
		DebugInitializer.lunarCycleDurationInDays = int.Parse(monthDuration.text);
		DebugInitializer.seasonCycleDurationInDays = int.Parse(yearDuration.text);

		DebugInitializer.startingFood = int.Parse(startingFood.text);
		DebugInitializer.startingSeed = int.Parse(startingSeed.text);
		DebugInitializer.maxHunger = int.Parse(maxHunger.text);
		DebugInitializer.generateSockets = generate.isOn;
		DebugInitializer.socketDensity = int.Parse(socketDensity.text);
		DebugInitializer.startingWWCount = int.Parse(startingWWCount.text);

		DebugInitializer.character.Age = int.Parse(charAge.text);
		DebugInitializer.character.Speed = int.Parse(charSpeed.text);
		DebugInitializer.character.Selfish = selfish.isOn;
		DebugInitializer.character.AgeOfDeath = int.Parse(charAOD.text);
    }

	public void UpdateValues(bool val){
		UpdateValues("");
	}

	public void GoToScene(string level){
		UnityEngine.SceneManagement.SceneManager.LoadScene(level);
	}
}
