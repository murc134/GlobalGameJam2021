using UnityEngine;

public class RegisteredTracker : MonoBehaviour
{
	private void OnEnable()
	{
		PlayerRecognitionTrackingController.Instance.RegisterTracker(gameObject, (PlayerRecognitionTracking)this);
	}
	private void OnDisable()
	{
		if (PlayerRecognitionTrackingController.Instance == null)
			return;
		PlayerRecognitionTrackingController.Instance.UnRegisterTracker(gameObject);
	}
}