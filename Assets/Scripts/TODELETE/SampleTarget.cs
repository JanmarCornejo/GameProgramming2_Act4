using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SampleTarget : Entity
{
    public int HealthVar = 5;

    protected override void Start()
    {
        base.Start();

        _hp = HealthVar;
        MaxHealth = HealthVar;
        Debug.Log(CurrentHealth);
    }


    protected override void UpdateEntity()
    {
        
    }
}
