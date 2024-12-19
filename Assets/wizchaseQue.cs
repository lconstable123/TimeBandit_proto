using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wizchaseQue : MonoBehaviour
{
    [SerializeField] wiz[] wiz;
    // Start is called before the first frame update
  void OnTriggerEnter(Collider other){


        if (other.gameObject.CompareTag("Player")){
            foreach (wiz wiz in wiz){
                wiz.enablechase=true;
            }

        }}

}
