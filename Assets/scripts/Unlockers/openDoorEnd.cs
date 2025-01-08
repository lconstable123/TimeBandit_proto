using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class openDoorEnd: MonoBehaviour
{
    [SerializeField] GameObject doorToOpen;
    public bool isOpen=false;
    public bool locked=true;
    GameSession gs;
    [SerializeField] AudioClip doorSound;
    AudioSource As;
    // Start is called before the first frame update
    void Start()
    {
        As = GetComponent<AudioSource>();
        gs = GameSession.Persistent;
        if(gs.EndDoorOpen){
                HardOpen();
        }

    }


     void OnTriggerEnter(Collider other){


        if (other.gameObject.CompareTag("Player")){
            if(!gs.EndDoorOpen && !isOpen && !locked){
                doorToOpen.GetComponent<Animator>().SetBool("isOpen",true);
                isOpen = true;
                //As.PlayOneShot(doorSound);
                 gs = GameSession.Persistent;
                 gs.EndDoorOpen=true;
            
            }
            }
        }
    void HardOpen(){
        doorToOpen.GetComponent<Animator>().SetTrigger("hardOpen");
        doorToOpen.GetComponent<Animator>().SetBool("isOpen",true);
    }  



    
}
