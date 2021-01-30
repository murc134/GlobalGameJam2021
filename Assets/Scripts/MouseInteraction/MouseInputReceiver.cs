using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class MouseInputReceiver : ClickEventHandler
{
    public bool DebugInput = false;
    public override void HandleDrag(PointerInfo drag, PointerInfo drop)
    {
        if (DebugInput)
        {
            Debug.Log($"Drag {drag.HitTransform.name} on {drop.HitTransform.name}", gameObject);
        }
    }

    public override void HandleDrop(PointerInfo drag, PointerInfo drop)
    {
        if (DebugInput)
        {
            Debug.Log($"Drop {drag.HitTransform.name} on {drop.HitTransform.name}", gameObject);
        }
    }

    public override void HandleMouseClick1(PointerInfo info)
    {
        if (DebugInput)
        {
            Debug.Log($"Click1 {info.HitTransform.name}", gameObject);
        }
    }
    public override void HandleMouseClick2(PointerInfo info)
    {
        if (DebugInput)
        {
            Debug.Log($"Click2 {info.HitTransform.name}", gameObject);
        }
    }
    public override void HandleMouseDown(PointerInfo info)
    {
        if (DebugInput)
        {
            Debug.Log($"Down {info.HitTransform.name}", gameObject);
        }
    }
    public override void HandleMouseHold(PointerInfo info)
    {
        if (DebugInput)
        {
            Debug.Log($"Hold {info.HitTransform.name}", gameObject);
        }
    }
    public override void HandleMouseHover(PointerInfo info)
    {
        if (DebugInput)
        {
            Debug.Log($"Hover {info.HitTransform.name}", gameObject);
        }
    }
    public override void HandleMouseUp(PointerInfo info)
    {
        if (DebugInput)
        {
            Debug.Log($"Up {info.HitTransform.name}", gameObject);
        }
    }
}
