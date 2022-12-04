using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetIndicator : MonoBehaviour
{
    private PlayableEntity _entity;
    public Transform OffsetTransform;
    
    // Start is called before the first frame update
    void Start()
    {
        _entity = this.GetComponentInParent<PlayableEntity>();
    }

    // Update is called once per frame
    void Update()
    {
        var direction = _entity.GetPointingDirection();
        var angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }
}
