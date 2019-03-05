using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeirManager : MonoBehaviour
{
	public Transform container;
	public GameObject prefab;
	public List<CharacterViewButton> buttons = new List<CharacterViewButton>();
	public int MaxButtons = 10;

	private void Start()
	{
		buttons.Clear();
	}

	private void OnDisable()
	{
		foreach(CharacterViewButton g in buttons){
			Destroy(g.gameObject);
		}
		buttons.Clear();
	}

	private void OnEnable()
	{
		int i = 0;
		foreach(Assets.Scripts.Data.Character c in GameManager.instance.gameData.characters)
		{
			if (i >= MaxButtons) break;
			CharacterViewButton go = Instantiate(prefab, container).GetComponent<CharacterViewButton>();
			go.Init(c);
			buttons.Add(go);
			i++;
		}
	}
}
