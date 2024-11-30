using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggelVis : MonoBehaviour
{
    [SerializeField] GameObject toggle1;
    [SerializeField] GameObject toggle2;
    [SerializeField] string Tag = "";
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

        if (Tag != ""){

        if (other.gameObject.CompareTag(Tag)){
            if(toggle){
                toggle2.SetActive(true);
                toggle1.SetActive(false);
                
            } else {
                toggle2.SetActive(false);
                toggle1.SetActive(true);
                

            }

        }
        }
    }
}
