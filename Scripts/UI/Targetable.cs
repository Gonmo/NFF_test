using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Targetable : MonoBehaviour
{
    public GameObject mainplayer;

    [SerializeField]
    GameObject targetimage; //Gameobject with the target Image

    void Start()
    {
        //Deactivate the target from start
        targetimage.SetActive(false);
    }

    //When the gameobject is selected (mouse or pressed)
    void OnMouseDown()
    {
        //Set the Main Character target
        Player_Controller playerscript = mainplayer.GetComponent<Player_Controller>();

        //Deactivate the target from previous target (if existent)
        if (playerscript.target != null )
        {
            GameObject oldtarget = playerscript.target;
            
            if (oldtarget != null )
            {
                oldtarget.transform.Find("Canvas").transform.Find("Target").gameObject.SetActive(false);
            }

        }

        playerscript.target = gameObject;

        //Activate the target sprite
        targetimage.SetActive(true);

    }
}
