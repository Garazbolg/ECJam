using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Data;
using System;

public class GameManager : MonoBehaviour
{
	public static GameManager instance;

	public GameData gameData;

	public PlayerController_Mouse player;

	private bool timescaleLocked = false;
	public float secondsInAYear { get; private set; }

	public bool running = true;

	public bool GoingToBed = true;

	public bool InBed = false;

	public float secondsBeforeNextKid = 1;

	public GameObject gameOverScreen_WW;
	public GameObject gameOverScreen_Extinct;
	public GameObject gameOverExtinctHungerUI;
	public GameObject gameOverExtinctAgeUI;
	public TMPro.TextMeshProUGUI gameOverExtinctText;

	public GameObject debugScreen;

	public GameObject heirManager;

	private void Awake()
	{
		if (instance != null)
			DestroyImmediate(instance);
		instance = this;
	}

	// Start is called before the first frame update
	void Start()
    {
		foreach (Cycle c in gameData.cycles)
			c.Init();
		secondsInAYear = gameData.cycles[1].timePerStage * 4;
		gameData.playerCharacter.Age*= secondsInAYear;
		gameData.playerCharacter.AgeOfDeath*= secondsInAYear;
	}

    // Update is called once per frame
    void Update()
    {
		if (running)
		{
			UpdateTime();
			foreach (Cycle c in gameData.cycles)
				c.Update();
			UpdateAges();
			secondsBeforeNextKid -= Time.deltaTime*Mathf.Clamp(gameData.foodSupply/40f,1,float.PositiveInfinity);
			if (secondsBeforeNextKid < 0)
				NewKid();
		}
		if (Input.GetKeyDown(KeyCode.Space))
			running = !running;
		if (Input.GetKeyDown(KeyCode.K))
			StartCoroutine(KillPlayer(TypeOfDeath.Age));
		if (Input.GetKeyDown(KeyCode.L))
			NewKid();
		if (Input.GetKeyDown(KeyCode.D))
			debugScreen.SetActive(!debugScreen.activeSelf);
		if (Input.GetKeyDown(KeyCode.Escape))
			GoToScene("MenuDynamic");
	}

	public static void Run(Script script){
		if (string.IsNullOrEmpty(script.script) || !instance.running) return;
		Debug.Log("Running : " + script.script);
		string[] instructions = script.script.Split('\n');
		foreach(string command in instructions){
			string[] word = command.Split(" ".ToCharArray(), System.StringSplitOptions.RemoveEmptyEntries);
			switch(word[0]){
				case "food":
					int val = int.Parse(word[1]);
					instance.gameData.foodSupply += val;
					if (instance.gameData.foodSupply < 0)
						instance.gameData.foodSupply = 0;
					break;

				case "seed":
					int val3 = int.Parse(word[1]);
					instance.gameData.seedAmount += val3;
					if (instance.gameData.seedAmount < 0)
						instance.gameData.seedAmount = 0;
					break;

				case "spawnwolves":
					if (instance.gameData.cycles[2].currentStage == 4)
						WerewolfSpawner.SpawnRaid();
					break;

				case "despawnwolves":
					WerewolfSpawner.DespawnRaid();
					break;

				case "newday":

					AudioManager.PlaySound(7);
					foreach (Assets.Scripts.Data.SeedSocket ss in instance.gameData.sockets)
						ss.NewDay();
					break;

				case "werewolves":
					int valW = int.Parse(word[1]);
					instance.gameData.numberOfWWToSpawn += valW;
					break;

				case "eat":
					instance.Eat();
					break;

				case "sleepsound":
					AudioManager.PlaySound(10);
					break;
					
				case "nightsound":
					//AudioManager.PlaySound(14);
					break;

				case "bed":
					AudioManager.PlaySound(13);
					instance.GoingToBed = word[1].CompareTo("1") == 0;
					if (!instance.GoingToBed)
					{
						instance.InBed = false;
						instance.player.WakeUp();
					}
					else if(instance.gameData.cycles[2].currentStage != 3)
						AudioManager.PlaySound(14, 2);
					break;

				default:break;
			}
		}
	}

	public void UpdateAges(){
		for(int i = gameData.characters.Count - 1; i>=0;i--)
		{
			Character c = gameData.characters[i];
			c.Age += Time.deltaTime;
			if (c.Age > c.AgeOfDeath)
			{
				gameData.characters.Remove(c);
				KillKid();
			}
		}
		if(gameData.playerCharacter != null)
			gameData.playerCharacter.Age += Time.deltaTime;
		if (gameData.playerCharacter.Age > gameData.playerCharacter.AgeOfDeath)
			StartCoroutine(KillPlayer(TypeOfDeath.Age));
	}

	public static void InitSockets(int numberOfSockets){
		instance.gameData.sockets = new Assets.Scripts.Data.SeedSocket[numberOfSockets];
		for (int i = 0; i < numberOfSockets; i++)
			instance.gameData.sockets[i] = new Assets.Scripts.Data.SeedSocket();
	}

	public static void NextSeed(){
		instance.gameData.selectedSeed = (instance.gameData.selectedSeed + 1) % CropsManager.instance.cropTypes.Length;
	}

	public void NewKid(){
		// TODO Baby sound
		gameData.characters.Add(new Character(gameData.foodSupply));
		secondsBeforeNextKid = (0.1f + (1-Mathf.Clamp01(GameManager.instance.gameData.foodSupply/20f)) * 0.5f) * secondsInAYear;
	}

	public void Eat(){
		AudioManager.PlaySound(4);
		if (gameData.playerCharacter.Selfish)
			if (!Feed(gameData.playerCharacter))
				StartCoroutine(KillPlayer(TypeOfDeath.Hunger));
		for(int i = gameData.characters.Count -1; i>=0;i--)
		{
			if (!Feed(gameData.characters[i]))
			{
				gameData.characters.RemoveAt(i);
				KillKid();
			}
		}
		if (!gameData.playerCharacter.Selfish)
			if (!Feed(gameData.playerCharacter))
				StartCoroutine(KillPlayer(TypeOfDeath.Hunger));
	}

	public void KillKid(){
		AudioManager.PlaySound(1);
	}

	public void UpdateTime(){
		Time.timeScale = Mathf.Lerp(Time.timeScale,
									(InBed && gameData.cycles[2].currentStage != 4) ? 5 : 1,
									Time.unscaledDeltaTime * 10);
	}

	public bool Feed(Character c){
		gameData.foodSupply -= 1; // Should increase with the number of children
		if (gameData.foodSupply <= 0)
		{
			gameData.foodSupply = 0;
			c.DaysHungry++;
			if (c.DaysHungry >= gameData.maxDaysHungry)
				return false;
		}
		return true;
	}

	public void GoToScene(string n){
		UnityEngine.SceneManagement.SceneManager.LoadScene(n);
	}

	public static void GameOverWW(){
		instance.StartCoroutine(instance.GameOver_WW());
		
	}

	IEnumerator GameOver_WW()
	{
		running = false;
		AudioManager.PlaySound(8);

		AudioManager.PlaySound(6);
		Time.timeScale = 1;
		gameOverScreen_WW.SetActive(true);
		yield return null;
	}

	IEnumerator GameOver_Extinct(TypeOfDeath t)
	{
		running = false;
		AudioManager.PlaySound(9);
		Time.timeScale = 1;
		switch (t)
		{
			case TypeOfDeath.Hunger:
				gameOverExtinctHungerUI.SetActive(true);
				gameOverExtinctText.text = "Without enough food, you lose the energy to work.\nUltimately you let starvation take you";
				break;
			case TypeOfDeath.Age:
				gameOverExtinctAgeUI.SetActive(true);
				gameOverExtinctText.text = "You died of old age.\nWith no one to carry on your name and your work,\nyour crops, your legacy, turn to ash,\nclaimed by the sands of time.";
				break;
		}
		gameOverScreen_Extinct.SetActive(true);
		yield return null;
	}

	private enum TypeOfDeath{ Hunger, Age}

	IEnumerator KillPlayer(TypeOfDeath t)
	{
		Debug.LogWarning("Kill Player");
		AudioManager.PlaySound(6);
		if (gameData.characters.Count < 1)
		{
			StartCoroutine(GameOver_Extinct(t));
		}
		else
		{
			running = false;
			Time.timeScale = 1;
			//TODO : Animate Player Death

			player.TeleportHome();
			gameData.playerCharacter = null;

			heirManager.SetActive(true);

			while(gameData.playerCharacter == null){
				yield return null;
			}
			
			heirManager.SetActive(false);
			gameData.characters.Remove(gameData.playerCharacter);

			player.SetCharacter(gameData.playerCharacter);
			gameData.hadKids = false;
			yield return null;
			running = true;
		}
	}
}
