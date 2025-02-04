using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetBarrel : Target
{
    public override void Hit(float damage)
    {
        base.Hit(damage);
        Destroy(gameObject);
    }
}
