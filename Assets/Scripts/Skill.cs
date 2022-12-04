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
    public int Cooldown;
    public Projectile ProjectilePrefab;
    //TODO vfx, sounds, etc
}

public enum SkillType
{
    Unassigned,
    //Playable Character Skills
    AxeNova,
    Teleport,
    MultiShot,
}
