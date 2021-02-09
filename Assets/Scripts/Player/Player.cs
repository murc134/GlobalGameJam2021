using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Singleton<Player>
{
    public static event Action<Player> OnInstantiated;
	private void Awake()
	{
        m_Instance = this;
	}
	void Start()
    {
        OnInstantiated(this);
    }
}
