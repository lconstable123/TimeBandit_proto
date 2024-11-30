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
    [SerializeField] bool isOn = true;
    [SerializeField] Cams ChangeToCamera;
    [SerializeField] bool ChangeOut = false;
    [SerializeField] Cams ChangeOutCamera;
    [Header("transitions")]
    [SerializeField] bool HardCut;
    [SerializeField] CinemachineStateDrivenCamera sdc;


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
          sdc.m_DefaultBlend.m_Time = 2f;
            
        } else {
          sdc.m_DefaultBlend.m_Time = 0f;
        }
        cm.ChangeCam(ChangeToCamera);
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
