using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [SerializeField] private PoolType _type;
    public PoolType GetPoolType() => _type;

    public void Initialize(PoolType type)
    {
        _type = type;
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

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
