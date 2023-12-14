using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Controller : MonoBehaviour
{
    /* -------------- PHYSICS & ANIMATIONS -------------- */
    //Define variable to store a rigidbody2d
    Rigidbody2D rigidbody2d;


    //Define variable to store the animator
    Animator animator;

    //Define target
    [HideInInspector]
    public GameObject target;

    //Variable to define the looking direction (for animations)
    public Vector2 lookDirection = new Vector2(1, 0); 

    public float horizontal = 0f;
    public float vertical = 0f;

    /* -------------- PLAYER STATS -------------- */
    public StatSystem playerstats;

    /* -------------- JUTSUS ------------------- */ 
    public AbilityHolder Fireball;
    public KeyCode fireball_key;

    public AbilityHolder Calabaza;
    public KeyCode calabaza_key;

    public AbilityHolder SShuriken;
    public KeyCode sshuriken_key;

    public AbilityHolder Dash;
    public KeyCode dash_key;

    public AbilityHolder melee;
    public KeyCode melee_key;

    public AbilityHolder cyclone;
    public KeyCode cyclone_key;

    public AbilityHolder chakrarestore;
    public KeyCode chakrarestore_key;


    // Start is called before the first frame update
    void Start()
    {
        //Link the GameObject rigidbody2D with a variable
        rigidbody2d = GetComponent<Rigidbody2D>();

        //Save the linked animator within a variable
        animator = GetComponent<Animator>();

    }


    // Update is called once per frame
    void Update()
    {
        //Update target
        if (target != null)
        {
            playerstats.shootDirection = (target.transform.position - gameObject.transform.position).normalized;
        }

        //Get movement inputs
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        /* -------------- Animations -------------- */
        //Vector to compute animations
        Vector2 move = new Vector2(horizontal, vertical);

        //Math function to check the direction to look
        if (!Mathf.Approximately(move.x, 0.0f) || !Mathf.Approximately(move.y, 0.0f))
        {
            lookDirection.Set(move.x, move.y);
            lookDirection.Normalize();
            if (target == null)
            {
                playerstats.shootDirection = lookDirection;
            } 
            
        }


        /* -------------- Movement & Animations -------------- */
        if (move.magnitude != 0)
        {
            Move(move);
            AnimationManagement(lookDirection, move);
        }
        


        /* -------------- Jutsus -------------- */
        if (Input.GetKeyDown(fireball_key))
        {
            Fireball.ActivateHolder();
        }

        if (Input.GetKeyDown(calabaza_key))
        {
            Calabaza.ActivateHolder();
        }

        if (Input.GetKeyDown(sshuriken_key))
        {
            SShuriken.ActivateHolder();
        }

        if (Input.GetKeyDown(dash_key))
        {
            Dash.ActivateHolder();
        }

        if (Input.GetKeyDown(melee_key))
        {
            StartPunching();
        }

        if (Input.GetKeyDown(cyclone_key))
        {
            cyclone.ActivateHolder();
        }

        if (Input.GetKeyDown(chakrarestore_key))
        {
            chakrarestore.ActivateHolder();
        }

    }

    public void Move(Vector2 move)
    {
        //Get current position as vector (from rigidbody to avoid physics computation lag from transform.position)
        Vector2 position = rigidbody2d.position;

        //Update position vector
        //Time.deltaTime to synchronize different performance CPUs
        //Updated with "move" vector (looking vector) to reduce the coding 1 line
        position = position + move * playerstats.speed * Time.deltaTime;

        //Update rigidbody position with updated position vector
        rigidbody2d.MovePosition(position);
    }

    public void AnimationManagement(Vector2 lookDirection, Vector2 move)
    {
        //Send variables to animator
        animator.SetFloat("Look X", lookDirection.x);
        animator.SetFloat("Look Y", lookDirection.y);
        animator.SetFloat("speed", move.magnitude);
    }


    public void StartPunching()
    {
        animator.SetBool("Punch", true);
        melee.ActivateHolder();
    }

    public void StopPunchAnimation()
    {
        animator.SetBool("Punch", false);
    }


}
