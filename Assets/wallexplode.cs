using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wallexplode : MonoBehaviour
{
    [SerializeField] Transform Solidwall;
    [SerializeField] Transform Holewall;
    bool isTriggered =false;
void OnTriggerEnter(Collider other){
 if (other.CompareTag("Player") && !isTriggered){
  Holewall.gameObject.SetActive(true);
Solidwall.gameObject.SetActive(false);
isTriggered = true;
    
}

}
}
    

