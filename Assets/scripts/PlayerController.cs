//using System;
//using System.Collections;
//using System.Collections.Generic;
//using UnityEditor.Callbacks;

using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
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
    [SerializeField] bool camerBasedMove = false;

    [Header("navigation")]
    public float XRayoffset= 0.2f;
    public float YRayoffset = 0.4f;
    public float probeLength = 0.3f;
    public float groundprobelength = 1f;
    public bool groundAhead;
    public bool groundBelow;
    Camera cam;

    [Header("Slope handling")]
    public float maxSlopeAngle;
    private RaycastHit slopeHit;
    private RaycastHit groundslopeHit;
    public bool OnSlopDebug;
    public float SlopeAngleDebug;
    Vector3 force = new();

    public LayerMask TerrainLayer;
    Rigidbody rb;
    SpriteRenderer sr;
    float x,y;
    Vector3 moveDir;

    private bool isJumping;
    public bool isClimbing;
    public bool touchingRamp;

    public enum MovingMode{
        ground = 1,
        ramp=2,
        falling=3,
        climbing=4
    }
    public MovingMode movingMode;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        sr = GetComponent<SpriteRenderer>();
        cam = Camera.main;
    }

    void Update()
    {
        GroundProbe(true);
       // ApplyForce(force);

       // if(isClimbing){ClimbLadder2();}
      
    }
    void FixedUpdate(){
        ProcessForce();
        ProcessSpriteFlip();
    }

    void ClimbLadder2()
    {
        rb.useGravity = false;
        //Vector3 climbVelocity = new Vector3(rb.velocity.x,y*1,0);
        //rb.velocity = climbVelocity;
        //bool playerHasVerticalSpeed = Math.Abs(myRigidbody.velocity.y) > Mathf.Epsilon;
        //animator.SetBool("isClimbing",true);
        //if(!playerHasVerticalSpeed){animator.speed = 0;} else {animator.speed = 1;}
    }

     void EndClimbing(){
       isClimbing = false;
        //myRigidbody.gravityScale = gravity;
       // animator.speed = 1;
       // animator.SetBool("isClimbing",false);
    }

    void OnTriggerEnter(Collider other){
        //if (other.gameObject.layer == LayerMask.NameToLayer("Ladder")){
         //   isClimbing = true;
            //movingMode = MovingMode.climbing;
          //  Debug.Log("ladded");
        //};
       
       
    }

    void OnTriggerExit(Collider other){
   // if (other.gameObject.layer == LayerMask.NameToLayer("Ladder")){
     //   if(isClimbing){EndClimbing();};
    //    };
    }
    void OnCollisionEnter(Collision other ){
        if (other.gameObject.layer == LayerMask.NameToLayer("Ladder")){
            isClimbing = true;
            //movingMode = MovingMode.climbing;
            Debug.Log("ladded");
        };
    }
      void OnCollisionExit(Collision other ){
         if (other.gameObject.layer == LayerMask.NameToLayer("Ladder")){
        if(isClimbing){
            Debug.Log("leaving ladder");
            EndClimbing();};
        };
        }
            // if (other.gameObject.layer == LayerMask.NameToLayer("Ramp")){
            // //Debug.Log("ramp");
            // movingMode = MovingMode.ramp;
            // touchingRamp = true;
            // };
            // if (other.gameObject.layer == LayerMask.NameToLayer("Ground")){
            //Debug.Log("ramp");
            //movingMode = MovingMode.ground;
            //touchingRamp = true;
           // };
    
    

    void GroundProbe( bool debug)
        {
        
           float xOffset = (Mathf.Abs(x)*XRayoffset) ;
           float yOffset = (Mathf.Abs(y)*YRayoffset);
           Vector3 off = new (moveDir.x*xOffset*XRayoffset,0,moveDir.z*yOffset*YRayoffset);
           Vector3 heightOffset = new(0,playerHeight*0.5f,0);
           Vector3 startray = transform.position + off;
            groundAhead = Physics.Raycast(startray, Vector3.down, out slopeHit,  probeLength, TerrainLayer);
            groundBelow = Physics.Raycast(transform.position, Vector3.down, out groundslopeHit,  groundprobelength, TerrainLayer);

            CalulateMoveMode();
            

            if (debug){Debug.DrawRay(startray, Vector3.down*probeLength);}
           // if (debug){Debug.DrawRay(transform.position,Vector3.down*groundprobelength);}
        }

    void OnMove(InputValue move){
        
        x = move.Get<Vector2>().x;
        y = move.Get<Vector2>().y;
        
        if (!camerBasedMove){
            moveDir = new Vector3(x, 0, y);} 
        else {
            Vector3 pureMov = new Vector3(x, 0, y); 
            Vector3 cameraForward = cam.transform.forward;
            Vector3 cameraRight = cam.transform.right;
            cameraForward.y = 0;
            cameraRight.y = 0;
            cameraForward.Normalize();
            cameraRight.Normalize();
            moveDir = cameraForward*y+cameraRight*x;

        }

    }

    void OnJump(){
       // Debug.Log("jump");
       if (movingMode != MovingMode.falling){
       rb.velocity += new Vector3(0,jumpPower,0);
        //rb.useGravity= true;
        //rb.drag = 0;
        movingMode = MovingMode.falling;
       }
    }
    private void ProcessJump()
    {
    //     //rb.AddForce(Vector3.up* jumpPower, ForceMode.Force);
    //     movingMode = MovingMode.falling;
    //    // rb.velocity += new Vector3(x*playerSpeed,jumpPower,0);
    //     rb.velocity += new Vector3(0,jumpPower,0);
    //     rb.useGravity= true;
    //     rb.drag = 0;

    }

    

    void CalulateMoveMode(){
        float angle = Vector3.Angle(Vector3.up,slopeHit.normal);
        SlopeAngleDebug = angle;

        if (isClimbing){
            // if on ladder
            movingMode = MovingMode.climbing;
        } else if (!groundBelow && !groundAhead){
            movingMode = MovingMode.falling;
        } else {
            if (angle < maxSlopeAngle && angle != 0 ){
                movingMode = MovingMode.ramp;
            } else {movingMode = MovingMode.ground;}; 
    }
    }
    private Vector3 GetSlopeMoveDirection(){
        return Vector3.ProjectOnPlane(moveDir,slopeHit.normal).normalized;
    }

    private Vector3 GetLadderMoveDirection(){
        if(!groundBelow || y > Mathf.Epsilon){return new Vector3(x,y,0);}
        if(groundBelow && y > Mathf.Epsilon){return new Vector3(x,y,0);}
        else { return moveDir;}
    }
     void ProcessForce(){
       
       Quaternion playerrot = transform.rotation;
   
       switch (movingMode){
        
        case MovingMode.climbing:
            rb.useGravity = false;
            force = playerSpeed *Time.fixedDeltaTime * GetLadderMoveDirection();
            break;

        case MovingMode.ground:

        
            rb.drag = groundDrag;
            rb.useGravity = false;
            force = playerSpeed * Time.fixedDeltaTime * moveDir;
            //Debug.Log(force);
            break;

        case MovingMode.ramp:
            rb.drag = groundDrag;
            rb.useGravity = false;
            force = playerSpeed * Time.fixedDeltaTime * GetSlopeMoveDirection(); 
            break;

        case MovingMode.falling:
            rb.useGravity= true;
            
            rb.drag = 0f;  
            
            force = playerSpeed*.01f * Time.fixedDeltaTime * moveDir;
            break;

        default: 
            break;
       }
       ApplyForce(force);
    }

    void ApplyForce(Vector3 force){
            rb.AddForce(force, ForceMode.Force);
            if(rb.velocity.y > 0 || rb.velocity.z > 0){
             rb.AddForce(Vector3.down * 100f, ForceMode.Force);
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

//probe ahead
//probe under
//if probhead is false
    //check probe under
    //if both are false player is int air
//if flat is under, 

