using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallRemover : MonoBehaviour
{
     [SerializeField] bool isOn = false;
     [SerializeField] GameObject toAnimate;
     Animator a;
    // Start is called before the first frame update
    void Start()
    {
        if (isOn){
            a = toAnimate.GetComponent<Animator>();

        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
       void OnTriggerEnter(Collider other){
        if (other.CompareTag("Player" ) && isOn){
           //Debug.Log("enter change");
           a.SetBool("isfade",true);
          
        }
    }

    void OnTriggerExit(Collider other){
        if (other.CompareTag("Player") && isOn ){
       //  Debug.Log("out change");
       a.SetBool("isfade",false);
     
        }
    }
}
