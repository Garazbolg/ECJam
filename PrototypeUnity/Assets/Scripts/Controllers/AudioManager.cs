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
	bool ww = false;

	private void Awake()
	{
		if (instance)
		{
			instance.menu = menu;
			DestroyImmediate(gameObject);
		}
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
			if (currentClip != -3)
			{
				audMusicNight.Stop();
				audMusic.clip = Menu;
				audMusic.Play();
				currentClip = -3;
			}
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
			bool newWW= (GameManager.instance.gameData.cycles[2].currentStage == 4 && GameManager.instance.gameData.cycles[0].currentStage >= 5);
			if (ww != newWW)
			{
				ww = newWW;
				if(ww)
					audMusicNight.Play();
				else
					audMusicNight.Stop();
			}
			audMusicNight.volume = ww ? (volume.Evaluate(GameManager.instance.gameData.cycles[0].GetPercent()-0.48f))*0.8f : 0;
			
		}
    }

	public static void PlaySound(int i){
		instance.audc.PlayOneShot(instance.sounds[i]);
	}

	public static void PlaySound(int i, float d){
		instance.StartCoroutine(instance.PlaySoundDelay(i, d));
	}

	IEnumerator PlaySoundDelay(int i, float d){
		yield return new WaitForSecondsRealtime(d);
		PlaySound(i);
	}
}
