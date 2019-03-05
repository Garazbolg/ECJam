using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Seed Socket View
public class SeedSocketView : MonoBehaviour
{
	public Assets.Scripts.Data.SeedSocket seedSocket;

	private int plantID = -1;
	private CropView cropView;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
		if (seedSocket != null)
		{
			if (plantID != seedSocket.plantID)
			{
				plantID = seedSocket.plantID;
				if (plantID == -1)
					Destroy(cropView.gameObject);
				else
				{
					cropView = Instantiate(CropsManager.GetCropType(plantID).viewPrefab, Vector3.back, Quaternion.identity).GetComponent<CropView>();
					cropView.transform.SetParent(transform, false);
				}
			}

			if (plantID != -1)
			{
				cropView.UpdateStage(seedSocket.plantGrowStage);
				cropView.SetHealth(seedSocket.GetHealth());
			}
		}
    }

	private void OnDrawGizmos()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere(transform.position, 0.5f);
	}

	public void SetSocket(Assets.Scripts.Data.SeedSocket seedSocket){
		this.seedSocket = seedSocket;
	}

	public void PlantSeed(int ID){
		if (GameManager.instance.gameData.seedAmount > 0)
		{
			GameManager.instance.gameData.seedAmount--;
			seedSocket.AddPlant(ID);
			AudioManager.PlaySound(5);
		}
	}

	public void Harvest(){
		seedSocket.Harvest();
		AudioManager.PlaySound(2);
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		WerewolfController werewolfController = collision.GetComponent<WerewolfController>();
		if(werewolfController && plantID != -1){
			RunContact(CropsManager.GetCropType(seedSocket.plantID).cropStages[seedSocket.plantGrowStage].OnWerewolfContact, seedSocket, werewolfController);
		}
	}

	public void RunContact(Script script, Assets.Scripts.Data.SeedSocket socket, WerewolfController ww)
	{
		if (string.IsNullOrEmpty(script.script)) return;
		Debug.Log("Running : " + script.script);
		string[] instructions = script.script.Split('\n');
		foreach (string command in instructions)
		{
			string[] word = command.Split(" ".ToCharArray(), System.StringSplitOptions.RemoveEmptyEntries);
			switch (word[0])
			{
				case "kill":
					WerewolfSpawner.Despawn(ww);
					break;

				case "destroy":
					socket.plantID = -1;
					break;

				case "damage":
					socket.Damage();
					break;
			}
		}
	}
}
