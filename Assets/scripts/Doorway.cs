using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Cinemachine;

using UnityEngine;

using UnityEngine.SceneManagement;

public class Doorway : MonoBehaviour
{

    
    //Animator animator;
    [Header("Door Info")]
    public int doorId;
    public bool doorOpen;
    
    [SerializeField] string whereTo;
    [SerializeField] int doorToId;

    [Header("Transition")]
    [SerializeField] float transitionDuration;

    [Header("Spawn Tweaking")]
    //[SerializeField] bool isSpawn;
    [SerializeField] public float SpawnVel;
    [SerializeField] float SpwanHeight;
    [SerializeField] float SpawnDist;

    [Header("Camera State")]
    [SerializeField] bool StateDrivenCameraReset;
    [SerializeField] CinemachineStateDrivenCamera sdc;
    [SerializeField] Cams startCamera;
    
    [SerializeField] bool returnToFlowCam=true;
    GameSession gs;
    GameObject player;


    void Start()
    {
        //animator = ppvol.GetComponent<Animator>();
        gs = GameSession.Persistent;
        // int enteredDoor = gs.GetDoorEntered();
        // if(enteredDoor == doorId){
        //     Debug.Log("door id");
        //     ProcessSpawn();
        // }
       // if (isSpawn){ProcessSpawn();};


    }

    void OnTriggerEnter(Collider other){
        if (other.CompareTag("Player")){
            if (doorOpen){
                gs.SetDoorEntered(doorToId);
                
                
                gs.LeaveScene(whereTo);   

            } 
        }
    }
 
    public void BruteLoadScene(){
         Debug.Log("entered doorway");
                SceneManager.LoadScene(whereTo);
    }
   
   public Vector3 ProcessSpawn(){
            player = FindObjectOfType<PlayerController>().gameObject;

               if (StateDrivenCameraReset){
                gs = GameSession.Persistent;
                Animator an =gs.GetComponent<Animator>();
                sdc.m_AnimatedTarget = an;
                sdc.m_DefaultBlend.m_Time = 0f;
                gs.GetComponent<cameraManager>().ChangeCam(startCamera);   
                //if(returnToFlowCam){
                StartCoroutine(ResetCameraDelay());
               // }; 
                //sdc.m_DefaultBlend.m_Time = 2f;
                
            }
                //sdc.m_DefaultBlend.m_Time = 0f;
                
                //cinemachineBrain.m_DefaultBlend.m_Time = blendTime;
            
            if (player == null){
               // gs.debug.text = "no player found";
               // Debug.Log("no player found");
                } else {
                  //  gs.debug.text = "found a player";
            }
            Vector3 spawnPos = transform.position-transform.forward*SpawnDist;
            spawnPos += new Vector3(0f,SpwanHeight,0f);
            

         
            return spawnPos;


   }

   IEnumerator ResetCameraDelay(){
    yield return new WaitForSeconds(2);
    sdc.m_DefaultBlend.m_Time = 2f;
   }

   

}
