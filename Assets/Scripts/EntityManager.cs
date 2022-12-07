using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityManager : MonoBehaviour
{
    public static EntityManager Instance { get; private set; }
    
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

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(this.gameObject);
            return;
        }

        Instance = this;
    }

    private void OnDestroy()
    {
        Instance = null;
    }
}
