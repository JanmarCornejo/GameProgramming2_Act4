using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ObjectPoolManager : Singleton<ObjectPoolManager>
{
    [SerializeField] private int _initAmountToPool = 2;
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

        while (_initAmountToPool > 0)
        {
            foreach (var poolBag in _pools.Values)
            {
                CreatePool(poolBag);
            }
            _initAmountToPool--;
        }
    }

    private ObjectPool CreatePool(ObjectPoolBag poolBag)
    {
        var pool = Instantiate(poolBag.Prefab);
        _poolsList.Add(pool);
        pool.gameObject.SetActive(false);
        return pool;
    }

    private GameObject GetPoolObject(PoolType type)
    {
        GameObject obj = null;
        foreach (var op in _poolsList.Where(o => o.GetPoolType() == type 
                                                   && !o.gameObject.activeInHierarchy))
        {
            obj = op.gameObject;
            obj.SetActive(true);
            return obj;
        }

        if (_pools.TryGetValue(type, out ObjectPoolBag poolBag))
        {
            var poolObj = CreatePool(poolBag);
            obj = poolObj.gameObject;
            obj.SetActive(true);
            return obj;
        }

        return null;
    }

    public T GetPoolObject<T>(PoolType type)
    {
        GameObject pool = GetPoolObject(type);
        pool.TryGetComponent(out T component);
        return component;
    }
    
    public T GetPoolObject<T>(EntityType entityType)
    {
        var type = entityType switch
        {
            EntityType.BigGoblin => PoolType.BigGoblin,
            EntityType.FastGoblin => PoolType.FastGoblin,
            EntityType.NormalGoblin => PoolType.Goblin,
            _ => PoolType.Unassigned
        };

        GameObject pool = GetPoolObject(type);
        pool.TryGetComponent(out T component);
        return component;
    }
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