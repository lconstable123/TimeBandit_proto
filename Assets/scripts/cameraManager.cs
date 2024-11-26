using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraManager : MonoBehaviour
{
    Animator animator;
    
    public Cams cams;
    
    

    // Start is called before the first frame update
    void Start()
    {
    animator = GetComponent<Animator>();
    
    }

    // Update is called once per frame
    void Update()
    {
        // if (Input.GetKeyDown(KeyCode.Alpha1)){
        //     Debug.Log("pressed");
            
        // }
        // if (Input.GetKeyDown(KeyCode.Alpha2)){
        //     Debug.Log("pressed");
            
        // }
        // if (Input.GetKeyDown(KeyCode.Alpha3)){
        //     Debug.Log("pressed");
           
        // }
    }

    public void ChangeCam(Cams changecam){
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
                animator.SetInteger("cam",3);
                break;


            default:
                animator.SetInteger("cam",1);
                break;


        }
        }


    }
    
}
