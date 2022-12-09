using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour, IHealthDamageHandler
{
    [SerializeField] private bool _initializer;
    [SerializeField] private ProjectileType _type;
    [SerializeField] private Projectile[] _childProjectiles;
    
    private Rigidbody2D _rigidbody;
    private float _moveSpeed;
    [SerializeField] AudioClip ShootSfx;
    private ProjectileInfo _info;
    

    private void Awake()
    {
        if (_initializer) return;
        
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    public void InitializeProjectile(ProjectileInfo info, Vector2 direction, bool parentProjectile = true)
    {
        SoundManager.Instance.PlaySound(ShootSfx);

        if (parentProjectile)
            _type = info.Type;
        
        _moveSpeed = info.MoveSpeed;
        AttackDamage = info.Damage;
        _info = info;
        Fire(_info, direction);
        Invoke(nameof(ReturnToPool), 3f);
        // Destroy(gameObject, 3f);
    }

    private void Fire(ProjectileInfo info, Vector2 direction)
    {
        switch (_type)
        {
            case ProjectileType.DwarfBasic:
            case ProjectileType.WizardBasic:
            case ProjectileType.MonkBasic:
                if (direction == Vector2.zero)
                    direction = Vector2.right;
                _rigidbody.AddForce(direction * _moveSpeed, ForceMode2D.Impulse);
                break;
            case ProjectileType.DwarfAxeNova:
            case ProjectileType.MonkMultiShot:
                foreach (var proj in _childProjectiles)
                {
                    proj.gameObject.SetActive(true);
                    var projTransform = proj.transform;
                    projTransform.localPosition = Vector3.zero;
                    // projTransform.localRotation = Quaternion.identity;
                    proj.InitializeProjectile(info, projTransform.right, false);
                }
                break;
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Enemy"))
        {
            col.TryGetComponent(out IHealthDamageHandler handler);
            Debug.Log(handler.CurrentHealth);
            var fx = ObjectPoolManager.Instance.GetPoolObject<ObjectPool>(PoolType.DeathEffect);
            fx.transform.position = col.transform.position;
            fx.ReturnToPool(1f);
            handler?.Apply(ApplyType.PrimaryDamage, this);
            ReturnToPool();
            // Destroy(this.gameObject);
        }
    }

    private void ReturnToPool()
    {
        gameObject.SetActive(false);
    }

    #region Dud IHealthDamangeHandler

    public EntityType Type { get; }
    public bool IsAlive { get; }
    public int CurrentHealth { get; }
    public int MaxHealth { get; }
    public float AttackRange { get; }
    public float AttackRate { get; }
    public Skill ActiveSkill { get; }
    public void AutoAttack() { }
    
    public void Apply(ApplyType type, IHealthDamageHandler agent)
    {
    }
    
    #endregion
    
    public int AttackDamage { get; private set; }

    public void OnDie(IHealthDamageHandler agent)
    {
    }
}

public enum ProjectileType
{
    Unassigned,
    DwarfBasic,
    WizardBasic,
    MonkBasic,
    DwarfAxeNova,
    MonkMultiShot,
    
}
