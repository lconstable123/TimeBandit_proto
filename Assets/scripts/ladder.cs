using System.Collections;
using System.Collections.Generic;

using UnityEngine;


public class ladder : MonoBehaviour
{
   // [SerializeField] TB_animator an;
    [SerializeField] PlayerController pc;
    [SerializeField] Animator an;
   // bool climbing = false;
    public bool ladderOn;
    public bool ladderDirection;
    
    
    // Start is called before the first frame update
    void Start()
    {
        //an= GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
    //    Vector3 ladderlock = new Vector3(transform.position.x, 0,transform.position.z);
    //    Vector3 direction = pc.gameObject.transform.position - ladderlock;
    //    pc.GetComponent<Rigidbody>().AddForce(direction);
        
    }
      void OnTriggerEnter(Collider other){
        if (other.CompareTag("Player")){
            //enable climbing
    if(ladderDirection){
        travelUp();
    } else {travelDown();};

            // pc.EndClimbing();
            // climbing=true;

           
            

}}
    public void travelDown()
    {
        an.SetTrigger("SetUp");
        an.SetTrigger("TravelDown");
    }
    public void travelUp(){
        an.SetTrigger("SetDown");
        an.SetTrigger("TravelUp");
    }
      }