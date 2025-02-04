using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpingObject : InteractableComponent
{
    public float jumpingDelay = 1f;
    public float jumpingStrength = 1f;
    
    float timer = 0;
    Rigidbody _rigidbody = null;

    public override void CloneTo(GameObject target)
    {
        JumpingObject clonedscript = target.AddComponent<JumpingObject>();
        clonedscript.jumpingDelay = jumpingDelay;
        clonedscript.jumpingStrength = jumpingStrength;
    }

    void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if(timer > jumpingDelay)
        {
            timer = 0;
            if(_rigidbody != null)
                _rigidbody.AddForce(transform.up * jumpingStrength);
        }
    }
}
