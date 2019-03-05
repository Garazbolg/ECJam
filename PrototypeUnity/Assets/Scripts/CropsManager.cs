using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CropsManager : MonoBehaviour
{
	public static CropsManager instance;
	
	public Assets.Scripts.Data.CropType[] cropTypes;

	public GameObject heartPrefab;

	private void Awake()
	{
		if (instance != null)
			DestroyImmediate(instance);
		instance = this;
	}

	public static Assets.Scripts.Data.CropType GetCropType(int ID){
		return instance.cropTypes[ID];
	}
}
