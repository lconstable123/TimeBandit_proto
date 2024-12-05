using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenWrapperQue : MonoBehaviour
{
        [Header("Screen Wrapper Manger (reference to enable)")]
    [SerializeField] Screen_wrapper sw;
    [SerializeField] bool enterstatus;
    [SerializeField] bool exitstatus;


    void OnTriggerEnter(Collider other){

         if(sw !=null && other.CompareTag("Player")){
            Debug.Log("turning  "+enterstatus);
        sw.enabled = enterstatus;                    
    }
    }
    void OnTriggerExit(Collider other){
         if(sw !=null && other.CompareTag("Player")){
            Debug.Log("turning "+ exitstatus);
         sw.enabled = exitstatus;                    
    }
    }
}
