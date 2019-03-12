using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIController : CharacterControlller
{
	public Transform target;
	public float selfAngle = 0;
	public float targetAngle = 0;
	public bool returnHome = false;
	public bool jumpOnTarget = false;

	// Update is called once per frame
	protected override void Update()
    {
		base.Update();
		if (target)
		{
			selfAngle = Planet.GetAngle(transform.position);
			targetAngle = 0;

			if (returnHome)
				target = transform.parent;

			if (target != null)
				targetAngle = Planet.GetAngle(target.position);

			leftHandle = selfAngle > targetAngle;
			rightHandle = selfAngle < targetAngle;

			if (jumpOnTarget)
			{
				if (Mathf.Abs(selfAngle - targetAngle) < WerewolfSpawner.instance.jumpAngle)
					jumpHandle = true;
				else
					jumpHandle = false;
			}
			else
				jumpHandle = false;
		}
		else
		{
			leftHandle = false;
			rightHandle = false;
			jumpHandle = false;
		}
    }

	private void OnDrawGizmos()
	{
		Gizmos.color = Color.magenta;
		if(target)
			Gizmos.DrawLine(transform.position, target.position);
	}
}
