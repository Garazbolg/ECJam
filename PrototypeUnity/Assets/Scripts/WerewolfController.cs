using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WerewolfController : AIController
{
	private void OnTriggerEnter2D(Collider2D collision)
	{
		if(collision.name.CompareTo("House") == 0)
		{
			GameManager.GameOverWW();
		}
	}
}
