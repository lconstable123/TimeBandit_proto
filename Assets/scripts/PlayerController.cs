//using System;
//using System.Collections;
//using System.Collections.Generic;
//using UnityEditor.Callbacks;
using UnityEngine;
//using UnityEngine.XR;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float playerSpeed;
    [SerializeField] float groundDist;
    public LayerMask TerrainLayer;
    Rigidbody rb;
    SpriteRenderer sr;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        sr = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");
        Vector3 moveDir = new Vector3(x,0,y);
        //Debug.Log(moveDir);
        rb.velocity = moveDir*playerSpeed*Time.deltaTime;
        
        if (x != 0 && x < 0){
            sr.flipX=true;
        }
        else if (x != 0 && x > 0){
            sr.flipX=false;

        }

    }
}
