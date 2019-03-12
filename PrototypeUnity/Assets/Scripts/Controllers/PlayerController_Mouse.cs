using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController_Mouse : AIController, PlayerController
{
	public int selectedSeed = 0;
	public LayerMask Targetable;

	public SpriteRenderer hat;
	public SpriteRenderer torso;


	// Start is called before the first frame update
	protected override void Start()
	{
		base.Start();
		SetCharacter(GameManager.instance.gameData.playerCharacter);
	}

	protected override void Update()
	{
		if (Input.GetMouseButtonUp(0))
		{
			Collider2D col = Physics2D.OverlapPoint(Camera.main.ScreenToWorldPoint(Input.mousePosition), Targetable);
			if (col != null)
			{
				SetTarget(col.transform);
			}
		}
		if (GameManager.instance.GoingToBed)
			target = Planet.instance.house;
		base.Update();
	}

	private void OnTriggerStay2D(Collider2D collision)
	{
		if (collision.transform == target)
		{
			Interact();
			target = null;
		}
	}

	public void Interact()
	{
		SeedSocketView currentSocket = target.GetComponent<SeedSocketView>();
		if (currentSocket != null)
		{
			if (currentSocket.seedSocket.plantID != -1)
				currentSocket.Harvest();
			else
				currentSocket.PlantSeed(selectedSeed);
		}
		else{
			if (target == Planet.instance.house)
			{
				GameManager.instance.InBed = true;
				gameObject.SetActive(false);
				AudioManager.PlaySound(3);
			}
		}
	}

	public void TeleportHome()
	{
		gameObject.SetActive(false);
		transform.localPosition = Vector3.forward * -10;
		transform.localRotation = Quaternion.identity;
		GameManager.instance.InBed = true;
	}

	public void SetTarget(Transform t)
	{
		target = t;
		if (!gameObject.activeSelf)
			WakeUp();
	}

	public void WakeUp()
	{
		gameObject.SetActive(true);
		target = null;
	}

	public void Step(){
		switch (GameManager.instance.gameData.cycles[1].currentStage)
		{
			case 0:
				AudioManager.PlaySound(11);
				break;
			case 1:
				AudioManager.PlaySound(11);
				break;
			case 2:
				AudioManager.PlaySound(0);
				break;
			case 3:
				AudioManager.PlaySound(12);
				break;
			default:
				break;
		}
	}

	public void SetCharacter(Assets.Scripts.Data.Character chara){
		this.character = chara;
		hat.color = character.hat;
		torso.color = character.vest;
	}
}