using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleManager : MonoBehaviour
{

	public ParticleSystem baby;
	public ParticleSystem death;
	public Animator characterStat;
	public int nbCharacters;
	public ParticleSystem foodgain;
	public ParticleSystem foodloss;
	public Animator foodStat;
	public int nbFood;
	public ParticleSystem seed;
	public Animator seedStat;
	public int nbSeed;

    // Start is called before the first frame update
    void Start()
    {
		nbCharacters = GameManager.instance.gameData.characters.Count;
		nbFood = GameManager.instance.gameData.foodSupply;
		nbSeed = GameManager.instance.gameData.seedAmount;
	}

    // Update is called once per frame
    void Update()
    {
        if(nbCharacters != GameManager.instance.gameData.characters.Count){
			int c = GameManager.instance.gameData.characters.Count;
			int diff = c - nbCharacters;
			nbCharacters = c;
			if (diff > 0)
			{
				StartCoroutine(Emit(baby, diff));
			}
			else
			{
				StartCoroutine(Emit(death, -diff));
			}
			characterStat.SetTrigger("Changed");
		}
		if (nbFood != GameManager.instance.gameData.foodSupply)
		{
			int c = GameManager.instance.gameData.foodSupply;
			int diff = c - nbFood;
			nbFood = c;
			if (diff > 0)
			{
				StartCoroutine(Emit(foodgain,diff));
			}
			else
			{
				StartCoroutine(Emit(foodloss, -diff));
			}

			foodStat.SetTrigger("Changed");
		}
		if (nbSeed != GameManager.instance.gameData.seedAmount)
		{
			int c = GameManager.instance.gameData.seedAmount;
			int diff = c - nbSeed;
			nbSeed = c;
			if (diff > 0)
			{
				StartCoroutine(Emit(seed, diff));
			}

			seedStat.SetTrigger("Changed");
		}
	}

	IEnumerator Emit(ParticleSystem ps, int n){
		for(int i = 0; i< n;i++){
			ps.Emit(1);
			yield return new WaitForSecondsRealtime(0.1f);
		}
	}
}
