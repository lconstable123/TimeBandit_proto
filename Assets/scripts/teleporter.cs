using System.Collections;
using System.Collections.Generic;

using Unity.VisualScripting;
using UnityEngine;
using Cinemachine;
using UnityEditor.Rendering;
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
                        Debug.Log("cam found");
                        dolly = virtualCamera.GetComponent<CinemachineVirtualCamera>().GetCinemachineComponent<CinemachineTrackedDolly>();
                    }
                    if (dolly != null){ 
                        Debug.Log("dolly found found");
                        dolly.m_AutoDolly.m_Enabled = false; }
                    other.transform.position = whereTo.transform.position;
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