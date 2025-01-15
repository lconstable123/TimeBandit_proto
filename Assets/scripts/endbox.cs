using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class endbox : MonoBehaviour
{
    [SerializeField] GameObject EndCanvas;
    bool firstTime = true;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter(Collider other){
        if (other.CompareTag("Player") && firstTime){
            firstTime = false;
            Animator an = EndCanvas.GetComponent<Animator>();
            if (an == null){Debug.Log("animator not found");} else{
            an.SetTrigger("In");
            }
        }
    }

}
