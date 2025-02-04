using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    public virtual void Hit(float damage)
    {
        Debug.Log("hit target for "+damage+" damage.");
    }
}
