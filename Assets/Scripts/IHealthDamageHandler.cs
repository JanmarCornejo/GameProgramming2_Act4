public interface IHealthDamageHandler
{
    EntityType Type { get; }
    bool IsAlive { get; }
    int CurrentHealth { get; }
    int MaxHealth { get; }
    int AttackDamage { get; }
    float AttackRange { get; }
    float AttackRate { get; }
    Skill ActiveSkill { get; }
    void AutoAttack();
    void Apply(ApplyType type, IHealthDamageHandler agent);
    void OnDie(IHealthDamageHandler agent);
}

public enum ApplyType
{
    None,
    PrimaryDamage,
    SkillDamage
}