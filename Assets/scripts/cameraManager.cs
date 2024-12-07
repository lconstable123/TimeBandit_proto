using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class cameraManager : MonoBehaviour
{
    Animator animator;
    
    public Cams cams;
    
 
   GameSession gs;
    


    void Start()
    {
    animator = GetComponent<Animator>();
    //gs= GetComponent<GameSession>();
    
    }


    void Update()
    {

    }

    public void ChangeCam(Cams changecam){
        animator = GetComponent<Animator>();
        if(cams != changecam){
            cams = changecam;
        switch (changecam){
            case(Cams.cam1):
              //  animator.SetTrigger("cam1");
                animator.SetInteger("cam",1);
                // Debug.Log("cam1");
                break;
            
            case(Cams.cam2):
                //animator.SetTrigger("cam2");
                animator.SetInteger("cam",2);
                // Debug.Log("cam2");
                break;
            
            case(Cams.cam3):
               // animator.SetTrigger("cam3");
               animator.SetInteger("cam",3);
                // Debug.Log("cam3");
                break;

            case(Cams.cam4):
                //animator.SetTrigger("wallcam");
                animator.SetInteger("cam",4);
                break;
            case(Cams.cam5):
                //animator.SetTrigger("wallcam");
                animator.SetInteger("cam",5);
                break;


            default:
                animator.SetInteger("cam",1);
                break;


        }
        }


    }


    
}
