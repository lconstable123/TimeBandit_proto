using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BoatField : MonoBehaviour
{
    [SerializeField] float intensity;
    [SerializeField] Rigidbody boat;
    bool trigger=false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (trigger){
            boat.AddForce(transform.forward*intensity, ForceMode.Force);  
        }
    }
    void OnTriggerEnter(Collider other){
         if (other.gameObject.CompareTag("boat")){
            trigger =true;
           // Debug.Log("in");
          
         }
    }
      void OnTriggerExit(Collider other){
         if (other.gameObject.CompareTag("boat")){
            trigger =false;
           // Debug.Log("in");
          
         }
    }
}
