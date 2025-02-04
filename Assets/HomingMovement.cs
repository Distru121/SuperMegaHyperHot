using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomingMovement : InteractableComponent
{
    public GameObject targetObject;

    public float speed = 1f;

    Rigidbody _rigidbody = null;
    public override void CloneTo(GameObject target)
    {
        HomingMovement clonedscript = target.AddComponent<HomingMovement>();
        clonedscript.targetObject = targetObject;
        clonedscript.speed = speed;
    }
    void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        if(_rigidbody!=null)
        {
            _rigidbody.AddForce(Vector3.Normalize(targetObject.transform.position - transform.position));
        }
    }
}
