using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiscoverSignal : MonoBehaviour
{
	public Material SignalMaterial;
    void Start()
    {
		PlayerRecognitionTrackingController.Instance.OnDetectingLevelChangeGlobal += Instance_OnDetectingLevelChangeGlobal;
    }

	private void Instance_OnDetectingLevelChangeGlobal(PlayerRecognitionTracking enemy, DetectingLevel signal)
	{
		switch (signal)
		{
			case DetectingLevel.OutOfView:
				SignalMaterial.color = Color.black;
				break;
			case DetectingLevel.inDistance:
				SignalMaterial.color = Color.green;
				break;
			case DetectingLevel.close:
				SignalMaterial.color = Color.yellow;
				break;
			case DetectingLevel.EyeContact:
				SignalMaterial.color = Color.red;
				break;
			default:
				break;
		}
	}

}
