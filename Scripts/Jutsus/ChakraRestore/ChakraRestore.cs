using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class ChakraRestore : Ability
{

    [Header("Specific")]
    public float ChakraRestoreRate;

    [Header("Animation Prefab")]
    public GameObject ChakraRestoreprefab;

    public override void Activate(GameObject parent)
    {
        //Get rigidbody to stop the restoration when movement starts
        Rigidbody2D rigidbody = parent.GetComponent<Rigidbody2D>();

        StatSystem casterstats = parent.GetComponent<StatSystem>(); //To update the chakra

        //Creation of the effect
        //Positioning on the character position + xf in vertical direction
        //Rotation linked to angle variable.
        GameObject chakrarestoreobject = Instantiate(ChakraRestoreprefab, rigidbody.position + new Vector2 (0,-0.3f), Quaternion.identity);

        ChakraRestorePrefab chakrarestorescript = chakrarestoreobject.GetComponent<ChakraRestorePrefab>();
        chakrarestorescript.caster = parent;
        chakrarestorescript.ChakraRestoreRate = ChakraRestoreRate;

    }
}
