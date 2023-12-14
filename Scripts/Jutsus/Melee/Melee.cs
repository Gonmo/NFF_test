using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Melee : Ability
{

    [Header("Melee hit Prefab")]
    public GameObject MeleePrefab;

    [Header("Hit settings")]
    public float damage;
    public float MeleeRange;


    public override void Activate(GameObject parent)
    {
        //Get the caster stats
        StatSystem caster_script = parent.GetComponent<StatSystem>();
        Vector2 shootDirection = caster_script.shootDirection;
        Rigidbody2D rigidbody2d = parent.GetComponent<Rigidbody2D>();


        Collider2D[] hits = Physics2D.OverlapCircleAll(rigidbody2d.position + shootDirection * MeleeRange / 2, MeleeRange / 2);

        foreach (Collider2D hit in hits)
        {
            GameObject collided_gameobject = hit.gameObject;

            if (collided_gameobject != null & collided_gameobject.tag != parent.tag & collided_gameobject.tag != "Untagged")
            {
                //Get the General Enemy script from the gameObject
                StatSystem statenemy = collided_gameobject.GetComponent<StatSystem>();

                statenemy.ChangeHealth(-damage);

                Instantiate(MeleePrefab, collided_gameobject.transform.position, Quaternion.identity);
            }
        }

    }
}
