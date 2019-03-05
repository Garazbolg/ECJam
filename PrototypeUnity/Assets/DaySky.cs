using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DaySky : MonoBehaviour
{
	public Animator anim;
	
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
		anim.SetFloat("time", GameManager.instance.gameData.cycles[0].GetPercent());
    }
}
