using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Animator))]
public class CropView : MonoBehaviour
{
	public Animator anim;
	public Transform heartContainer;
	private List<GameObject> heartsObjects = new List<GameObject>();
	
    // Start is called before the first frame update
    void Start()
    {
		GetComponentInChildren<Canvas>().worldCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

	public void UpdateStage(int stage){
		anim.SetInteger("Stage", stage);
	}

	public void SetHealth(int v){
		int diff = v - heartsObjects.Count;
		if(diff>0)
		{
			for (int i = 0; i < diff; i++)
				heartsObjects.Add(Instantiate(CropsManager.instance.heartPrefab, heartContainer));
		}
		else if(diff < 0)
		{
			anim.SetTrigger("Hurt");
			for (int i = 0; i < -diff; i++)
			{
				Destroy(heartsObjects[0]);
				heartsObjects.RemoveAt(0);
			}
		}
	}

	public void DestroyIt(){
		Destroy(gameObject);
	}
}
