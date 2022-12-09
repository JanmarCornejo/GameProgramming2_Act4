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
        if (!_playerInsideGame)
        {
            _playerInsideGame = true;
            var obj = Instantiate(data.Prefab);
            obj.TryGetComponent(out Entity player);
            player.InitializeEntity(data, true);
            _entities.Add(player);
            return player;
        }
        var entity = ObjectPoolManager.Instance.GetPoolObject<Entity>(type);
        entity.InitializeEntity(data);
        _entities.Add(entity);
        return entity;
    }

    public Entity GetPlayerEntity()
    {
        return _entities.FirstOrDefault(e => e.IsPlayer);
    }
}
