using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Guide used: https://youtu.be/1fBKVWie8ew, managed mostly on AbilityHolder
public class CooldownManagement : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        gameObject.SetActive(false);
    }

}
