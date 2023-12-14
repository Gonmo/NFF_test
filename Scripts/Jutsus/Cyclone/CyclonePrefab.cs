using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CyclonePrefab : MonoBehaviour
{
    //Get rigidbody
    Rigidbody2D rigidbody2d;

    //Projectile team
    string casterteam;

    /* Jutsu stats given by Calabaza script*/
    [HideInInspector]
    public float damage;
    [HideInInspector]
    public float maxtime;
    [HideInInspector]
    public GameObject caster;

    //Timers for expiration time (this jutsu does not expire on collision)
    float expirationtimer;

    //This jutsu does damage on contact. Damage each x seconds tick.
    float damagetimer;
    float growrate = 1f;


    // Start is called before the first frame update
    void Start()
    {
        casterteam = caster.tag;
        expirationtimer = 0;
        damagetimer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        expirationtimer += Time.deltaTime;

        //Initially, enlarge the beam
        if (expirationtimer < maxtime / 1.7)
        {
            gameObject.transform.localScale += new Vector3(0, 0.1f, 0) * growrate*2; // Scale object in the specified direction
        }

        
        //Destroy if it arrives to certain time active, start diminishing the beam
        if (expirationtimer >= maxtime/1.7)
        {
            gameObject.transform.localScale += new Vector3(-0.1f, 0, 0) * growrate/10; // Scale object in the specified direction
        }

        // At end time, destroy object
        if (expirationtimer >= maxtime)
        {
            //Destroy GameObject
            Destroy(gameObject);
        }
    }

    //void OnCollisionEnter2D(Collision2D collision)
    void OnTriggerStay2D(Collider2D collision)
    {
        //If the collision is not the caster
        if (collision.gameObject != caster)
        {

            //Get the GameObject of the collision
            GameObject collided_gameobject = collision.gameObject;

            //If the gameobject is an enemy, call the damage function
            if (collided_gameobject != null & collided_gameobject.tag != casterteam & collided_gameobject.tag != "Untagged")
            {
                //Get the General Enemy script from the gameObject
                StatSystem statenemy = collided_gameobject.GetComponent<StatSystem>();

                //only deal damage on the damage ticks
                damagetimer += Time.deltaTime;

                if (damagetimer >= 0.25)
                {
                    //Deal the damage
                    statenemy.ChangeHealth(-damage);
                    //Reset the damage timer
                    damagetimer = 0;
                }
            }

            

        }

    }
}
