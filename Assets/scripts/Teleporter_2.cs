using System.Collections;
using Cinemachine;
using UnityEngine;

public class Teleporter_2 : MonoBehaviour
{

    

    [Header("Door Info")]

    [SerializeField] GameObject whereTo;
     [SerializeField] float impulse;
     [Header("camera resetting")]
     [SerializeField] ScneCameraManager scm;
     [SerializeField] Vector3 spawndirection;
     [SerializeField] float spawnrot;



    // [Header("Camera State")]

    // [SerializeField] CinemachineStateDrivenCamera sdc;
    // [SerializeField] Cams startCamera;

    // [Header("Set Dolly and Aim")]
    // [SerializeField] CinemachineVirtualCamera dollyCamera = null;
    // [SerializeField] CinemachineVirtualCamera aimCamera = null;

    // GameSession gs;

    // //camera things
    // private float DollyDamping; 
    // float ComposerXDamping;
    // float ComposerYDamping;
    // private CinemachineTrackedDolly dolly;
    // CinemachineComposer comp;
    
    void Start()
    {
        // gs = GameSession.Persistent;
    }

    void OnTriggerEnter(Collider other){
        if (other.CompareTag("Player")){
           
            if (scm != null){
                scm.ProcessCams();
            }
            Teleport(other);
        }
    }
 
void Teleport(Collider other){
    other.transform.position = whereTo.transform.position;
    //other.transform.rotation = transform.rotation;
    Rigidbody rb =other.GetComponent<Rigidbody>();
    //rb.rotation = Quaternion.Euler(new Vector3(0,spawnrot,0));          
    Vector3 force = spawndirection* impulse;
    rb.AddForce(force, ForceMode.Force);
}
   
//    void ProcessCams(){

//         if (sdc != null)
//         {
//             ResetSDC();  
//         }
               
//         if (dollyCamera != null){
//             ResetDolly();  
//         }
//         if (aimCamera != null){
//             ResetAim();   
//         }

//         StartCoroutine(IRestore());


//    }


// void ResetDolly(){
//    dolly = dollyCamera.GetComponent<CinemachineVirtualCamera>().GetCinemachineComponent<CinemachineTrackedDolly>();
   
//    DollyDamping = dolly.m_XDamping;
//    dolly.m_XDamping=0;

// dolly.m_AutoDolly.m_Enabled = true;
   
                   
// }

// void ResetAim(){
//    comp = aimCamera.GetComponent<CinemachineVirtualCamera>().GetCinemachineComponent<CinemachineComposer>();
//      if (comp != null){
//    ComposerXDamping = comp.m_VerticalDamping;
//    ComposerYDamping = comp.m_HorizontalDamping;
//    comp.m_VerticalDamping = 0;
//    comp.m_HorizontalDamping = 0;
//      }
// }



//     private void ResetSDC()
//     {
//         gs = GameSession.Persistent;
//         Animator an = gs.GetComponent<Animator>();
//         sdc.m_AnimatedTarget = an;
//         sdc.m_DefaultBlend.m_Time = 0f;

//         gs.GetComponent<cameraManager>().ChangeCam(startCamera);
        
//     }

//     IEnumerator IRestore(){
//     yield return new WaitForSeconds(2);

//         if (sdc !=null){sdc.m_DefaultBlend.m_Time = 2f;};
//         if (dollyCamera != null){dolly.m_XDamping = DollyDamping;}
//         if (comp != null){
//             comp.m_VerticalDamping = ComposerXDamping;
//             comp.m_HorizontalDamping = ComposerYDamping;
//         }
//    }


}
