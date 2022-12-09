using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Entity
{
    [SerializeField] private Transform player;
    [SerializeField] private EntityType _type;
    [SerializeField] private int _attackDamage;
    [SerializeField] private float _attackRange;
    [SerializeField] private float _attackRate;
    private Vector2 movement;
    private Rigidbody2D rb;

    protected override void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
        //player = GameObject.FindGameObjectWithTag("Player").transform;
        InitializeEnemy();
        _type = Type;
        _attackDamage = AttackDamage;
        _attackRange = AttackRange;
        _attackRate = AttackRate;
    }

    public void InitializeEnemy()
    {
        _hp = MaxHealth;
        player = EntityManager.Instance.GetEntityPlayer().transform;
    }

    protected override void UpdateEntity()
    {
        Vector3 direction = player.position - transform.position;
        direction.Normalize();
        movement = direction;
        MoveEnemy(movement);

    }

    void MoveEnemy(Vector2 direction)
    {
        rb.MovePosition((Vector2)transform.position + (direction * (_moveSpeed * Time.deltaTime)));
    }

    private void FlipSprite()
    {
        var dirToFace = _faceDirection.x >= 0 ? 1 : -1;
        _spriteRenderer.flipX = dirToFace == -1;
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (!col.gameObject.CompareTag("Player")) return;
        
        col.gameObject.TryGetComponent(out IHealthDamageHandler handler);
        handler.Apply(ApplyType.PrimaryDamage, this);
        // Destroy(this.gameObject);
        gameObject.SetActive(false);
    }
}
