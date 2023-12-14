using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Fireball : Ability
{
    [Header("Projectile Prefab")]
    public GameObject Fireball_prefab;

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
        GameObject Fireball_object = Instantiate(Fireball_prefab, rigidbody2d.position + shootDirection.normalized * 0.4f, Quaternion.AngleAxis(angle, Vector3.forward));

        //Add movement to the proyectile
        Rigidbody2D fireballbody = Fireball_object.GetComponent<Rigidbody2D>();
        fireballbody.AddForce(shootDirection * force);

        //Add torque
        fireballbody.AddTorque(torque);

        //Pass the caster gameobject to the projectile (for future damage calculation)
        Fireball_prefab fireball_script = Fireball_object.GetComponent<Fireball_prefab>();
        fireball_script.caster = parent;
        fireball_script.maxrange = maxrange;
        fireball_script.damage = damage;
    }
}
