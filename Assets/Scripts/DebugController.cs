using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugController : MonoBehaviour
{
    public EntityType Type;

    public DebugType DebugType;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            switch (DebugType)
            {
                case DebugType.CreateEntity:
                    EntityManager.Instance.CreateEntity(Type);
                    break;
            }
        }
    }
}

public enum DebugType
{
    None,
    CreateEntity
}

// public entity