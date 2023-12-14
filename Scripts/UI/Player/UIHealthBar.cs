using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIHealthBar : MonoBehaviour
{
    //Allow to use UIHealthBar functions from any other script without reference
    public static UIHealthBar instance { get; private set; }

    //Reference to the HP_mask
    public Image mask;

    //float with the original HP bar length
    float originalSize;


    void Awake()
    {
        instance = this;
    }



    // Start is called before the first frame update
    void Start()
    {
        originalSize = mask.rectTransform.rect.width;
    }

    public void SetValue(float value)
    {
        mask.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, originalSize * value);
    }

}
