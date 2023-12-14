using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; //Necessary addition

//Following guide: https://youtu.be/_lREXfAMUcE

public class CastBar : MonoBehaviour
{

    [SerializeField]
    private Slider slider; //Reference to the slider

    //Variables needed to hide the bar until damage is taken
    GameObject background;
    GameObject fillarea;

    void Start()
    {
        //Get the child objects
        background = gameObject.transform.Find("Background").gameObject;
        fillarea = gameObject.transform.Find("FillArea").gameObject;

        //Deactivate the child objects from start
        background.SetActive(false);
        fillarea.SetActive(false);
    }

    public void UpdateCastBar(float castTime, float totalCastTime)
    {
        //If child objects are deactivated (no dmg received yet) and we receive damage (call to this function), unhide the bar
        if (background.activeSelf == false & fillarea.activeSelf == false)
        {
            background.SetActive(true);
            fillarea.SetActive(true);
        }

        slider.value = castTime / totalCastTime; //Update HP bar

    }

    public void FinishCastBar()
    {
        //Get the child objects
        background = gameObject.transform.Find("Background").gameObject;
        fillarea = gameObject.transform.Find("FillArea").gameObject;

        //Deactivate the child objects from start
        background.SetActive(false);
        fillarea.SetActive(false);
    }
}
