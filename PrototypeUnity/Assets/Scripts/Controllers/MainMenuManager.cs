using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuManager : MonoBehaviour
{

	public float musicVolume = 1f;

    public void GoToScene(string s){
		UnityEngine.SceneManagement.SceneManager.LoadScene(s);
	}

	public void Quit()
	{
#if UNITY_EDITOR
		UnityEditor.EditorApplication.isPlaying = false;
#else
      Application.Quit();
#endif
	}

	private void Update()
	{
		AudioManager.instance.audMusic.volume = musicVolume;
	}
}
