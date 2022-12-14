using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using UnityEngine;

[System.Serializable]
public class SaveData
{
    public int health;
    public float[] position;

    public SaveData(Entity entity)
    {
        health = entity.CurrentHealth;
        position = new float[3];

        position[0] = entity.transform.position.x;
        position[1] = entity.transform.position.y;
        position[2] = entity.transform.position.z;

    }

    public SaveData()
    {
        health = 5;
        position = new float[3];

        position[0] = 0;
        position[1] = 0;
        position[2] = 0;
    }
}
