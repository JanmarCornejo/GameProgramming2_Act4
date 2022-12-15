using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using UnityEngine;

[System.Serializable]
public class SaveData
{
    public int Health;
    public int KillCount;
    public float[] Positions;

    public SaveData(Entity entity, int currentKillCount)
    {
        Health = entity.CurrentHealth;
        KillCount = currentKillCount;
        Positions = new float[3];
        var entityPos = entity.transform.position;
        Positions[0] = entityPos.x;
        Positions[1] = entityPos.y;
        Positions[2] = entityPos.z;
    }

    public SaveData(int killCount)
    {
        DefaultValues();
        KillCount = killCount;
    }

    public SaveData()
    {
        DefaultValues();
        KillCount = 0;
    }

    private void DefaultValues()
    {
        Health = 5;
        Positions = new float[3];
        Positions[0] = 0;
        Positions[1] = 0;
        Positions[2] = 0;
    }
}
