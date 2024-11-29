using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class revealbobber : MonoBehaviour
{
    [SerializeField] Animator an;
    [SerializeField] bool onoff;
    // Start is called before the first frame update

    void OnTriggerEnter(Collider other){
         if (other.gameObject.CompareTag("boat")){
            if (onoff){
                an.SetBool("appear",true);
            } else {
               an.SetBool("appear",false); 
            }
            
    }
}

}
