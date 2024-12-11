using System.Collections;
using System.Collections.Generic;
using UnityEditor.Callbacks;
using UnityEngine;
using UnityEngine.InputSystem.XR;

public class ladder : MonoBehaviour
{
    [SerializeField] TB_animator an;
    [SerializeField] PlayerController pc;
    bool climbing = false;
    
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       Vector3 ladderlock = new Vector3(transform.position.x, 0,transform.position.z);
       Vector3 direction = pc.gameObject.transform.position - ladderlock;
       pc.GetComponent<Rigidbody>().AddForce(direction);
        
    }
      void OnTriggerEnter(Collider other){
        if (other.CompareTag("Player")){
            pc.EndClimbing();
            climbing=true;

           
            

}

      }}
