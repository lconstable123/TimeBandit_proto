using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lvl1dooropen : MonoBehaviour
{
    Animator an;
    // Start is called before the first frame update
    void Start()
    {
       an = GetComponent<Animator>(); 
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter(){
        an.SetBool("doorOpen", true);

    }
     void OnTriggerExit(){
       // an.SetBool("doorOpen", false);

    }
}
