using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DetectingLevel { OutOfView, inDistance, close, EyeContact }
public class PlayerRecognitionTracking : MonoBehaviour
{
    public event Action<DetectingLevel> OnDetectingLevelChange;
    public DetectingLevel DetectionStatus = DetectingLevel.OutOfView;
    public float MaximumDistance = 12f;
    public float CloseDistance = 3f;

    public float ViewFieldOffset = 0.8f;
    public float DistanceToPlayer;
    public float LookScore;
    public Player MainPlayer;
    public GameObject Head;

	private void Awake()
	{
        Player.OnInstantiated += (ply) => MainPlayer = ply;
    }
	void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        DistanceToPlayer = Vector3.Distance(Head.transform.position, MainPlayer.transform.position);

        LookScore = GetLookAtScore();

        // Check level of Distance
        DetectingLevel Status;
        if(DistanceToPlayer < MaximumDistance)
		{
            if(DistanceToPlayer < CloseDistance)
			{
                Status = DetectingLevel.close;
            }
			else
			{
                Status = DetectingLevel.inDistance;
            }

		}
		else
		{
            Status = DetectingLevel.OutOfView;
        }

        // Check View direction
        if (LookScore > 1 - ViewFieldOffset)
		{
            if (Physics.Raycast(Head.transform.position , MainPlayer.transform.position - Head.transform.position, out RaycastHit hit, MaximumDistance))
            {
                if (hit.collider.tag == "Player")
                    Status = DetectingLevel.EyeContact;
            }
            else
		    {
                Status = DetectingLevel.close;
            }
		}

        if(Status != DetectionStatus)
		{
            DetectionStatus = Status;
            OnDetectingLevelChange?.Invoke(Status);
        }
    }


	/// <summary>
	/// Get View Track in percent, 1 meens direct in front
	/// </summary>
	/// <returns>returns a score between [-1,1] where 1 means the Enemy is looking directly at this object right now, -1 means directly looking away.</returns>
	/// <remarks>https://github.com/osmosacademy/vr-passport/blob/95a94c91d907a921c0a63ec41b76a0a70f4b80d0/Assets/Scripts/Vision/VisionTracker.cs</remarks>
	public float GetLookAtScore()
    {
        Vector3 playerToObject = MainPlayer.transform.position - Head.transform.position;

        //if (maximumDistance != 0 && playerToObject.sqrMagnitude > maximumDistanceSquared)
        //    return -1;

        playerToObject.Normalize();
        Vector3 lookDirection = transform.forward;

        return Vector3.Dot(lookDirection, playerToObject);
    }

    void OnDrawGizmosSelected()
    {
		switch (DetectionStatus)
		{
			case DetectingLevel.OutOfView:
                return;
			case DetectingLevel.inDistance:
                Gizmos.color = Color.green;
                break;
			case DetectingLevel.close:
                Gizmos.color = Color.yellow;
                break;
			case DetectingLevel.EyeContact:
                Gizmos.color = Color.red;
                break;
			default:
				break;
		}

        Vector3 direction = (MainPlayer.transform.position - Head.transform.position);
        Gizmos.DrawRay(Head.transform.position, direction);
    }
}
