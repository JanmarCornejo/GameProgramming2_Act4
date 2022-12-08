using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private bool _initializer;
    [SerializeField] private ProjectileType _type;
    [SerializeField] private Projectile[] _childProjectiles;
    
    private Rigidbody2D _rigidbody;
    private float _moveSpeed;
    [SerializeField] AudioClip ShootSfx;

    private void Awake()
    {
        if (_initializer) return;
        
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    public void InitializeProjectile(ProjectileInfo info, Vector2 direction, bool parentProjectile = true)
    {
        SoundManager.instance.PlaySound(ShootSfx);

        if (parentProjectile)
            _type = info.Type;
        
        _moveSpeed = info.MoveSpeed;

        switch (_type)
        {
            case ProjectileType.DwarfBasic:
            case ProjectileType.WizardBasic:
            case ProjectileType.MonkBasic:
                if (direction == Vector2.zero)
                {
                    direction = Vector2.right;
                }
                _rigidbody.AddForce(direction * _moveSpeed, ForceMode2D.Impulse);
                break;
            case ProjectileType.DwarfAxeNova:
                foreach (var proj in _childProjectiles)
                {
                    proj.InitializeProjectile(info, proj.transform.right, false);
                }
                //_rigidbody.AddForce(transform.right * _moveSpeed, ForceMode2D.Impulse);
                break;
            case ProjectileType.MonkMultiShot:
                foreach (var proj in _childProjectiles)
                {
                    proj.InitializeProjectile(info, proj.transform.right, false);
                }
                break;
        }

        //TODO back to object pool
        Destroy(gameObject, 3f);
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
