using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class Entity : MonoBehaviour, IHealthDamageHandler, ISkillHandler
{
    /// <summary>
    /// To initialize entity by values given by EntityInfo
    /// </summary>
    /// <param name="info"></param>
    public void InitializeEntity(EntityInfo info)
    {
        Type = info.Type;
        MaxHealth = info.MaxHealth;
        AttackDamage = info.AttackDamage;
        AttackRange = info.AttackRange;
        AttackRate = info.AttackRate;
        Skills = info.Skills;
        _spriteRenderer.sprite = info.Sprite;
        _moveSpeed = info.MoveSpeed;
        _basicProjectile = info.BasicProjectileInfo;
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
    private float _nextAttackTime;
    public Skill ActiveSkill { get; protected set; }
    private float _nextActiveSkillTime;
    public Skill[] Skills { get; protected set; }

    private TargetIndicator _indicator;
    //TODO add projectile to object pooling
    private ProjectileInfo _basicProjectile;
    protected Rigidbody2D _rigidbody;
    protected SpriteRenderer _spriteRenderer;
    [SerializeField] protected float _moveSpeed;
    [SerializeField] protected Vector2 _faceDirection;

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
        Destroy(this.gameObject);
        
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
    
    public virtual void AutoAttack()
    {
        switch (Type)
        {
            case EntityType.Dwarf:
            case EntityType.Wizard:
            case EntityType.Monk:
                //TODO auto attack logic with directions
                if (Time.time > _nextAttackTime)
                {
                    // Debug.Log("Auto Attack");
                    var tr = _indicator.OffsetTransform.transform;
                    var projectile = Instantiate(_basicProjectile.Prefab, tr.position,
                        _indicator.transform.rotation);
                    projectile.InitializeProjectile(_basicProjectile, _faceDirection);
                    _nextAttackTime = Time.time + 1 / AttackRate;
                }
                break;
        }
    }

    public virtual void CastSkill(SkillType type)
    {
        ActiveSkill = Skills.FirstOrDefault(s => s.Type == type);
        if (ActiveSkill == null)
            return;

        if (Time.time <= _nextActiveSkillTime) 
            return;

        //TODO logic of the skills
        //Dwarf - AxeNova
        //Wizard - Teleport or Laser
        //Monk - MultiShot
        switch (type)
        {
            //TODO optimize
            case SkillType.AxeNova:
                var axeNova = Instantiate(ActiveSkill.ProjectileInfo.Prefab, 
                    transform.position, Quaternion.identity);
                axeNova.InitializeProjectile(ActiveSkill.ProjectileInfo, Vector2.one);
                break;
            case SkillType.Teleport:
                var newPosition = (Vector2)transform.position + (_faceDirection * ActiveSkill.Range);
                transform.position = newPosition;
                break;
            case SkillType.MultiShot:
                var count = (int)ActiveSkill.Range;
                var shot = Instantiate(ActiveSkill.ProjectileInfo.Prefab, _indicator.OffsetTransform.position,
                    _indicator.transform.rotation);
                shot.InitializeProjectile(ActiveSkill.ProjectileInfo, _faceDirection);
                break;
        }

        _nextActiveSkillTime = Time.time + ActiveSkill.Cooldown;
    }

    protected virtual  void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    protected virtual void Start()
    {
        _indicator = this.transform.GetComponentInChildren<TargetIndicator>();
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
    SmallGoblin = 100,
    AbyssMage,
}