using System.Collections;
using System.Collections.Generic;

using Unity.VisualScripting;
using UnityEngine;
using Cinemachine;
using UnityEditor.Rendering;
using System;
//using CinemachineTrackedDolly;

public class teleporter : MonoBehaviour
{
     Transform player; 
     public CinemachineVirtualCamera virtualCamera; 
     CinemachineTrackedDolly dolly;
     [SerializeField] GameObject whereTo;
    // [SerializeField] bool isResetter;/ [SerializeField] bool isParralaxTeleporter;
     [SerializeField] float teleportXdist;
     [SerializeField] float zNormaliser;
     [SerializeField] float teleportZmagnitute;
     [SerializeField] float ParallaxspawnHeight;
     [SerializeField] float impulse;
    [Header("flip X exiting portal")]
     [SerializeField] bool flipX ;
    [SerializeField] float fliptime;
    [SerializeField] PlayerController pc;

    [Header("experiment")]


    // [Header("Screen Wrapper Manger (reference to enable)")]
    // [SerializeField] Screen_wrapper sw;
    // [SerializeField] bool screenwrapperstatus;
    // [SerializeField] bool LR;
     Vector3 teleportPos;

    public enum Mode {
        parallaxResetter =1,
        parralaxWrapper = 2,
        teleporter = 3,

     }
     public enum Side {
        left=1,
        right=2,

     }
     [Header("Modes (mostly obsolete)")]

     [SerializeField] Mode teleportMode;
     [SerializeField] Side side;
     
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter(Collider other){
        if (other.CompareTag("Player"))
        {
            switch (teleportMode){
                case Mode.parallaxResetter:
                    if (virtualCamera != null){
                        dolly = virtualCamera.GetComponent<CinemachineVirtualCamera>().GetCinemachineComponent<CinemachineTrackedDolly>();
                    }
                    if (dolly != null){ dolly.m_AutoDolly.m_Enabled = true; }
                    break;

                case Mode.parralaxWrapper:
                    if (virtualCamera != null){
                        dolly = virtualCamera.GetComponent<CinemachineVirtualCamera>().GetCinemachineComponent<CinemachineTrackedDolly>();
                    }
                    ParallaxTeleport(other);
                    break;

                case Mode.teleporter:
                    if (virtualCamera != null){
                       // Debug.Log("cam found");
                        dolly = virtualCamera.GetComponent<CinemachineVirtualCamera>().GetCinemachineComponent<CinemachineTrackedDolly>();
                    }
                    if (dolly != null){ 
                       // Debug.Log("dolly found found");
                        //dolly.m_AutoDolly.m_Enabled = false; 
                        dolly.m_XDamping=0;
                       // dolly.m_YDamping=0;
                     //   dolly.m_ZDamping=0;
                        StartCoroutine(EnableDamping());
                        }
                    if (flipX && pc != null){
                        pc.flipXInput(fliptime);
                    }
                    // if(sw !=null && screenwrapperstatus == false){
                    //     Debug.Log("disabling screen wrapper");
                    //     sw.enabled = screenwrapperstatus;
                        
                    // }
                    



                    other.transform.position = whereTo.transform.position;
                    other.transform.rotation = transform.rotation;
                    Rigidbody rb =other.GetComponent<Rigidbody>();
                    //if (rb == null){Debug.Log("no");};
                    Vector3 force = transform.forward* impulse;
                    rb.AddForce(force, ForceMode.Force);

                    // if(sw !=null && screenwrapperstatus == true){
                    //     Debug.Log("enabling screen wrapper");
                    //     sw.enabled = screenwrapperstatus;
                        
                    // }
                   //dolly.m_XDamping=1.5f;
                    break;

                default:
                    break;


            }

            // if(isParralaxTeleporter){
            //     if (virtualCamera != null){
            //         dolly = virtualCamera.GetComponent<CinemachineVirtualCamera>().GetCinemachineComponent<CinemachineTrackedDolly>();
            //     }

            //     if (!isResetter){ParallaxTeleport(other);}else{
            //         if (dolly != null)
            //     { dolly.m_AutoDolly.m_Enabled = true; }
            //     }

            // }else{
            //     other.transform.position = whereTo.transform.position;
            // }

        }
        IEnumerator EnableDamping(){
            yield return new WaitForSeconds(.5f);
            // Debug.Log("damped");
            dolly.m_XDamping=1.5f;
           // dolly.m_XDamping=1.5f;
            //dolly.m_ZDamping=1.5f;
        }
    }







    private void ParallaxTeleport(Collider other)
    {
        player = other.transform;
        float Zoffset = (player.transform.position.z+zNormaliser)*teleportZmagnitute;
        Debug.Log(Zoffset);

        if(side==Side.left){
        teleportPos = new(player.transform.position.x+(teleportXdist*Zoffset),
                    ParallaxspawnHeight,
                    player.transform.position.z);
        }else{
        teleportPos = new(player.transform.position.x-(teleportXdist*Zoffset),
                    ParallaxspawnHeight,
                    player.transform.position.z);

        }

            if (dolly != null)
            { dolly.m_AutoDolly.m_Enabled = false; }
        
       
        other.transform.position = teleportPos;
 
        // dolly.m_AutoDolly.m_Enabled  = true;
    
}
}
