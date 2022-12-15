using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using UnityEngine;

//TODO link the achievement when saving
[System.Serializable]
public class SaveData
{
    public int Health;
    public float[] Positions;

    public SaveData(Entity entity)
    {
        Health = entity.CurrentHealth;
        Positions = new float[3];
        var entityPos = entity.transform.position;
        Positions[0] = entityPos.x;
        Positions[1] = entityPos.y;
        Positions[2] = entityPos.z;

    }

    public SaveData()
    {
        Health = 5;
        Positions = new float[3];
        Positions[0] = 0;
        Positions[1] = 0;
        Positions[2] = 0;
    }
}
