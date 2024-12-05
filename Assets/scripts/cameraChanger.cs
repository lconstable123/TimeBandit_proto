using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Cinemachine;

public class cameraChanger : MonoBehaviour
{
    GameSession gs;
    cameraManager cm;
    Cams cams;
    [Header("states")]
    [SerializeField] public bool isOn = true;
    [SerializeField] bool turnOffAfterChange=false;
    [SerializeField] Cams ChangeToCamera;
    [SerializeField] bool ChangeOut = false;
    [SerializeField] Cams ChangeOutCamera;
    [Header("transitions")]
    [SerializeField] bool HardCut;
    [SerializeField] CinemachineStateDrivenCamera sdc;
    // [Header("Screen Wrapper Manger (reference to enable)")]
    // [SerializeField] Screen_wrapper sw;
    // [SerializeField] bool screenwrapperstatus;


    void Start()
    {
      gs = GameSession.Persistent;
      if (gs==null){
      }  
      cm = gs.GetComponent<cameraManager>();
       if (cm==null){
      }  

    }
    
    void OnTriggerEnter(Collider other){
        if (other.CompareTag("Player" ) && isOn){

  if (!HardCut){
          if (sdc!=null){sdc.m_DefaultBlend.m_Time = 2f;}
            
        } else {
          if(sdc!=null){sdc.m_DefaultBlend.m_Time = 0f;}
        }
        cm.ChangeCam(ChangeToCamera);
        }
  if (turnOffAfterChange){
    isOn=false;
  }



    }

    void OnTriggerExit(Collider other){
      if (ChangeOut){
        if (other.CompareTag("Player") && isOn ){

         if (!HardCut){
          sdc.m_DefaultBlend.m_Time = 2f;
            
        } else {
          sdc.m_DefaultBlend.m_Time = 0f;
        }
            cm.ChangeCam(ChangeOutCamera);
        }
       
    }
    }
}
