using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class openDoorAnim : MonoBehaviour
{
    [SerializeField] GameObject doorToOpen;
    public bool isOpen=false;
    GameSession gs;
    [SerializeField] AudioClip doorSound;
    AudioSource As;
    // Start is called before the first frame update
    void Start()
    {
        As = GetComponent<AudioSource>();
        gs = GameSession.Persistent;
        if(gs.centralDoorOpen){
                HardOpen();
        }
        if(gs.centralRoomFirstTime){
            StartCoroutine(doorCloseAfterDelay());
            gs.centralRoomFirstTime = false;
        }
    }


     void OnTriggerEnter(Collider other){


        if (other.gameObject.CompareTag("Player")){
            if(!gs.centralDoorOpen && !isOpen){
                doorToOpen.GetComponent<Animator>().SetBool("isOpen",true);
                isOpen = true;
                As.PlayOneShot(doorSound);
                 gs = GameSession.Persistent;
                 gs.centralDoorOpen=true;
            
            }
            }
        }
    void HardOpen(){
        doorToOpen.GetComponent<Animator>().SetTrigger("hardOpen");
        doorToOpen.GetComponent<Animator>().SetBool("isOpen",true);
    }  

    IEnumerator doorCloseAfterDelay(){
        yield return new WaitForSeconds(1.5f);
        doorToOpen.GetComponent<Animator>().SetBool("isOpen",false);
        As.PlayOneShot(doorSound);
        gs.centralDoorOpen=false;
    }

    
}
