using UnityEngine;

public class PointerInfo
{
    public bool HasReceiver
    {
        get
        {
            return Receiver != null;
        }
    }
    private ClickEventHandler receiver;
    public ClickEventHandler Receiver
    {
        get
        {
            return receiver;
        }
        private set
        {
            receiver = value;
        }
    }
    public Vector3 OriginalMousePos;
    public Vector3 HitPos { get => hit.point; }
    public Vector3 HitTransformPos { get => hit.transform.position; }
    public GameObject HitGameObject { get => hit.transform.gameObject; }
    public Transform HitTransform { get => hit.transform; }

    private RaycastHit hit;

    private float time;

    private float maxLifetime = 0.5f;

    private Vector3 mouseMovementDelta = CameraMousePointer.INVALIDINPUT;

    private static Vector3 previousMousepos = CameraMousePointer.INVALIDINPUT;

    public Vector3 MouseDelta => mouseMovementDelta == CameraMousePointer.INVALIDINPUT ? Vector3.zero : mouseMovementDelta;

    public PointerInfo(Vector3 origMousePos, RaycastHit newhit)
    {
        if (previousMousepos == CameraMousePointer.INVALIDINPUT)
        {
            previousMousepos = origMousePos;
        }

        mouseMovementDelta = previousMousepos - origMousePos;

        Receiver = null;
        OriginalMousePos = origMousePos;
        hit = newhit;
        time = Time.time;

        if (hit.transform == null)
        {
            throw new System.Exception("Hit Cannot be null");
        }

        previousMousepos = origMousePos;
    }
    public PointerInfo(ClickEventHandler receiver, Vector3 origMousePos, RaycastHit newhit)
    {
        if (previousMousepos == CameraMousePointer.INVALIDINPUT)
        {
            previousMousepos = origMousePos;
        }

        mouseMovementDelta = previousMousepos - origMousePos;

        Receiver = receiver;
        OriginalMousePos = origMousePos;
        hit = newhit;
        time = Time.time;

        previousMousepos = origMousePos;
    }

    public bool isOutdated
    {
        get { return ((Time.time - time) > maxLifetime); }
    }

    public override string ToString()
    {
        return HitTransform.name;
    }
}