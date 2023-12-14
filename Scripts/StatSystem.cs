using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatSystem : MonoBehaviour
{
    //Health system
    public float maxHealth;
    [HideInInspector]
    public float currentHealth;
    NPCHealthBar NPChealthbar; //Health bar for NPCs

    //Chakra system
    public float maxChakra;
    [HideInInspector]
    public float currentChakra;

    //Stamina system
    public float maxSta;
    [HideInInspector]
    public float currentSta;
    public float StaRegen = 0.25f;
    float StaTimer = 0f;

    //Speed
    public float speed;

    //ShootDirection
    [HideInInspector]
    public Vector2 shootDirection = new Vector2(1, 0);

    //Casting
    public bool casting;

    // Start is called before the first frame update
    void Start()
    {
        //Set initial stats
        currentHealth = maxHealth;
        currentChakra = maxChakra;
        currentSta = maxSta;

        casting = false;

        NPChealthbar = GetComponentInChildren<NPCHealthBar>(); //Get NPC health bar reference (if existing)
    }

    public void ChangeHealth(float amount)
    {
        //Update the current health
        currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);

        //Update the HP bar if main character
        if (gameObject.name == "MainCharacter")
        {
            UIHealthBar.instance.SetValue(currentHealth / maxHealth);
        }
        else if (currentHealth == 0) //If not the main Character, destroy gameObject on 0 health
        {
            Destroy(gameObject);
        }
        else if (NPChealthbar != null) //If a NPC healthbar exists, update it
        {
            NPChealthbar.UpdateHealthBar(currentHealth, maxHealth);
        }
 
    }

    public void ChangeChakra(float amount)
    {
        //Update the current health
        currentChakra = Mathf.Clamp(currentChakra + amount, 0, maxChakra);

        //Update the HP bar if main character
        if (gameObject.name == "MainCharacter")
        {
            UIChakraBar.instance.SetValue(currentChakra / maxChakra);
        }

    }

    public void ChangeSta(float amount)
    {
        //Update the current health
        currentSta = Mathf.Clamp(currentSta + amount, 0, maxSta);

        //Update the HP bar if main character
        if (gameObject.name == "MainCharacter")
        {
            UIStaBar.instance.SetValue(currentSta / (float)maxSta);
        }

    }

    void Update()
    {
        //Passive Stamina update
        StaTimer += Time.deltaTime;
        if (StaTimer >= 0.25f)
        {
            ChangeSta(StaRegen);
            StaTimer = 0;
        }
    }
}
