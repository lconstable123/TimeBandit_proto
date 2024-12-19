using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class Screen_wrapper : MonoBehaviour
{
    private Camera mainCamera;
    CinemachineTrackedDolly dolly;
    [SerializeField] CinemachineVirtualCamera virtualCamera; 
    float buffer= 0.01f;
    public bool isEnabled = false;

    void Start()
    {
        // Get the main camera (rendering camera)
        mainCamera = Camera.main;
        StartCoroutine(startDelay() );
        // Optionally, you can get the Cinemachine Virtual Camera, if needed

    }

    void Update()
    {
        if(isEnabled){CheckAndTeleport();}
    }

    void CheckAndTeleport()
    {

        Vector3 screenpos = mainCamera.WorldToScreenPoint(transform.position);
        Vector3 relpos = new Vector3(screenpos.x/mainCamera.pixelWidth,screenpos.y/mainCamera.pixelHeight,screenpos.z);
        

       if (relpos.x < -0.1)
         {
            Vector3 rightPosinWorld = mainCamera.ScreenToWorldPoint(new Vector3(mainCamera.pixelWidth-buffer,screenpos.y,screenpos.z));
            Vector3 offset = new Vector3(rightPosinWorld.x,transform.position.y,transform.position.z);
            StopDolly();
            transform.position = rightPosinWorld;
            
        }
        if (relpos.x > 1.1)
         {
            Vector3 rightPosinWorld = mainCamera.ScreenToWorldPoint(new Vector3(buffer,screenpos.y,screenpos.z));
            Vector3 offset = new Vector3(rightPosinWorld.x,transform.position.y,transform.position.z);
            StopDolly();
            transform.position = rightPosinWorld;
            
       }

    void StopDolly(){
        if (virtualCamera != null){
                        dolly = virtualCamera.GetComponent<CinemachineVirtualCamera>().GetCinemachineComponent<CinemachineTrackedDolly>();
                    }
                    if (dolly != null){ 
                       // Debug.Log("dolly found found");
                        dolly.m_AutoDolly.m_Enabled = false; 
                        dolly.m_XDamping=0;
                        StartCoroutine(EnableDamping());
                        }



    }



      IEnumerator EnableDamping(){
            yield return new WaitForSeconds(.5f);
            // Debug.Log("damped");
            dolly.m_XDamping=1.5f;
        }
    }
    
    IEnumerator startDelay(){
        yield return new WaitForSeconds(.5f);
        isEnabled = true;
    }
}
