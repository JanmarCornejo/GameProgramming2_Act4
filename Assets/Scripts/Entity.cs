using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class Entity : MonoBehaviour, IHealthDamageHandler, ISkillHandler
{
    //TODO To be called in entity manager or similar
    public virtual void InitializeEntity(EntityInfo info)
    {
        Type = info.Type;
        MaxHealth = info.MaxHealth;
        AttackDamage = info.AttackDamage;
        AttackRange = info.AttackRange;
        AttackRate = info.AttackRate;
        Skills = info.Skills;
        _spriteRenderer.sprite = info.Sprite;
    }

    public EntityType Type { get; private set; }
    public bool IsAlive => CurrentHealth > 0;
    private int _hp;

    public int CurrentHealth
    {
        get => _hp;
        private set => _hp = Mathf.Clamp(value, 0, MaxHealth);
    }
    public int MaxHealth { get; protected set;}
    public int AttackDamage { get; protected set;}
    public float AttackRange { get; protected set;}
    public float AttackRate { get; protected set;}
    public Skill ActiveSkill { get; protected set; }
    public Skill[] Skills { get; protected set; }

    private SpriteRenderer _spriteRenderer;

    
    public virtual void AutoAttack()
    {
        switch (Type)
        {
            case EntityType.Dwarf:
            case EntityType.Wizard:
            case EntityType.Monk:
                //TODO auto attack logic
                
                break;
        }
    }

    public virtual void Apply(ApplyType type, IHealthDamageHandler agent)
    {
        if (!IsAlive) return;

        switch (type)
        {
            case ApplyType.PrimaryDamage:
                CurrentHealth -= agent.AttackDamage;
                break;
            case ApplyType.SkillDamage:
                CurrentHealth -= agent.ActiveSkill.Damage;
                break;
        }
        
        if(!IsAlive)
            OnDie(this);
    }

    public virtual void OnDie(IHealthDamageHandler agent)
    {
        //TODO dead state for enemy AI
        // Destroy(this.gameObject);
        switch (agent.Type)
        {
            case EntityType.Dwarf:
            case EntityType.Wizard:
            case EntityType.Monk:
                //TODO Restart scene
                Debug.Log($"Restart Scene");
                break;
        }
    }

    public virtual void CastSkill(SkillType type)
    {
        ActiveSkill = Skills.FirstOrDefault(s => s.Type == type);
        //TODO logic of the skills
        //Dwarf - AxeNova
        //Wizard - Teleport or Laser
        //Monk - MultiShot
        switch (type)
        {
            case SkillType.AxeNova:
                break;
            case SkillType.Teleport:
                break;
            case SkillType.MultiShot:
                break;
        }
    }

    protected virtual  void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();

    }

    protected virtual void Start()
    {

    }

    protected virtual void Update()
    {
        UpdateEntity();
        AutoAttack();
    }
    
    /// <summary>
    /// Will call this function every frame
    /// </summary>
    protected abstract void UpdateEntity();
}

public enum EntityType
{
    Unassigned,
    //Playable Characters
    Dwarf = 1,
    Wizard,
    Monk,
    
    //Enemy
    Slime = 100,
    AbyssMage,
    
    
}