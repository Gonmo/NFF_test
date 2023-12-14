using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AbilityHolder : MonoBehaviour
{
    //Ability data input (activate requirements)
    public Ability ability;

    //Internal variable for cooldown management
    [HideInInspector]
    public float cooldownTime;
    float castTime;
    Vector2 oldposition;

    //Caster resources
    public StatSystem casterstats;

    CastBar castbar; //Cast bar UI slider script

    public Image cooldownShader = null; //Link the cooldown shader for the button (if Main Character)

    //Possible ability states
    enum AbilityState
    {
        ready,
        casting,
        cooldown
    }
    
    AbilityState state = AbilityState.ready;

    void Start()
    {
        castbar = GetComponentInChildren<CastBar>(); //Get cast bar reference (if existing)
    }


    // Update is called once per frame
    public void ActivateHolder()
    {
        
        //If ability is ready
        if (state == AbilityState.ready)
        {
            if (casterstats.currentSta >= ability.reqSta && casterstats.currentChakra >= ability.reqChakra && casterstats.casting == false)
            {
                //Start casting
                state = AbilityState.casting;

                //If there is cast time, stop the movement
                if(ability.castTime > 0f)
                {
                    gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                    castTime = 0f;
                    oldposition = gameObject.GetComponent<Rigidbody2D>().position;
                    casterstats.casting = true;
                }

            }
        }

    }



    void Update()
    {

        //If we are casting the ability
        if(state == AbilityState.casting)
        {
            //If the cast time is 0 (instant), activate the ability
            if (ability.castTime == 0)
            {
                //Remove chakra/sta cost
                casterstats.currentSta -= ability.reqSta;
                casterstats.currentChakra -= ability.reqChakra;


                //If the main character uses the ability, update the UI
                if (gameObject.name == "MainCharacter")
                {
                    //Update sta/chakra bar
                    UIStaBar.instance.SetValue(casterstats.currentSta / (float)casterstats.maxSta);
                    UIChakraBar.instance.SetValue(casterstats.currentChakra / (float)casterstats.maxChakra);

                    if(cooldownShader != null)
                    {
                        cooldownShader.gameObject.SetActive(true); //Activate the cooldown shader
                        cooldownShader.fillAmount = 100.0f; //Initialize the shader
                    }

                }

                //Activate the ability
                ability.Activate(gameObject);

                //Set ability in cooldown
                state = AbilityState.cooldown;
                cooldownTime = ability.cooldownTime;

            }


            //In any other case (no instant ability):
            if (ability.castTime > 0)
            {
                //Check if the cast time has already been met. If so, launch the ability
                if (castTime >= ability.castTime)
                {
                    //Remove chakra/sta cost
                    casterstats.currentSta -= ability.reqSta;
                    casterstats.currentChakra -= ability.reqChakra;


                    //If the main character uses the ability, update the UI
                    if (gameObject.name == "MainCharacter")
                    {
                        //Update sta/chakra bar
                        UIStaBar.instance.SetValue(casterstats.currentSta / (float)casterstats.maxSta);
                        UIChakraBar.instance.SetValue(casterstats.currentChakra / (float)casterstats.maxChakra);

                        //Cooldown Shader for the button
                        if (cooldownShader != null)
                        {
                            cooldownShader.gameObject.SetActive(true); //Activate the cooldown shader
                            cooldownShader.fillAmount = 100.0f; //Initialize the shader
                        }
                    }

                    //Activate the ability
                    ability.Activate(gameObject);

                    //Set ability in cooldown
                    state = AbilityState.cooldown;
                    cooldownTime = ability.cooldownTime;
                    castTime = 0f;

                    //If existing, update the castBar UI back to 0 and finish it
                    if (castbar != null)
                    {
                        castbar.UpdateCastBar(0f, ability.castTime);
                        castbar.FinishCastBar();
                    }
                    casterstats.casting = false;
                }

                //If we have cast time and we are still (same position as oldposition, add time to the casttime -->Not working, to be fixed!!
                else if (ability.castTime > 0f & gameObject.GetComponent<Rigidbody2D>().position == oldposition)
                {
                    castTime += Time.deltaTime;

                    //If existing, update the castBar UI
                    if (castbar != null)
                    {
                        castbar.UpdateCastBar(castTime, ability.castTime);
                    }
                }

                //In any other case (we have moved), move the ability back to ready
                else
                {
                    state = AbilityState.ready;
                    castTime = 0f;

                    //If existing, update the castBar UI
                    if (castbar != null)
                    {
                        castbar.UpdateCastBar(castTime, ability.castTime);
                        castbar.FinishCastBar();
                    }
                    casterstats.casting = false;
                }
            }
         



        }



        //If ability is in cooldown
        if(state == AbilityState.cooldown)
        {
            if (cooldownTime > 0)
            {
                //Decrease the cooldown with time
                cooldownTime -= Time.deltaTime;

                if (cooldownShader != null)
                {
                    cooldownShader.fillAmount = cooldownTime / ability.cooldownTime;
                }

            }
            else
            {
                //When cooldown done, set ability as ready
                state = AbilityState.ready;
            }
        }

    }
}
