using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(order = 1, fileName = "ProjectileInfo", menuName = "Survival Game/Add Projectile Info")]
public class ProjectileInfo : ScriptableObject
{
    public string Name;
    public ProjectileType Type;
    public Projectile Prefab;
    public float MoveSpeed;
    public int Damage;
}
