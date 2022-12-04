using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    public static DataManager Instance { get; private set; }

    [SerializeField] private string _entityInfoPath = "EntityInfo";
    private Dictionary<EntityType, EntityInfo> _entityData = new Dictionary<EntityType, EntityInfo>();

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(this.gameObject);
            return;
        }

        Instance = this;

        var entityInfos = Resources.LoadAll<EntityInfo>(_entityInfoPath);
        foreach (var e in entityInfos)
        {
            _entityData[e.Type] = e;
        }
    }

    public EntityInfo GetEntityInfo(EntityType type) => _entityData[type];
    
    private void OnDestroy()
    {
        Instance = null;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }
}
