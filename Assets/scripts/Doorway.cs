using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Cinemachine;

using UnityEngine;

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

    [Header("Set Dolly")]
    [SerializeField] CinemachineVirtualCamera dollyCamera = null;

    GameSession gs;
    private float DollyDamping; 
    private CinemachineTrackedDolly dolly;
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
            StartCoroutine(ResetCameraDelay());
        }

        Vector3 spawnPos = transform.position-transform.forward*SpawnDist;
        spawnPos += new Vector3(0f,SpwanHeight,0f);
        return spawnPos;
   }


void SetDolly(){
   
   dolly = dollyCamera.GetComponent<CinemachineVirtualCamera>().GetCinemachineComponent<CinemachineTrackedDolly>();
   dolly.m_AutoDolly.m_Enabled = true;
   DollyDamping = dolly.m_XDamping;
   dolly.m_XDamping=0;
                   


}





    private void ResetSDC()
    {
        gs = GameSession.Persistent;
        Animator an = gs.GetComponent<Animator>();
        sdc.m_AnimatedTarget = an;
        sdc.m_DefaultBlend.m_Time = 0f;

        gs.GetComponent<cameraManager>().ChangeCam(startCamera);

        if (dollyCamera!=null){
            SetDolly();
        }

    }

    IEnumerator ResetCameraDelay(){
    yield return new WaitForSeconds(2);
    sdc.m_DefaultBlend.m_Time = 2f;

    if (dollyCamera != null){
        
        dolly.m_XDamping=DollyDamping;
    }
   }

   public void BruteLoadScene(){
        Debug.Log("entered doorway");
        SceneManager.LoadScene(whereTo);
    }

}
