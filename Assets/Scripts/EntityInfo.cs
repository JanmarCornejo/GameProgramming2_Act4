using UnityEngine;


[CreateAssetMenu(order = 1, fileName = "EntityInfo", menuName = "Survival Game/Add Entity Info")]
public class EntityInfo : ScriptableObject
{
    public string Name;
    public Entity Prefab;
    public Sprite Sprite;
    public EntityType Type;
    public int MaxHealth;
    public int AttackDamage;
    public float AttackRange;
    public float AttackRate;
    public Skill[] Skills;
}