using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SunMoonView : MonoBehaviour
{
	public SpriteMask moonMask;

	public Sprite[] moonPhasesMasks;
	
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
		transform.rotation = Quaternion.Euler(Vector3.forward * (-75 + 360 * (GameManager.instance.gameData.cycles[0].GetPercent())));
		moonMask.sprite = moonPhasesMasks[GameManager.instance.gameData.cycles[2].currentStage];
    }
}
