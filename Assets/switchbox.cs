using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class switchbox : MonoBehaviour
{
    [SerializeField] GameObject go1;
    [SerializeField] GameObject go2;
    [SerializeField] bool In = true;
    [SerializeField] bool firstTime=true;
    public bool isLeftWall = false;
    public bool isRightWall = false;

    public bool isForwardWall = false;
    [SerializeField] GameObject leftWall;
    [SerializeField] GameObject rightWall;
    [SerializeField] GameObject forwardWall;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter(Collider other){
         if (other.CompareTag("Player") && !firstTime){
           
            go1.SetActive(true);
            go2.SetActive(false);
            
         }
    }


    void OnTriggerExit(Collider other){
         if (other.CompareTag("Player")){
                go2.SetActive(true);
                go1.SetActive(false);
                firstTime=false;
                 if(isLeftWall){leftWall.SetActive(false);}
            if(isRightWall){rightWall.SetActive(false);}
            if(isForwardWall){forwardWall.SetActive(false);}

         }
    }
}
