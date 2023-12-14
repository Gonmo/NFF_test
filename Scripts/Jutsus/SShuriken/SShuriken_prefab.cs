using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SShuriken_prefab : MonoBehaviour
{

    //Get rigidbody
    Rigidbody2D rigidbody2d;

    //Projectile team
    string casterteam;

    /* Jutsu stats given by SShuriken script*/
    [HideInInspector]
    public float damage;       //Jutsu damage
    [HideInInspector]
    public float maxrange;    //Jutsu max range
    [HideInInspector]
    public GameObject caster;


    // Start is called before the first frame update
    void Start()
    {
        casterteam = caster.tag;
    }



    // Update is called once per frame
    void Update()
    {
        //Destroy if it arrives to certain distance without hitting anything
        if (transform.position.magnitude > maxrange)
        {
            //Destroy GameObject
            Destroy(gameObject);
        }

    }


    //void OnCollisionEnter2D(Collision2D collision)
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject != caster)
        {
            //Get the GameObject of the collision
            GameObject collided_gameobject = collision.gameObject;

            //Get the General Enemy script from the gameObject
            StatSystem statenemy = collided_gameobject.GetComponent<StatSystem>();

            //If the script is found (enemy confirmation. To be changed in the future), call the damage function
            if (collided_gameobject != null & collided_gameobject.tag != casterteam & collided_gameobject.tag != "Untagged")
            {
                statenemy.ChangeHealth(-damage);
            }

            //Destroy GameObject
            Destroy(gameObject);
        }

    }
}
