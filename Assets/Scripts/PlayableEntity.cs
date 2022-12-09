using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayableEntity : Entity
{
    //just to display on inspector
    [Header("Only for visualization, not for debug")]
    [SerializeField] private EntityType _type;
    [SerializeField] private int _attackDamage;
    [SerializeField] private float _attackRange;
    [SerializeField] private float _attackRate;
    
    //variables

    protected override void Start()
    {
        base.Start();
        _type = Type;
        _attackDamage = AttackDamage;
        _attackRange = AttackRange;
        _attackRate = AttackRate;
    }

    protected override void UpdateEntity()
    {
        Navigate();
    }
    
    private void Navigate()
    {
        //Basic movement and skill stuff
        float xAxis = Input.GetAxisRaw("Horizontal");
        float yAxis = Input.GetAxisRaw("Vertical");
        _faceDirection = new Vector2(xAxis, yAxis);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            CastSkill(Skills[0].Type);
        }
        
        _rigidbody.velocity = _faceDirection * _moveSpeed;
        FlipSprite();
    }

    private void FlipSprite()
    {
        var dirToFace = _faceDirection.x >= 0 ? 1 : -1;
        _spriteRenderer.flipX = dirToFace == -1;

        //TODO fix direction indicator
        // if (_spriteRenderer.flipX && _faceDirection == Vector2.zero)
        // {
        //     _faceDirection = Vector2.right;
        // }
    }
    
    public Vector2 GetPointingDirection() => _faceDirection;

}
