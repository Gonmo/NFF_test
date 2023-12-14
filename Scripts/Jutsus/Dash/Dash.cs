using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Dash : Ability
{
    [Header("Specific")]
    public float dashStep;


    public override void Activate(GameObject parent)
    {
        //Get rigidbody and casterstats from parent (for shootdirection)
        Rigidbody2D rigidbody = parent.GetComponent<Rigidbody2D>();

        StatSystem casterstats = parent.GetComponent<StatSystem>(); //shoot direction to bhe changed to lookdirection when target system is applied

        //Update position with the dashStep
        Vector2 position = rigidbody.position;
        position = position + casterstats.shootDirection.normalized * dashStep * Time.deltaTime;
        rigidbody.MovePosition(position);
    }
}
