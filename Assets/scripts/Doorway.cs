using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using System.Runtime.CompilerServices;
using Cinemachine;

using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.SceneManagement;

public class Doorway : MonoBehaviour
{

    

    [Header("Door Info")]
    public int doorId;
    public bool doorOpen;
    
    [SerializeField] string whereTo;
    [SerializeField] int doorToId;

    [Header("Transition")]
    [SerializeField] float transitionDuration;

    [Header("Spawn Tweaking")]
    [SerializeField] public float SpawnVel;
    [SerializeField] float SpwanHeight;
    [SerializeField] float SpawnDist;

    [Header("Camera State")]

    [SerializeField] CinemachineStateDrivenCamera sdc;
    [SerializeField] Cams startCamera;

    [Header("Set Dolly and Aim")]
    [SerializeField] CinemachineVirtualCamera dollyCamera = null;
    [SerializeField] CinemachineVirtualCamera aimCamera = null;

    GameSession gs;
    private float DollyDamping; 
    float ComposerXDamping;
    float ComposerYDamping;
    private CinemachineTrackedDolly dolly;
    CinemachineComposer comp;
    
    void Start()
    {
        gs = GameSession.Persistent;
    }

    void OnTriggerEnter(Collider other){
        if (other.CompareTag("Player")){
            if (doorOpen){
                gs.SetDoorEntered(doorToId);  
                gs.LeaveScene(whereTo);   

            } 
        }
    }
 
    
   
   public Vector3 ProcessSpawn(){

        if (sdc != null)
        {
            ResetSDC();  
        }
               
        if (dollyCamera != null){
            ResetDolly();  
        }
        if (aimCamera != null){
            ResetAim();   
        }

        StartCoroutine(IRestore());


        Vector3 spawnPos = transform.position-transform.forward*SpawnDist;
        spawnPos += new Vector3(0f,SpwanHeight,0f);
        return spawnPos;
   }


void ResetDolly(){
   dolly = dollyCamera.GetComponent<CinemachineVirtualCamera>().GetCinemachineComponent<CinemachineTrackedDolly>();
   
   DollyDamping = dolly.m_XDamping;
   dolly.m_XDamping=0;
   
//    comp = dollyCamera.GetComponent<CinemachineVirtualCamera>().GetCinemachineComponent<CinemachineComposer>();
//     if (comp != null){
//    ComposerXDamping = comp.m_VerticalDamping;
//    ComposerYDamping = comp.m_HorizontalDamping;
//    comp.m_VerticalDamping = 0;
//    comp.m_HorizontalDamping = 0;
//     }
dolly.m_AutoDolly.m_Enabled = true;
   
                   
}

void ResetAim(){
   comp = aimCamera.GetComponent<CinemachineVirtualCamera>().GetCinemachineComponent<CinemachineComposer>();
     if (comp != null){
   ComposerXDamping = comp.m_VerticalDamping;
   ComposerYDamping = comp.m_HorizontalDamping;
   comp.m_VerticalDamping = 0;
   comp.m_HorizontalDamping = 0;
     }
}



    private void ResetSDC()
    {
        gs = GameSession.Persistent;
        Animator an = gs.GetComponent<Animator>();
        sdc.m_AnimatedTarget = an;
        sdc.m_DefaultBlend.m_Time = 0f;

        gs.GetComponent<cameraManager>().ChangeCam(startCamera);
        
    }

    IEnumerator IRestore(){
    yield return new WaitForSeconds(2);

        if (sdc !=null){sdc.m_DefaultBlend.m_Time = 2f;};
        if (dollyCamera != null){dolly.m_XDamping = DollyDamping;}
        if (comp != null){
            comp.m_VerticalDamping = ComposerXDamping;
            comp.m_HorizontalDamping = ComposerYDamping;
        }
   }

   public void BruteLoadScene(){
        Debug.Log("entered doorway");
        SceneManager.LoadScene(whereTo);
    }

}
