using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayCharacterAnimation : MonoBehaviour
{

    [SerializeField] Animator an;
    [SerializeField] TB_animator tbAn;
    [SerializeField] PlayerController pc;
    [SerializeField] Transform sleeppos;
    
    GameSession gs;
    public bool locked = true;
    // Start is called before the first frame update
    void Start()
    {

         gs = GameSession.Persistent;
         int enteredDoor = gs.GetDoorEntered();
         if(enteredDoor == 0){
            
            an.SetTrigger("sleepNow");
            

            //pc.movingMode = m
            sleep(false);

         }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter(Collider other){
        if(!locked){
        if (other.gameObject.CompareTag("Player")){

            //Debug.Log("time to sleep");
            locked = true;
            if (an == null){Debug.Log("no animator");};
           sleep(true);
    }
}
    }
void sleep(bool hardlock){
    pc.sleep(hardlock,sleeppos);
    tbAn.Sleep();
    
}
}
