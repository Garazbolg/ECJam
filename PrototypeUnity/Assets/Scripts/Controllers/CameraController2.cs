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
	public float lerpTargetValue = 2f;
	public float lerpAcceleration = 0.2f;

	private Vector3 targetPosition;
	private Vector3 skyTargetPosition;
	private Vector3 sunTargetPosition;

	private Vector3 sunOffset;

	public float size = 13.52f;
	public Camera cam;

	public static Vector3 lastPosition = new Vector3(0, -24.2f, -9.16f);
	
    // Start is called before the first frame update
    void Start()
    {
		sunOffset = SunAndMoon.transform.position;
		transform.position = lastPosition;
    }

    // Update is called once per frame
    void Update()
    {
		lerpSpeed = Mathf.Lerp(lerpSpeed, lerpTargetValue, Time.unscaledDeltaTime * lerpAcceleration);

		Vector3 m = Input.mousePosition;
		m.x = (m.x * 2 / Screen.height) - 1;
		m.y = (m.y * 2 / Screen.height) - 1;

		targetPosition = Planet.instance.transform.position + planetOffset + m*offsetScale + Vector3.forward * zOffset;
		skyTargetPosition = Planet.instance.transform.position + planetOffset + m * skyOffsetScale + Vector3.forward * 100;
		sunTargetPosition = sunOffset + m * sunmoonscale + Vector3.forward * 30;
		cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, size, Time.unscaledDeltaTime * lerpSpeed);

		transform.position = Vector3.Lerp(transform.position, targetPosition, Time.unscaledDeltaTime * lerpSpeed);
		sky.transform.position = Vector3.Lerp(sky.transform.position, skyTargetPosition, Time.unscaledDeltaTime * lerpSpeed);
		SunAndMoon.transform.position = Vector3.Lerp(SunAndMoon.transform.position, sunTargetPosition, Time.unscaledDeltaTime * lerpSpeed);
	}

	private void OnDestroy()
	{
		lastPosition = transform.position;
	}
}
