using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugInitializer : MonoBehaviour
{
	public GameManager gameManager;

	public static float dnCycleDurationInSeconds = 50;
	public static int lunarCycleDurationInDays = 8;
	public static int seasonCycleDurationInDays = 96;

	public static int startingFood = 30;
	public static int startingSeed = 5;
	public static int maxHunger = 3;

	public static bool generateSockets = false;
	public static int socketDensity = 10;

	public static int startingWWCount = 5;

	public static Assets.Scripts.Data.Character character;

	private void Awake()
	{
		if(gameManager){
			gameManager.gameData.cycles[0].timePerStage = (dnCycleDurationInSeconds)/ gameManager.gameData.cycles[0].stages.Length;
			gameManager.gameData.cycles[1].timePerStage = (dnCycleDurationInSeconds * seasonCycleDurationInDays) / gameManager.gameData.cycles[1].stages.Length;
			gameManager.gameData.cycles[2].timePerStage = (dnCycleDurationInSeconds * lunarCycleDurationInDays) / gameManager.gameData.cycles[2].stages.Length;

			gameManager.gameData.foodSupply = startingFood;
			gameManager.gameData.seedAmount = startingSeed;
			gameManager.gameData.maxDaysHungry = maxHunger;

			gameManager.gameData.generateSockets = generateSockets;
			gameManager.gameData.socketDensity = socketDensity;
			gameManager.gameData.numberOfWWToSpawn = startingWWCount;

			gameManager.gameData.playerCharacter = character;
		}
	}
}
