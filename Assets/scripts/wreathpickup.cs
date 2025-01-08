using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wreathpickup : MonoBehaviour
{
    [SerializeField] GameObject potwreath;
    [SerializeField] GameObject oldWall;
    [SerializeField] GameObject playerWreath;
    [SerializeField] GameObject newWall;
    [SerializeField] GameObject frame;
    [SerializeField] openDoorEnd door;
    
    GameSession gs;
    // Start is called before the first frame update
    void Start()
    {
        gs = GameSession.Persistent;
        if(gs.EndDoorOpen){
                SetEndState();
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other){
     if (other.gameObject.CompareTag("Player")){
        SetEndState();
    //      if(frame != null){
    //         Animator an = frame.GetComponent<Animator>();
    //             if(an != null){
    //                 an.SetTrigger("on");
    //             }
    //  }
}
    }

void SetEndState(){

   
        if(potwreath != null){
            potwreath.SetActive(false);
        }
        if(oldWall != null){
            oldWall.SetActive(false);
        }
        if(door != null){
            door.locked = false;
        }


        if(playerWreath != null){
            playerWreath.SetActive(true);
        }
        if (newWall != null){
            newWall.SetActive(true);
        }

       
        
     }
}


