using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatsView : MonoBehaviour
{
	public TMPro.TextMeshProUGUI food;
	public TMPro.TextMeshProUGUI seed;
	public TMPro.TextMeshProUGUI baby;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
		food.text = GameManager.instance.gameData.foodSupply.ToString();
		seed.text = GameManager.instance.gameData.seedAmount.ToString();
		baby.text = GameManager.instance.gameData.characters.Count.ToString();
    }
}
