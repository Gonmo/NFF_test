using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC_movement_AI : MonoBehaviour
{

    Rigidbody2D rigidbody2D;

    Vector2 spawnpoint; //Vector with the spawnpoint coordinates

    /* -------- Variables for moving algorithm --------- */
    float timer = 0f; // General timer
    float move = 1f; //1 = move; 0 = idle
    float max_action_time = 4f; //max time doing a movement/idling
    float min_action_time = 2f; //min time doing a movement/idling
    Vector2 target_direction;


    // Start is called before the first frame update
    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();

        //Get initial spawn point for the movement logic
        spawnpoint = rigidbody2D.position;

    }


    //Function to calculate the desired direction vector for the movement
    public Vector2 spawnpoint_wander(Vector2 spawnpoint, Vector2 current_position)
    {
        //Calculate the vector to come back to spawn point
        Vector2 distance_spawn = spawnpoint - current_position;

        //If too far away from spawnpoint, come back to spawn point (with some variance)
        if (distance_spawn.magnitude > 1.5f)
        {
            /* --- Variance calculation --- */

            //Get current angle from direction vector 
            float angle = Vector2.Angle(Vector2.right, distance_spawn);

            //As Vector2.Angle only works when the angle is between 0 and 180 --> workaround
            if (distance_spawn.y < 0)
            {
                angle = -angle;
            }

            //Include a variance in the angle
            angle = angle + Random.Range(-30f, 30f);

            //Recalculate the vector with the new angle
            distance_spawn = new Vector2(Mathf.Cos(angle * Mathf.PI / 180), Mathf.Sin(angle * Mathf.PI / 180));

        }
        else //If not too far from spawnpoint, random normalized vector for movement.
        {
            distance_spawn = Random.insideUnitCircle.normalized;
        }

        return distance_spawn;
    }


    public void no_combat_wander(float speed)
    {
        //Get current position
        Vector2 position = rigidbody2D.position;

        //If we decide to move
        if (move >= 0.4f) //60% posibilities to move
        {

            //If the timer is 0 it means we start the movement: Direction & timer to be defined.
            if (timer == 0)
            {
                //Random timer for movement
                timer = Random.Range(min_action_time, max_action_time);

                //Obtain target movement vector from spawnpoint and current position
                target_direction = spawnpoint_wander(spawnpoint, position);

            }

            //Set the new position based on speed, direction & timedelta
            position = position + Time.deltaTime * speed * target_direction;

            //Set the new position
            rigidbody2D.MovePosition(position);

            //Reduce the timer
            timer -= Time.deltaTime;

            //Check if the timer has ended
            if (timer <= 0)
            {
                //Recalculate if we keep moving or idle
                move = Random.value;

                //Reset the timer
                timer = 0f;
            }

        }

        //if we decide to idle
        if (move < 0.4f) //40% possibilities to idle
        {
            //If the timer is 0 it means we start the idling: timer to be defined.
            if (timer == 0)
            {
                //Random timer 
                timer = Random.Range(min_action_time, max_action_time);
            }

            //Reduce the timer
            timer -= Time.deltaTime;

            //Check if the timer has ended
            if (timer <= 0)
            {
                //Recalculate if we keep moving or idle
                move = Random.value;

                //Reset the timer
                timer = 0f;
            }
        }
    }



    public void combat_positioning (float speed, GameObject target)
    {
        
        Vector2 current_position = rigidbody2D.position; //Enemy position
        Vector2 target_position = target.transform.position; //Target position

        Vector2 direction = target_position - current_position; //Vector between enemy and target
        direction = direction.normalized; //Normalize it to avoid bigger speeds at longer distances

        current_position = current_position + Time.deltaTime * speed * direction; //Update the current position with the desired one (towards the target)

        //Set the new position
        rigidbody2D.MovePosition(current_position);


    }


}
