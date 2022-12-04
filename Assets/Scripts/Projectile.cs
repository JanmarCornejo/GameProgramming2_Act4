using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private ProjectileType _type;
    private Rigidbody2D _rigidbody;
    private float _moveSpeed;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    public void InitializeProjectile(ProjectileInfo info, Vector2 direction)
    {
        _moveSpeed = info.MoveSpeed;
        if (direction == Vector2.zero)
        {
            direction = Vector2.right;
        }
        _rigidbody.AddForce(direction * _moveSpeed, ForceMode2D.Impulse);
    }

}

public enum ProjectileType
{
    Unassigned,
    DwarfBasic,
    WizardBasic,
    MonkBasic,
}
