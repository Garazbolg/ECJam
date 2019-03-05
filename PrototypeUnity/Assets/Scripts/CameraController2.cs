using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController2 : MonoBehaviour
{

	public float offsetScale = 2;
	public float zOffset = -10;

	public GameObject sky;
	public float skyOffsetScale = -1.1f;

	public GameObject SunAndMoon;
	public float sunmoonscale = 3;

	public Vector3 planetOffset;

	public float lerpSpeed = 5f;

	private Vector3 targetPosition;
	private Vector3 skyTargetPosition;
	private Vector3 sunTargetPosition;

	private Vector3 sunOffset;
	
    // Start is called before the first frame update
    void Start()
    {
		sunOffset = SunAndMoon.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
		Vector3 m = Input.mousePosition;
		m.x = (m.x * 2 / Screen.height) - 1;
		m.y = (m.y * 2 / Screen.height) - 1;

		targetPosition = Planet.instance.transform.position + planetOffset + m*offsetScale + Vector3.forward * zOffset;
		skyTargetPosition = Planet.instance.transform.position + planetOffset + m * skyOffsetScale + Vector3.forward * 100;
		sunTargetPosition = sunOffset + m * sunmoonscale + Vector3.forward * 30;

		transform.position = Vector3.Lerp(transform.position, targetPosition, Time.unscaledDeltaTime * lerpSpeed);
		sky.transform.position = Vector3.Lerp(sky.transform.position, skyTargetPosition, Time.unscaledDeltaTime * lerpSpeed);
		SunAndMoon.transform.position = Vector3.Lerp(SunAndMoon.transform.position, sunTargetPosition, Time.unscaledDeltaTime * lerpSpeed);
	}
}
