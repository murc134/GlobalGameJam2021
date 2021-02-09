using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRecognitionTrackingController : Singleton<PlayerRecognitionTrackingController>
{
    public event Action<PlayerRecognitionTracking, DetectingLevel> OnDetectingLevelChangeGlobal;
    public Dictionary<GameObject ,PlayerRecognitionTracking> AllTracker = new Dictionary<GameObject, PlayerRecognitionTracking>();
    public void RegisterTracker(GameObject go, PlayerRecognitionTracking tracker)
    {
        AllTracker.Add(go, tracker);
        tracker.OnDetectingLevelChange += (sign) => CompareDetectionLevelAndInvokeEvent(sign, tracker);
    }

	public void UnRegisterTracker(GameObject go)
	{
        var tracker = AllTracker[go];
        tracker.OnDetectingLevelChange -= (sign) => CompareDetectionLevelAndInvokeEvent(sign, tracker);
        AllTracker.Remove(go);
    }
    private void Awake()
    {
        m_Instance = this;
    }
    private void CompareDetectionLevelAndInvokeEvent(DetectingLevel sign, PlayerRecognitionTracking tracker)
    {
		foreach (var enemy in AllTracker.Values)
		{
            // ignore related enemy
            if (enemy == tracker)
                continue;
            // If some Enemy has allredy a higher level of Discover, no event will rise
            if ( enemy.DetectionStatus > sign)
                return;
        }

        OnDetectingLevelChangeGlobal(tracker, sign);
    }


}
