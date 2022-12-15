using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : Singleton<DataManager>
{
    public static event Action<SaveData, bool> OnLoadData;
    
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

    public SaveData InvokeLoadData(bool loadPlayer = false)
    {
        var data = SaveSystem.Instance.Load();
        OnLoadData?.Invoke(data, loadPlayer);
        return data;
    }
}
