using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeasonsView : MonoBehaviour
{
	public Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
		animator.SetInteger("Stage", GameManager.instance.gameData.cycles[1].currentStage);
    }
}
