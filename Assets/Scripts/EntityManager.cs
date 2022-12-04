using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityManager : MonoBehaviour
{
    public static EntityManager Instance { get; private set; }
    
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

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
