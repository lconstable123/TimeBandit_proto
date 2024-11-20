using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class cameraChanger : MonoBehaviour
{
    GameSession gs;
    cameraManager cm;
    Cams cams;
    [SerializeField] Cams ChangeToCamera;
    [SerializeField] Cams ChangeOutCamera;
    // Start is called before the first frame update
    void Start()
    {
      gs = GameSession.Persistent;
      if (gs==null){
        Debug.Log("gmaeobe not ofund");
      }  
      cm = gs.GetComponent<cameraManager>();
       if (cm==null){
        Debug.Log("camMonager not ofund");
      }  
    }
    void Update(){
       // if (Input.GetKeyDown(KeyCode.Alpha3)){
        //    Debug.Log("pressed");
          // cm.ChangeCam(Cams.followcam);
       // }



    }
    
    void OnTriggerEnter(Collider other){
        if (other.CompareTag("Player")){
           //Debug.Log("enter change");
            cm.ChangeCam(ChangeToCamera);
        }
    }
    void OnTriggerStay(Collider other){
        if (other.CompareTag("Player")){
          Debug.Log("change coamera volume " + ChangeToCamera.ToString());
            cm.ChangeCam(ChangeToCamera);
        }
}
    void OnTriggerExit(Collider other){
        if (other.CompareTag("Player") ){
       //  Debug.Log("out change");
            cm.ChangeCam(ChangeOutCamera);
        }
    }
}
