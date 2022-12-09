using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Skill
{
    public string Name;
    public SkillType Type;
    public int Damage;
    public float Range;
    public float Cooldown;
    public ProjectileInfo ProjectileInfo;
}

public enum SkillType
{
    Unassigned,
    //Playable Character Skills
    AxeNova,
    Teleport,
    MultiShot,
}
