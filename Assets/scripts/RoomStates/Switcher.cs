using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switcher : MonoBehaviour
{
    [SerializeField] GameObject toggle1;
    

    public bool toggled;
    // Start is called before the first frame update

    void OnTriggerEnter(Collider other){


        if (other.gameObject.CompareTag("Player")){

                toggle1.SetActive(toggled);
            }
        }

}

