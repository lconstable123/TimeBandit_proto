using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wreathpickup : MonoBehaviour
{
    [SerializeField] GameObject potwreath;
    [SerializeField] GameObject oldWall;
    [SerializeField] GameObject playerWreath;
    [SerializeField] GameObject newWall;
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
        if(potwreath != null){
            potwreath.SetActive(false);
        }
        if(oldWall != null){
            oldWall.SetActive(false);
        }


        if(playerWreath != null){
            playerWreath.SetActive(true);
        }
        if (newWall != null){
            newWall.SetActive(true);
        }
     }
}
}
