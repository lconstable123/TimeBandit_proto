using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wallexplode : MonoBehaviour
{
    [SerializeField] Transform Solidwall;
    [SerializeField] Transform Holewall;
    [SerializeField] GameObject Teleporter;
    bool isTriggered =false;
    [SerializeField] int wallId;
    [SerializeField] switchbox sb;
    [SerializeField] AudioClip smashsound;
void OnTriggerEnter(Collider other){
 if (other.CompareTag("Player") && !isTriggered){
  Holewall.gameObject.SetActive(true);
  if (Teleporter != null){
    Solidwall.gameObject.SetActive(false);
  AudioSource ac = GetComponent<AudioSource>();
  ac.PlayOneShot(smashsound,1);
  }

Teleporter.SetActive(false);
isTriggered = true;
setSb(wallId);


    
}

}

void setSb(int id){
  
  switch(id){
    case 0:
      sb.isLeftWall = true;
      break;
    case 1:
      sb.isRightWall = true;
      break;
    case 2:
      sb.isForwardWall = true;
      break;
    default:
      break;
  }
}

}
    

