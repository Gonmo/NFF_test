using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingTreeControl : MonoBehaviour
{
    //Combat variables
    bool combat = false;
    GameObject CombatTarget;


    //External functions & data
    public StatSystem npcstats;

    //List of spells
    public AbilityHolder projectileHolder;


    // Update is called once per frame
    void Update()
    {
        //After being destroyed, the script is still executing for some loops
        //During this execution, the target gameobject goes to null and creates errors
        //Hence, if the enemy is destroyed, no need to compute the target position anymore
        if (CombatTarget != null && combat == true)
        {
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
