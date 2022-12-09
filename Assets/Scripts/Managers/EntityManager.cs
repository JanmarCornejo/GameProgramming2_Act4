using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EntityManager : Singleton<EntityManager>
{
    private List<Entity> _entities = new List<Entity>();
    
    //Only 1 playable at a time
    private bool _playerInsideGame = false;
    // [SerializeField] private 
    
    public Entity CreateEntity(EntityType type)
    {
        var data = DataManager.Instance.GetEntityInfo(type);
        var obj = Instantiate(data.Prefab);
        var entity = obj.GetComponent<Entity>();
        if (!_playerInsideGame)
        {
            _playerInsideGame = true;
            entity.InitializeEntity(data, true);
        }
        else
            entity.InitializeEntity(data);
        _entities.Add(entity);
        return entity;
    }

    public Entity GetEntityPlayer()
    {
        return _entities.FirstOrDefault(e => e.IsPlayer);
    }
}
