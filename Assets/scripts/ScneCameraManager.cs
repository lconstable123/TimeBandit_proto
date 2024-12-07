using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class ScneCameraManager : MonoBehaviour
{
       [Header("Camera State")]

    [SerializeField] CinemachineStateDrivenCamera sdc;
    [SerializeField] Cams startCamera;

    [Header("Set Dolly and Aim")]
    [SerializeField] CinemachineVirtualCamera dollyCamera = null;
    [SerializeField] CinemachineVirtualCamera aimCamera = null;


    //camera things
    private float DollyDamping; 
    float ComposerXDamping;
    float ComposerYDamping;
    private CinemachineTrackedDolly dolly;
    CinemachineComposer comp;
    GameSession gs;



    public void ProcessCams(){

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
        gs = GameSession.Persistent;
        //gs.camerasLocked = true;
        if (gs.camerasLocked == false){
            Debug.Log("locking cams");
            StartCoroutine(IRestore());}else{
                Debug.Log("already lockede");
            }


   }


void ResetDolly(){
   dolly = dollyCamera.GetComponent<CinemachineVirtualCamera>().GetCinemachineComponent<CinemachineTrackedDolly>();
   
   DollyDamping = dolly.m_XDamping;
   dolly.m_XDamping=0;

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
    gs.camerasLocked = true;

    yield return new WaitForSeconds(4f);
        if (sdc !=null){sdc.m_DefaultBlend.m_Time = 2f;};
        if (dollyCamera != null){dolly.m_XDamping = DollyDamping;}
        if (comp != null){
            comp.m_VerticalDamping = ComposerXDamping;
            comp.m_HorizontalDamping = ComposerYDamping;
        }
        gs.camerasLocked= false;
   }


}
