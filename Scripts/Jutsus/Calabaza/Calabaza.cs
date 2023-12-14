using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Calabaza : Ability
{
    [Header("Projectile Prefab")]
    public GameObject Calabaza_prefab;

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

        //Angle to orientate the calabaza
        //Based on player look direction + manual offset
        float angle = Mathf.Atan2(shootDirection.y, shootDirection.x) * Mathf.Rad2Deg + 90;

        //Creation of the calabaza
        //Positioning on the character position + xf in vertical direction
        //Rotation linked to angle variable.
        GameObject Calabaza_object = Instantiate(Calabaza_prefab, rigidbody2d.position + shootDirection.normalized * 0.4f, Quaternion.AngleAxis(angle, Vector3.forward));

        //Add movement to the proyectile
        Rigidbody2D calabazabody = Calabaza_object.GetComponent<Rigidbody2D>();
        calabazabody.AddForce(shootDirection * force);

        //Add torque
        calabazabody.AddTorque(torque);

        //Pass the caster gameobject to the projectile (for future damage calculation)
        Calabaza_prefab calabaza_script = Calabaza_object.GetComponent<Calabaza_prefab>();
        calabaza_script.caster = parent;
        calabaza_script.maxrange = maxrange;
        calabaza_script.damage = damage;
    }
}
