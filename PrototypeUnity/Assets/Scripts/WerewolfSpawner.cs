using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Misc;

public class WerewolfSpawner : MonoBehaviour
{
	public static WerewolfSpawner instance;
	
	public float minInterval = 0.5f;
	public float maxInterval = 3f;

	public float jumpAngle = 20;

	private Transform[] spawnPoints;

	public GameObject werewolfPrefab;

	private List<AIController> aIControllers = new List<AIController>();

	private void Awake()
	{
		if (instance != null)
			DestroyImmediate(instance);
		instance = this;
	}

	// Start is called before the first frame update
	void Start()
    {
		spawnPoints = GetComponentsInChildren<Transform>().Next();
		
    }

    // Update is called once per frame
    void Update()
    {
        
    }


	public static void SpawnRaid(){
		Debug.LogWarning("LauchRaid");
		instance.StartCoroutine(instance.SpawnCoroutine(GameManager.instance.gameData.numberOfWWToSpawn));
	}

	public void SpawnOne(){
		Debug.Log("Spawn");
		AIController aIController = Instantiate(werewolfPrefab, Vector3.zero, Quaternion.identity).GetComponent<AIController>();
		aIController.transform.SetParent(spawnPoints[Random.Range(0, spawnPoints.Length)], false);
		aIController.target = Planet.instance.house;
		aIControllers.Add(aIController);
	}


	public static void DespawnRaid()
	{
		foreach (AIController aic in instance.aIControllers)
		{
			aic.returnHome = true;
		}
	}

	public static void Despawn(AIController aic){
		instance.aIControllers.Remove(aic);
		GameObject.Destroy(aic.gameObject);
	}

	IEnumerator SpawnCoroutine(int n){
		int i = 0;
		float time = 0;
		while(i<n){
			time -= Time.deltaTime;
			if(time <= 0){
				time = Random.Range(minInterval, maxInterval) - time;
				SpawnOne();
				i++;
			}
			yield return null;
		}
	}
}
