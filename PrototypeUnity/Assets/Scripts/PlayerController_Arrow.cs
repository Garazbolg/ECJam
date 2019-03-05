using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController_Arrow : AIController, PlayerController
{
	private SeedSocketView currentSocket;

	public int selectedSeed = 0;
    // Start is called before the first frame update
    protected override void Start()
    {
		base.Start();
		character = GameManager.instance.gameData.playerCharacter;
    }

    // Update is called once per frame
    protected override void Update()
    {
		base.Update();
		if (!target)
		{
			leftHandle = Input.GetKey(KeyCode.LeftArrow);
			rightHandle = Input.GetKey(KeyCode.RightArrow);
			jumpHandle = Input.GetKeyDown(KeyCode.UpArrow);
			if (Input.GetKeyDown(KeyCode.DownArrow) && currentSocket != null && selectedSeed != -1)
			{
				Interact();
			}
		}
		if (GameManager.instance.GoingToBed)
			target = Planet.instance.house;
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		SeedSocketView seedSocketView = collision.GetComponent<SeedSocketView>();
		if(seedSocketView){
			currentSocket = seedSocketView;
		}
	}

	private void OnTriggerExit2D(Collider2D collision)
	{
		SeedSocketView seedSocketView = collision.GetComponent<SeedSocketView>();
		if (seedSocketView)
		{
			currentSocket = null;
		}
	}

	public void Interact(){
		if (currentSocket != null)
		{
			if (currentSocket.seedSocket.plantID != -1)
				currentSocket.Harvest();
			else
				currentSocket.PlantSeed(selectedSeed);
		}
	}

	public void TeleportHome()
	{
		gameObject.SetActive(false);
		transform.localPosition = Vector3.zero;
		transform.localRotation = Quaternion.identity;
	}

	public void SetTarget(Transform t)
	{
		target = t;
	}

	public void WakeUp()
	{
		target = null;
		gameObject.SetActive(true);
	}
}
