using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [SerializeField] private PoolType _type;
    public PoolType GetPoolType() => _type;

    private bool _initialized = false;

    public void Initialize(PoolType type)
    {
        if (_initialized) return;
        _type = type;
        _initialized = true;
    }

    public void SetPositionAndRotation(Vector3 position, Quaternion rotation)
    {
        var tr = transform;
        tr.position = position;
        tr.rotation = rotation;
    }

    public void ReturnToPool(float time = 2f)
    {
        Invoke(nameof(ReturnObj), time);
    }

    private void ReturnObj()
    {
        gameObject.SetActive(false);
    }
}
