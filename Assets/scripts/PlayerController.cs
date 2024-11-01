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
    [SerializeField] float groundDrag;
    public LayerMask TerrainLayer;
    Rigidbody rb;
    SpriteRenderer sr;
    float x,y;
    Vector3 moveDir;
    bool grounded;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        sr = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        x = Input.GetAxis("Horizontal");
        y = Input.GetAxis("Vertical");
        moveDir = new Vector3(x,0,y);
        grounded = Physics.Raycast(transform.position,Vector3.down,groundDist*0.5f+0.2f,TerrainLayer);

        if(grounded){
            rb.drag = groundDrag;
        } else {rb.drag = 0;};


    }
    void FixedUpdate(){
        ProcessForce();
    }


    void ProcessVelocity(){
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");
        Vector3 moveDir = new Vector3(x,0,y);
        //Debug.Log(moveDir);
        rb.velocity = moveDir*playerSpeed;
        
        if (x != 0 && x < 0){
            sr.flipX=true;
        }
        else if (x != 0 && x > 0){
            sr.flipX=false;

        }
    }
     void ProcessForce(){
       
        //Debug.Log(moveDir);
        rb.AddForce(playerSpeed * Time.fixedDeltaTime * moveDir,ForceMode.Force);
        
        if (x != 0 && x < 0){
            sr.flipX=true;
        }
        else if (x != 0 && x > 0){
            sr.flipX=false;

        }
    }
}
