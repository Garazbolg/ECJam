using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NightSkyView : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
		transform.localRotation = Quaternion.Euler(Vector3.forward * GameManager.instance.gameData.cycles[2].GetPercent() * 360);
    }
}
