using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WalkableArea : MouseInputReceiver
{
    private static NavAgentPointToClickController localplayercontroller;

    void Awake()
    {
        if(localplayercontroller == null)
        {
            localplayercontroller = TagManager.FindObjectWithTag<NavAgentPointToClickController>("Localplayer");
            if(localplayercontroller == null)
            {
                Debug.LogError($"Could not find {typeof(NavAgentPointToClickController).Name} in scene! Please make sure that you have an object in the scene that is tagged as 'Localplayer' that has a {typeof(NavMeshAgent).Name} and a {typeof(NavAgentPointToClickController).Name}");
            }
        }
    }

    public override void HandleMouseClick1(PointerInfo info)
    {
        base.HandleMouseClick1(info);

        if(info.HitTransform == transform)
        {
            localplayercontroller.NavAgent.SetDestination(info.HitPos);
        }
    }
}
