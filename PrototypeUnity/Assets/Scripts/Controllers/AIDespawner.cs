using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIDespawner : MonoBehaviour
{
	private void OnTriggerEnter2D(Collider2D collision)
	{
		WerewolfController aIController = collision.GetComponent<WerewolfController>();
		if (aIController && aIController.returnHome)
			WerewolfSpawner.Despawn(aIController);
	}
}
