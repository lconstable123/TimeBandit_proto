using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using Unity.VisualScripting;
public class AmbentColourQue : MonoBehaviour
{

    [SerializeField] Color32 newColour;
    Color32 currentColour;

    [SerializeField] string Tag;
    [SerializeField] float transitionSpeed = 2f;
    bool triggered = false;
    float transition = 0;
    public float currentval;

    
    // Start is called before the first frame update
   void Update(){
    if(triggered){
        transition += Time.deltaTime*transitionSpeed;
        
        RenderSettings.ambientLight = Color32.Lerp(currentColour, newColour, transition);

        if (transition >= 1){
            triggered = false;
            transition = 0;
            
            }


    }
   }
    void OnTriggerEnter(Collider other){
        if (other.gameObject.CompareTag(Tag)){
            
                currentColour =  RenderSettings.ambientLight;
                triggered = true; 
        }
    }

    
}
