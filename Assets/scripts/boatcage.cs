using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class boatcage : MonoBehaviour
{
    [SerializeField] PlayerController player;
    [SerializeField] bool EnableCage;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter( Collider other){
        if (other.gameObject.CompareTag("boat")){
            if (EnableCage){
              
            player.BoatLock=true;
    } else {
            player.BoatLock=false;
    }
}

}}
