using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Cycle
{
	[System.Serializable]
	public struct Stage{
		public string name;
		public Script onStart;
		public Script onEnd;
	}

	public string name;
	public float timePerStage;
	public Stage[] stages;
	public int startingStage;

	public int currentStage { get; private set; }
	private float currentTime = 0;

	public void Init(){
		currentStage = startingStage;
		currentTime = 0;
		GameManager.Run(stages[currentStage].onStart);
	}

	public void Update(){
		currentTime += Time.deltaTime;
		if(currentTime > timePerStage)
		{
			currentTime -= timePerStage;
			GameManager.Run(stages[currentStage].onEnd);
			currentStage = (currentStage + 1) % stages.Length;
			GameManager.Run(stages[currentStage].onStart);
		}
	}

	public override string ToString()
	{
		return name + " : " + stages[currentStage].name + "(" + currentTime.ToString("N1") + "/" + timePerStage.ToString("N0") + ")";
	}

	public string GetCurrentStageName(){
		return stages[currentStage].name;
	}

	public float GetPercent(){
		return (currentStage + currentTime/timePerStage)/(stages.Length);
	}
}
