using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class SShuriken : Ability
{
    [Header("Projectile Prefab")]
    public GameObject SShuriken_prefab;

    [Header("Projectile settings")]
    public float force;
    public float torque;
    public float maxrange;
    public float damage;

    public override void Activate(GameObject parent)
    {
        StatSystem caster_script = parent.GetComponent<StatSystem>();
        Vector2 shootDirection = caster_script.shootDirection;

        Rigidbody2D rigidbody2d = parent.GetComponent<Rigidbody2D>();

        //Angle to orientate the fireball.
        //Based on player look direction + manual offset
        float angle = Mathf.Atan2(shootDirection.y, shootDirection.x) * Mathf.Rad2Deg + 90;

        //Creation of the fireball
        //Positioning on the character position + xf in vertical direction
        //Rotation linked to angle variable.
        GameObject SShuriken_object = Instantiate(SShuriken_prefab, rigidbody2d.position + shootDirection.normalized * 0.4f, Quaternion.AngleAxis(angle, Vector3.forward));

        //Add movement to the proyectile
        Rigidbody2D shurikenbody = SShuriken_object.GetComponent<Rigidbody2D>();
        shurikenbody.AddForce(shootDirection * force);

        //Add torque
        shurikenbody.AddTorque(torque);

        //Pass the caster gameobject to the projectile (for future damage calculation)
        SShuriken_prefab shuriken_script = SShuriken_object.GetComponent<SShuriken_prefab>();
        shuriken_script.caster = parent;
        shuriken_script.maxrange = maxrange;
        shuriken_script.damage = damage;
    }
}

