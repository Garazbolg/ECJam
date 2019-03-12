using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeirManager : MonoBehaviour
{
	public Transform container;
	public GameObject prefab;
	public int MaxButtons = 10;
	
	private void OnDisable()
	{
		foreach(Transform g in container.transform){
			Destroy(g.gameObject);
		}
	}

	private void OnEnable()
	{
		int i = 0;

		foreach(Assets.Scripts.Data.Character c in GameManager.instance.gameData.characters)
		{
			if (i >= MaxButtons) break;
			CharacterViewButton go = Instantiate(prefab, container).GetComponent<CharacterViewButton>();
			go.Init(c);
			i++;
		}
	}
}
