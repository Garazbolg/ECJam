using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CalendarManager : MonoBehaviour
{
	public Transform lunar;
	public Transform seasons;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
		lunar.localRotation = Quaternion.Euler(Vector3.forward * (360 * (GameManager.instance.gameData.cycles[2].GetPercent())+10));
		seasons.localRotation = Quaternion.Euler(Vector3.forward * 360 * (GameManager.instance.gameData.cycles[1].GetPercent()));
    }
}
