using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room1_reset : MonoBehaviour
{
    [Header("global")]
    [SerializeField] int OnEnteringDoor;
    [SerializeField] GameObject AssetToReset;
    [Header("Set animation")]
    [SerializeField] string AnimationToPlay;
    [SerializeField] string BoolToSet;
    [SerializeField] bool StateToSet;
    
    void Start()
    {
        GameSession gs = GameSession.Persistent;
        
        if (gs.enteredDoor == OnEnteringDoor){
            Animator an = AssetToReset.GetComponent<Animator>(); 
            an.Play(AnimationToPlay);
            an.SetBool(BoolToSet,StateToSet);
        }
        
       
    }

}
