using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggelCompon : MonoBehaviour
{
    [SerializeField] Screen_wrapper toggle1;

    public bool toggle;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter(Collider other){


        if (other.gameObject.CompareTag("Player")){
                toggle1.isEnabled=false;
            }
        }
    void OnTriggerExit(Collider other){


        if (other.gameObject.CompareTag("Player")){
                toggle1.isEnabled=true;
            }
        }    
}

