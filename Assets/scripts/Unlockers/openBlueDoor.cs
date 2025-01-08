using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class openBlueDoor : MonoBehaviour
{
    [SerializeField] GameObject doorToOpen;
    [SerializeField] AudioClip openSound;
    AudioSource ac;
    public bool isOpen=false;
    GameSession gs;
    // Start is called before the first frame update
    void Start()
    {
        gs = GameSession.Persistent;
        ac = GetComponent<AudioSource>();
        if(gs.blueDoorOpen){
                HardOpen();

        }
    }


     void OnTriggerEnter(Collider other){


        if (other.gameObject.CompareTag("Player")){
            if(!gs.blueDoorOpen && !isOpen){
                
                doorToOpen.GetComponent<Animator>().SetBool("isOpen",true);
                isOpen = true;
                 gs = GameSession.Persistent;
                 gs.blueDoorOpen=true;
                 if (ac != null){
                    ac.PlayOneShot(openSound);
                 }
            
            }
            }
        }
    void HardOpen(){
        doorToOpen.GetComponent<Animator>().SetBool("isOpen",true);
        doorToOpen.GetComponent<Animator>().SetTrigger("hardOpen");
        
    }
}
