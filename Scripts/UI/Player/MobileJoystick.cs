using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems; //To use IPointerUpHandler, IDragHandler

//Guide from: https://youtu.be/UCJXgwSnXuU

public class MobileJoystick : MonoBehaviour, IPointerUpHandler, IDragHandler, IPointerDownHandler
{

    private RectTransform joystickTransform;

    [SerializeField]
    private float dragMovementDistance = 30; //Distance the joystick moves in the screen
    [SerializeField]
    private int dragOffsetDistance = 150; //Relation between drag distance and joystick movement (size of joystick area)
    [SerializeField]

    //MainCharacter gameObject (to change its position)
    GameObject MainCharacter;
    Player_Controller MainCharacterScript;

    //Variable to define the looking direction (for animations)
    Vector2 lookDirection = new Vector2(1, 0);
    Vector2 offset;
    Vector2 offsetLatch;

    void Start()
    {
        MainCharacterScript = MainCharacter.GetComponent<Player_Controller>(); //Get the movement script
    }

    void Update() 
    {
        // The "OnDrag" method only computes the relative drag from the center if there is movement.
        // If we keep the joystick left, the OnDrag function does not activate, stopping the movement
        // Hence, a latch is required, set at the beginning of the drag and reset "OnPointerUp"
        if(offsetLatch.magnitude > 0)
        {
            MainCharacterScript.Move(offset); //Update the movement
        }
    }

    public void OnDrag(PointerEventData eventData) //What happens when we drag the joystick
    {
        RectTransformUtility.ScreenPointToLocalPointInRectangle( //Transform a screen space point to a position in the local space of a RectTransform that is on the plane of its rectangle.
            joystickTransform, //The RectTransform to find a point inside.
            eventData.position, //The camera associated with the screen space position.
            null, //Screen space position
            out offset); //Point in local space of the rect transform.

        offset = Vector2.ClampMagnitude(offset, dragOffsetDistance) / dragOffsetDistance; //Divide the dragged distance by the relation defined above. The result will be between -1 & 1.

        joystickTransform.anchoredPosition = offset * dragMovementDistance; //Move the joystick

        /* ------------- Send data to MainCharacter -------------- */
        MainCharacterScript.Move(offset); //Update the movement
        offsetLatch = offset;

        //Calculate the lookDirection and pass it to the animator
        if (!Mathf.Approximately(offset.x, 0.0f) || !Mathf.Approximately(offset.y, 0.0f))
        {
            lookDirection.Set(offset.x, offset.y);
            lookDirection.Normalize();
            //Update the shootDirection (only if there is no target already selected
            StatSystem playerstats = MainCharacter.GetComponent<StatSystem>();
            if (MainCharacterScript.target == null) { playerstats.shootDirection = lookDirection; }
            
        }

        MainCharacterScript.AnimationManagement(lookDirection, offset);

    }

    public void OnPointerUp(PointerEventData eventData) //What happens when we stop touching the joystick
    {
        joystickTransform.anchoredPosition = Vector2.zero; //To reset the joystick position back to 0,0 (the center of the joystick area)
        offsetLatch = Vector2.zero; //Reset the latch 
        MainCharacterScript.AnimationManagement(lookDirection, Vector2.zero); //Reset the moving animation
    }

    public void OnPointerDown(PointerEventData eventData) //What happens when we start touching the joystick
    {
        //Needed this function to allow OnPointerUp to happen
    }


    private void Awake()
    {
        joystickTransform = (RectTransform)transform;
    }


}
