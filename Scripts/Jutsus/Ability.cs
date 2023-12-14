using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ability : ScriptableObject
{
    [Header("General")]
    public new string name;
    public float cooldownTime;
    public float reqChakra;
    public float reqSta;
    public float castTime;

    public virtual void Activate(GameObject parent){}
}
