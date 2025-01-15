using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exploder : MonoBehaviour
{
    [SerializeField] Transform explodesource;
    Rigidbody rb;
    [SerializeField] float amount = 100f;
    [SerializeField] float radius=100f;
    public GameObject[] childrenArray;
    // Start is called before the first frame update
    void Start()
    {
        explodebits();
        

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void explodebits(){

        childrenArray = new GameObject[transform.childCount];
        for(int i = 0; i < transform.childCount; i++){
            childrenArray[i] = transform.GetChild(i).gameObject;
        }

        foreach (GameObject child in childrenArray){
            rb = child.GetComponent<Rigidbody>();
            rb.AddExplosionForce(amount,explodesource.transform.position,radius);
        }
    }
}
