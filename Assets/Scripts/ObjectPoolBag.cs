using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(order = 1, fileName = "PoolBag", menuName = "Survival Game/Add Pool Bag")]
public class ObjectPoolBag : ScriptableObject
{
    public string Name;
    public PoolType Type;
    public ObjectPool Prefab;
}
