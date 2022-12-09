using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    public static T Instance { private set; get; }

    protected virtual void Awake()
    {
        if(Instance != null)
            Destroy(this.gameObject);

        Instance = (T)(MonoBehaviour)this;
    }

    protected virtual void OnDestroy()
    {
        if (Instance == this)
            Instance = null;
    }
}
