using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface PlayerController
{
	void TeleportHome();
	void Interact();
	void SetTarget(Transform t);
	void WakeUp();
}
