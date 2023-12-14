using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball_prefab : MonoBehaviour
{
    //Get rigidbody
    Rigidbody2D rigidbody2d;

    //Get animator
    Animator animator;

    //Projectile team
    string casterteam;

    /* Jutsu stats given by Fireball script*/
    [HideInInspector]
    public float damage;       //Jutsu damage
    [HideInInspector]
    public float maxrange;    //Jutsu max range
    [HideInInspector]
    public GameObject caster;

    void Awake()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
    }


    void Start()
    {
        animator = GetComponent<Animator>();
        casterteam = caster.tag;

    }


    // Update is called once per frame
    void Update()
    {
        //Destroy if it arrives to certain distance without hitting anything
        if (transform.position.magnitude > maxrange)
        {
            //Stop all movement
            rigidbody2d.velocity = Vector3.zero;

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

            //If the script is found (enemy confirmation. To be changed in the future), call the damage function
            // caster != null check in case it collides with something after the caster is dead (this would mean this projectile deals no damage if the caster is dead. To be checked)
            if (collided_gameobject != null & collided_gameobject.tag != casterteam & collided_gameobject.tag != "Untagged")
            {
                //Get the General Enemy script from the gameObject
                StatSystem statenemy = collided_gameobject.GetComponent<StatSystem>();

                statenemy.ChangeHealth(-damage);
            }

            //Stop all movement
            rigidbody2d.velocity = Vector3.zero;

            //Disable the collision for the explosion (to avoid other object to collide to it when exploding)
            GetComponent<BoxCollider2D>().enabled = false;

            //Set the explosion animation
            if (animator != null)
            {
                animator.SetTrigger("Destroy");
            }
        }

    }

    //Function to destroy object at the end of the explosion animation
    public void DestroyMe() { Destroy(gameObject); }
}
