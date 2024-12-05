using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraQueUnlocker : MonoBehaviour
{
        [Header("Screen Wrapper Manger (reference to enable)")]
    [SerializeField] cameraChanger ch;
    [SerializeField] bool enterstatus;
    [SerializeField] bool exitstatus;


    void OnTriggerEnter(Collider other){

         if(ch !=null && other.CompareTag("Player")){
         //   Debug.Log("turning  "+enterstatus);
        ch.isOn = enterstatus;                    
    }
    }
    void OnTriggerExit(Collider other){
         if(ch !=null && other.CompareTag("Player")){
          //  Debug.Log("turning "+ exitstatus);
         ch.isOn = exitstatus;
                           
    }
    }
}
