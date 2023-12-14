using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChakraRestorePrefab : MonoBehaviour
{

    //Timer for regen ticks
    float timer;

    [HideInInspector]
    public float ChakraRestoreRate;

    //Vector to save the initial position (we have to stay still to restore)
    Vector2 oldposition;

    //Caster resources
    [HideInInspector]
    public GameObject caster;
    StatSystem casterstats;

    // Start is called before the first frame update
    void Start()
    {
        //Get caster position and stats
        casterstats = caster.GetComponent<StatSystem>();
        oldposition = caster.GetComponent<Rigidbody2D>().position;

        //Set player to casting to avoid launching other abilities meanwhile restoring chakra
        casterstats.casting = true;

        //Set timer to 0
        timer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        //Update the timer
        timer += Time.deltaTime;

        //If we havent moved, the stamina is above minimum & we dont have maximum chakra
        if (caster.GetComponent<Rigidbody2D>().position == oldposition && casterstats.currentSta > 1 && casterstats.currentChakra < casterstats.maxChakra)
        {
            //Check the timer tick
            if (timer > 0.25f)
            {
                //Update the chakra
                casterstats.ChangeChakra(ChakraRestoreRate);
                //Update the stamina
                casterstats.ChangeSta(-1);
                //Reset the timer
                timer = 0;
            }

        }
        //If we have moved or we dont have enough stamina or we have maximum chakra, stop the restoration
        else
        {
            casterstats.casting = false;
            Destroy(gameObject); //Destroy the gameObject
        }
    }
}
