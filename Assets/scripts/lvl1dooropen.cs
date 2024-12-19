using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lvl1dooropen : MonoBehaviour
{
    Animator an;
    AudioSource ac;
    [SerializeField] AudioClip openSound;
    bool firstTime = true;
    // Start is called before the first frame update
    void Start()
    {
       ac = GetComponent<AudioSource>();
       an = GetComponent<Animator>(); 
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter(){
        an.SetBool("doorOpen", true);
        if (ac != null && firstTime){
            ac.PlayOneShot(openSound);
            firstTime = false;
            }

    }
     void OnTriggerExit(){
       // an.SetBool("doorOpen", false);

    }
}
