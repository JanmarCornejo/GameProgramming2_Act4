using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayableEntity : Entity
{
    
    [SerializeField] private bool _canControl; 

    protected override void UpdateEntity()
    {
        if (!IsAlive) return;
        Navigate();
    }
    
    private void Navigate()
    {
        //Basic movement and skill stuff
        if (Input.GetKeyDown(KeyCode.Space))
        {
            CastSkill(Skills[0].Type);
        }
        
        float xAxis = Input.GetAxisRaw("Horizontal");
        float yAxis = Input.GetAxisRaw("Vertical");
        Vector2 currentDirection = new Vector2(xAxis, yAxis);
        if (currentDirection == Vector2.zero)
        {
            _rigidbody.velocity = Vector2.zero;
            return;
        }
        _faceDirection = currentDirection;
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
