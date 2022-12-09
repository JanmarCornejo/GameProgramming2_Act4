using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityManager : Singleton<EntityManager>
{
    
    //Only 1 playable at a time
    // [SerializeField] private 
    
    public Entity CreateEntity(EntityType type)
    {
        var data = DataManager.Instance.GetEntityInfo(type);
        var obj = Instantiate(data.Prefab);
        var entity = obj.GetComponent<Entity>();
        entity.InitializeEntity(data);
        return entity;
    }
}
