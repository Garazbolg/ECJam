using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(TMPro.TextMeshProUGUI))]
public class DebugViewCycles : MonoBehaviour
{
	TMPro.TextMeshProUGUI textMesh;

	// Start is called before the first frame update
	void Start()
    {
		textMesh = GetComponent<TMPro.TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
		string debugCycles = "";
		foreach (Cycle c in GameManager.instance.gameData.cycles)
		{
			debugCycles += c.ToString() + "\n";
		}
		textMesh.text = debugCycles;
	}
}
