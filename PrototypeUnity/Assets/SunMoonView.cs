using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SunMoonView : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
		transform.rotation = Quaternion.Euler(Vector3.forward * (-75 + 360 * (GameManager.instance.gameData.cycles[0].GetPercent())));
    }
}
