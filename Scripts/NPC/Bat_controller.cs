using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bat_controller : MonoBehaviour
{

    bool combat = false;
    GameObject CombatTarget;

    public float melee_dmg;

    //External functions & data
    public NPC_movement_AI NPC_movement_AI;
    public StatSystem npcstats;

    //List of spells
    public AbilityHolder projectileHolder;


    void Update()
    {

        if (combat == false)
        {
            NPC_movement_AI.no_combat_wander(npcstats.speed);
        }

        //After being destroyed, the script is still executing for some loops
        //During this execution, the target gameobject goes to null and creates errors
        //Hence, if the enemy is destroyed, no need to compute the target position anymore
        else if (CombatTarget != null && combat == true)
        {
            NPC_movement_AI.combat_positioning(npcstats.speed, CombatTarget);

            //Update shoot direction
            Vector2 current_position = GetComponent<Rigidbody2D>().position; //Your position
            Vector2 target_position = CombatTarget.transform.position; //Target position
            npcstats.shootDirection = target_position - current_position; //Vector between yourself and target
            npcstats.shootDirection = npcstats.shootDirection.normalized; //normalize the vector

            //Attempt to shoot towards the target
            projectileHolder.ActivateHolder();
        }

        //If we have lost our target, come back to "no combat" mode
        else if (CombatTarget == null && combat == true) { combat = false; }

    }


    //Melee attack on contact with player
    void OnCollisionEnter2D(Collision2D collision)
    {
        //Get the GameObject of the collision
        GameObject collided = collision.gameObject;

        //Get the stats from the collided gameobject
        StatSystem statenemy = collided.GetComponent<StatSystem>();

        //If the collided object is from a different tag/team, damage
        if (statenemy != null && collided.tag != gameObject.tag && collided.tag != "Untagged")
        {
            statenemy.ChangeHealth(-melee_dmg);
        }
    }


    //On trigger --> bigger 2dcollider for the aggro. Save the collided gameobject and set combat to true
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag != gameObject.tag && collision.gameObject != null && collision.gameObject.tag != "Untagged")
        {
            combat = true; //Set the enemy to combat mode

            CombatTarget = collision.gameObject; //Get the GameObject of the target
        }

    }





}
