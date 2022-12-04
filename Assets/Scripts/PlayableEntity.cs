using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayableEntity : Entity
{
    //just to display on inspector
    [Header("Only for visualization, not for debug")]
    [SerializeField] private EntityType _type;
    [SerializeField] private int _attackDamage;
    [SerializeField] private float _attackRange;
    [SerializeField] private float _attackRate;

    protected override void Start()
    {
        base.Start();
        _type = Type;
        _attackDamage = AttackDamage;
        _attackRange = AttackRange;
        _attackRate = AttackRate;
    }

    protected override void UpdateEntity()
    {
        Navigate();
    }
    
    private void Navigate()
    {
        //Do movement controls here
        
    }
}
