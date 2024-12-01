using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using Unity.VisualScripting;
public class cameraLenQue : MonoBehaviour
{
    [SerializeField] CinemachineVirtualCamera vc;
    [SerializeField] float FOV;
    float initialFOV;

    [SerializeField] float Yrot;
    [SerializeField] string Tag;
    [SerializeField] float transitionSpeed = 2f;
    bool triggered = false;
    float transition = 0;
    public float currentval;
    CinemachineTrackedDolly dolly;
    
    // Start is called before the first frame update
   void Update(){
    if(triggered){
        transition += Time.deltaTime*transitionSpeed;
        currentval =  Mathf.SmoothStep(initialFOV, FOV, transition);
        if (Mathf.Approximately(currentval,FOV)){
            triggered = false;
            transition = 0;
            
            }
    
        //vc.m_Lens.FieldOfView = currentval;
        dolly.m_PathOffset.x = currentval;

    }
   }
    void OnTriggerEnter(Collider other){
        if (other.gameObject.CompareTag(Tag)){
            if (vc != null){    
                dolly = vc.GetComponent<CinemachineVirtualCamera>().GetCinemachineComponent<CinemachineTrackedDolly>();
                if (dolly != null){
                    //initialFOV = vc.m_Lens.FieldOfView;
                    initialFOV = dolly.m_PathOffset.x;
                    triggered = true;   
                    

                }
                
                
            }
        }
    }

    
}
