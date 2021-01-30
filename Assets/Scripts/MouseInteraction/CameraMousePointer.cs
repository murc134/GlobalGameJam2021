using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void ClickEvent(PointerInfo info);

public class CameraMousePointer : MonoBehaviour
{
    private Camera Camera { get => Camera.main; }
    private RaycastHit hit;
    public LayerMask targetLayers;
    public float distance = 1000;

    public static Vector3 INVALIDINPUT { get { return Vector3.one * float.MinValue; } }

    private bool mousePressed = false;

    public ClickEvent OnMouseClick1, OnMouseClick2, OnMouseUp, OnMouseDown, OnMouseHold, OnMouseHover;

    public PointerInfo ClickDownObject = null;
    public Vector3 ClickDownPosition = INVALIDINPUT;

    public PointerInfo LastClickObject = null;
    public Vector3 LastClickPosition = INVALIDINPUT;

    public float MaxDblClickTime = 0.5f;
    public float LastClickedTime = 0;

    private bool point(Vector3 realmousepos, out PointerInfo info)
    {
        Ray mousepos = Camera.ScreenPointToRay(realmousepos);

        if (Physics.Raycast(mousepos, out hit, distance, targetLayers))
        {
            if (hit.transform != null)
            {
                ClickEventHandler receiver = hit.transform.GetComponent<ClickEventHandler>();

                if (receiver == null)
                {
                    Debug.DrawRay(mousepos.origin, mousepos.direction * distance, Color.yellow);

                    info = new PointerInfo(realmousepos, hit);

                }
                else
                {
                    Debug.DrawRay(mousepos.origin, mousepos.direction * distance, Color.green);
                    info = new PointerInfo(receiver, realmousepos, hit);
                }
                return true;
            }
            else
            {
                Debug.LogError("Error this case should not be valid, what happened?", this);
            }

        }
        else
        {
            Debug.DrawRay(mousepos.origin, mousepos.direction * distance, Color.red);
        }
        info = null;
        return false;
    }

    private void handleMouseDown(PointerInfo info)
    {
        mousePressed = true;

        ClickDownObject = info;
        ClickDownPosition = info.HitPos;

        foreach (ClickEventHandler handler in GameObject.FindObjectsOfType<ClickEventHandler>())
        {
            handler.HandleMouseDown(info);
        }
    }

    private void handleMouseUp(PointerInfo info)
    {
        mousePressed = false;

        if (LastClickObject != null && LastClickObject.HitTransform == info.HitTransform) // Double Click
        {
            // Check time
            if ((Time.time - LastClickedTime) <= MaxDblClickTime)
            {
                foreach (ClickEventHandler handler in GameObject.FindObjectsOfType<ClickEventHandler>())
                {
                    handler.HandleMouseClick2(info);
                }
                LastClickObject = null;
                LastClickedTime = 0;
            }
            else
            {
                foreach (ClickEventHandler handler in GameObject.FindObjectsOfType<ClickEventHandler>())
                {
                    handler.HandleMouseClick1(info);
                }
                LastClickObject = info;
                LastClickedTime = Time.time;
            }

        }
        else
        {
            foreach (ClickEventHandler handler in GameObject.FindObjectsOfType<ClickEventHandler>())
            {
                handler.HandleMouseUp(info);
                if (ClickDownObject.HitTransform == info.HitTransform) // Single Click
                {
                    handler.HandleMouseClick1(info);
                    LastClickedTime = Time.time;
                }
                else // Drop
                {
                    handler.HandleDrop(ClickDownObject, info);
                }
            }

            // Reset LastClickObject

            if (ClickDownObject.HitTransform == info.HitTransform) // Single Click
            {
                LastClickObject = ClickDownObject;
                LastClickedTime = Time.time;
            }
            else // Drop (not implemented)
            {
                LastClickObject = null;
                LastClickedTime = 0;
            }
        }

        ClickDownObject = null;
        ClickDownPosition = INVALIDINPUT;
    }

    private void handleMouseHold(PointerInfo info)
    {
        if (ClickDownObject.HitTransform == info.HitTransform) // Hold
        {
            foreach (ClickEventHandler handler in GameObject.FindObjectsOfType<ClickEventHandler>())
            {
                handler.HandleMouseHold(info);
            }
        }
        else // Drag
        {
            foreach (ClickEventHandler handler in GameObject.FindObjectsOfType<ClickEventHandler>())
            {
                handler.HandleDrag(ClickDownObject, info);
            }
        }
    }

    private void handleMouseHover(PointerInfo info)
    {
        foreach (ClickEventHandler handler in GameObject.FindObjectsOfType<ClickEventHandler>())
        {
            handler.HandleMouseHover(info);
        }
    }

    private void handleMouseInput()
    {
        PointerInfo info = null;
        if (point(Input.mousePosition, out info))
        {
            if (Input.GetMouseButtonDown(0)) // Mouse Down
            {
                handleMouseDown(info);
            }
            else if (Input.GetMouseButtonUp(0)) // Mouse Up
            {
                handleMouseUp(info);
            }
            else if (Input.GetMouseButton(0)) // Mouse Hold
            {
                handleMouseHold(info);
            }
            else // Hover
            {
                handleMouseHover(info);
            }
        }
    }

    private void Update()
    {
        handleMouseInput();
    }
}
