using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sleepUnlocker : MonoBehaviour
{
    [SerializeField] PlayCharacterAnimation pa;
    // Start is called before the first frame update
void OnTriggerEnter(Collider other){
     if (other.gameObject.CompareTag("Player")){
        pa.locked=false;
     }
}
}
