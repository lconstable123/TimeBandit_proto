using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class doorUnlocker : MonoBehaviour
{
    [SerializeField] Doorway[] doorsToUnlock;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
        void OnTriggerEnter(Collider other){
        if (other.CompareTag("Player")){
            foreach (Doorway doorway in doorsToUnlock){
                doorway.doorOpen=true;
                
            }
            //doorToUnlock.doorOpen = true;
        }
    }
}
