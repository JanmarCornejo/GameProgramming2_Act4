using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolManager : Singleton<ObjectPoolManager>
{
    //TODO object pooling

    [SerializeField] private int _amountToPool = 5;

    [SerializeField] private string _objectPoolPath = "PoolBags";
    private Dictionary<PoolType, ObjectPoolBag> _pools = new Dictionary<PoolType, ObjectPoolBag>();
    [SerializeField] private List<ObjectPool> _poolsList = new List<ObjectPool>();

    protected override void Awake()
    {
        base.Awake();

        var objs = Resources.LoadAll<ObjectPoolBag>(_objectPoolPath);
        foreach (var o in objs)
        {
            o.Prefab.Initialize(o.Type);
            _pools[o.Type] = o;
        }
        
        while (_amountToPool > 0)
        {
            foreach (var poolBag in _pools.Values)
            {
                var pool = Instantiate(poolBag.Prefab);
                _poolsList.Add(pool);
                pool.gameObject.SetActive(false);
            }
            _amountToPool--;
        }
    }

    // public ObjectPool GetPoolObject(PoolType type)
    // {
    //     foreach (var obj in _poolsList)
    //     {
    //         if(obj.)
    //     }
    //     //var obj = 
    // }
}

public enum PoolType
{
    Unassigned,
    //PREFABS OF ALL THE ENTITIES AND PROJECTILE TYPES
    BasicProjectile,
    AxeNovaProjectile,
    MultiShotProjectile,
    BigGoblin,
    FastGoblin,
    Goblin,
    
}
