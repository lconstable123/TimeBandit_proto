//using System;
//using System.Collections;
//using System.Collections.Generic;
//using UnityEditor.Callbacks;

using System;

using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
//using UnityEngine.XR;

public class PlayerController : MonoBehaviour
{
    [Header("locomotion")]
    [SerializeField] float playerSpeed = 400000.0f;
    [SerializeField] float groundDist = 1.5f;
    [SerializeField] float groundDrag;
    [SerializeField] float playerHeight = 2f;
    [SerializeField] float jumpPower = 4000000.0f;

    [Header("Slope handling")]
    public float maxSlopeAngle;
    private RaycastHit slopeHit;
    public bool OnSlopDebug;
    public float SlopeAngleDebug;


    public LayerMask TerrainLayer;
    Rigidbody rb;
    SpriteRenderer sr;
    float x,y;
    Vector3 moveDir;
    public bool grounded;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        sr = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        x = Input.GetAxis("Horizontal");
        y = Input.GetAxis("Vertical");
        moveDir = new Vector3(x, 0, y);
        
        if (Input.GetKeyDown(KeyCode.Space)){
        ProcessJump();
        }
        GroundProbe(true);
        //rb.useGravity = !OnSlope();
        void GroundProbe( bool debug)
        {
            Vector3 slopeChecker = new();
            if (y > 0.3f)
            {
                slopeChecker = Vector3.down + (Vector3.forward * .3f);
                grounded = Physics.Raycast(transform.position, slopeChecker, out slopeHit, groundDist * 0.5f + 0.2f, TerrainLayer);

            }
            else
            {
                slopeChecker = Vector3.down;
                grounded = Physics.Raycast(transform.position, Vector3.down, out slopeHit, groundDist * 0.5f + 0.2f, TerrainLayer);
            }
            if (debug){Debug.DrawRay(transform.position, slopeChecker);}
        }
    }

    private void ProcessJump()
    {
        rb.AddForce(Vector3.up* jumpPower, ForceMode.Force);
    }

    void FixedUpdate(){
        ProcessForce();
        ProcessSpriteFlip();
    }

    private bool OnSlope(){
        if(grounded){
            float angle = Vector3.Angle(Vector3.up,slopeHit.normal);
            OnSlopDebug = true;
            SlopeAngleDebug = angle;
            return angle < maxSlopeAngle && angle != 0;
        }
        OnSlopDebug = false;
        
        return false;
    }

    private Vector3 getSlopeMoveDirection(){
        return Vector3.ProjectOnPlane(moveDir,slopeHit.normal).normalized;
    }

     void ProcessForce(){
       Vector3 force = new();

        if(grounded){
            //on ground
            rb.drag = groundDrag;
            if (OnSlope()){
                rb.useGravity = false;
                force = playerSpeed * Time.fixedDeltaTime * getSlopeMoveDirection(); 
            } else {
                rb.useGravity = true;
                force = playerSpeed * Time.fixedDeltaTime * moveDir;
            }
        } else {
            //in air
                rb.useGravity= true;
                rb.drag = 0;
                force = playerSpeed*.1f * Time.fixedDeltaTime * moveDir;
        }

        rb.AddForce(force, ForceMode.Force);

        if(rb.velocity.y > 0){
            rb.AddForce(Vector3.down * 80f, ForceMode.Force);
        }
    }
    void ProcessSpriteFlip(){
        if (x != 0 && x < 0){
            sr.flipX=true;
        }
        else if (x != 0 && x > 0){
            sr.flipX=false;

        }
    }
}
