using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Cyclone : Ability
{
    [Header("Projectile Prefab")]
    public GameObject Cycloneprefab;

    [Header("Projectile settings")]
    public float velocity;
    public float maxtime;
    public float damage;

    public override void Activate(GameObject parent)
    {
        StatSystem caster_script = parent.GetComponent<StatSystem>();
        Vector2 shootDirection = caster_script.shootDirection;

        Rigidbody2D rigidbody2d = parent.GetComponent<Rigidbody2D>();

        //Angle to orientate the cyclone
        //Based on player look direction + manual offset
        float angle = Mathf.Atan2(shootDirection.y, shootDirection.x) * Mathf.Rad2Deg + 90;

        //Creation of the fireball
        //Positioning on the character position + xf in vertical direction
        //Rotation linked to angle variable.
        GameObject Cycloneobject = Instantiate(Cycloneprefab, rigidbody2d.position + shootDirection.normalized * 0.1f, Quaternion.AngleAxis(angle, Vector3.forward));

        //Add movement to the proyectile
        Rigidbody2D cyclonebody = Cycloneobject.GetComponent<Rigidbody2D>();
        //cyclonebody.velocity = shootDirection * velocity;


        //Pass the caster gameobject to the projectile (for future damage calculation)
        CyclonePrefab cyclonescript = Cycloneobject.GetComponent<CyclonePrefab>();
        cyclonescript.caster = parent;
        cyclonescript.maxtime = maxtime;
        cyclonescript.damage = damage;
    }
}
