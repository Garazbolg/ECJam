using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
	public static AudioManager instance;

	public AudioClip Menu;

	public bool menu = true;

	public AudioClip[] seasons;
	public int currentClip = -2;
	public AudioClip night;

	public AudioClip[] sounds; 

	public AudioSource audc;
	public AudioSource audMusic;
	public AudioSource audMusicNight;

	public AnimationCurve volume;

	private void Awake()
	{
		if (instance) DestroyImmediate(gameObject);
		else
		{
			instance = this;
			DontDestroyOnLoad(gameObject);
		}
	}

    // Update is called once per frame
    void Update()
    {
        if(menu){
			audMusic.clip = Menu;
		}
		else{
			int clip = GameManager.instance.gameData.cycles[1].currentStage;
			if (currentClip != clip)
			{
				audMusic.Stop();
				audMusic.clip = seasons[clip];
				audMusic.Play();
				currentClip = clip;
			}
			audMusic.volume = volume.Evaluate(GameManager.instance.gameData.cycles[0].GetPercent())*0.4f;
			bool ww = (GameManager.instance.gameData.cycles[2].currentStage == 4 && GameManager.instance.gameData.cycles[0].currentStage >= 5);
			audMusicNight.volume = ww ? 0.5f*(1-volume.Evaluate(GameManager.instance.gameData.cycles[0].GetPercent())) : 0;
		}
    }

	public static void PlaySound(int i){
		instance.audc.PlayOneShot(instance.sounds[i]);
	}
}
