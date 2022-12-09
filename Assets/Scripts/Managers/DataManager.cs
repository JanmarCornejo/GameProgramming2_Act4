using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : Singleton<DataManager>
{
    [SerializeField] private string _entityInfoPath = "EntityInfo";
    private Dictionary<EntityType, EntityInfo> _entityData = new Dictionary<EntityType, EntityInfo>();
    
    protected override void Awake()
    {
        base.Awake();
        var entityInfos = Resources.LoadAll<EntityInfo>(_entityInfoPath);
        foreach (var e in entityInfos)
        {
            _entityData[e.Type] = e;
        }
    }

    public EntityInfo GetEntityInfo(EntityType type)
    {
       return _entityData[type];
    }
}
