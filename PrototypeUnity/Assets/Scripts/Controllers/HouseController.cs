using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HouseController : MonoBehaviour
{

	public GameObject windowON;
	public GameObject windowOFF;

	private void OnTriggerEnter2D(Collider2D collision)
	{
		WerewolfController ww = collision.GetComponent<WerewolfController>();
		if(ww){
			GameManager.GameOverWW();
		}
	}

	private void Update()
	{
		windowOFF.SetActive(GameManager.instance.gameData.cycles[0].GetPercent() < 0.5);
		windowON.SetActive(GameManager.instance.gameData.cycles[0].GetPercent() > 0.5);
	}
}
