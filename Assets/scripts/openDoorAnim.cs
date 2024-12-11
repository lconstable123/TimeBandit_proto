using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class openDoorAnim : MonoBehaviour
{
    [SerializeField] GameObject doorToOpen;
    public bool isOpen=false;
    GameSession gs;
    // Start is called before the first frame update
    void Start()
    {
        gs = GameSession.Persistent;
        if(gs.centralDoorOpen){
                HardOpen();

        }
    }


     void OnTriggerEnter(Collider other){


        if (other.gameObject.CompareTag("Player")){
            if(!gs.centralDoorOpen && !isOpen){
                doorToOpen.GetComponent<Animator>().SetBool("isOpen",true);
                isOpen = true;
                 gs = GameSession.Persistent;
                 gs.centralDoorOpen=true;
            
            }
            }
        }
    void HardOpen(){
        doorToOpen.GetComponent<Animator>().SetTrigger("hardOpen");
        doorToOpen.GetComponent<Animator>().SetBool("isOpen",true);
    }
}
