using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "gun", menuName = "Create/gun")]
public class GunSO : ScriptableObject
{
    public float rateOfFire;
    public int maxAmmo;
    public int range;
}
