//using System;
//using System.Collections;
//using System.Collections.Generic;
//using UnityEditor.Callbacks;

using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;
//using UnityEngine.XR;

public class PlayerController : MonoBehaviour
{
    [Header("locomotion")]
    [SerializeField] float playerSpeed = 400000.0f;
    public float rota;
   // [SerializeField] float groundDist = 1.5f;
    [SerializeField] float groundDrag;
    [SerializeField] float playerHeight = 2f;
    [SerializeField] float jumpPower = 4000000.0f;
    [SerializeField] bool camerBasedMove = false;
    [SerializeField] float rotationSpeed = 10f;
    public bool controlsSoftLocked = false;
    public bool controlsHardLocked = false;
   // [SerializeField] float slopeUp = 20f;

    [Header("navigation")]
    public float XRayoffset= 0.2f;

    public float probeLength = 0.3f;
    public float steppableLength = 0.3f;
    public float groundprobelength = 1f;
    public bool groundAhead;
    public bool groundBelow;
    public bool steppable;
    [SerializeField] bool BoatModeAllow = false;
    public bool ParentedToBoat = false;
    public bool nearBoat = false;
    public bool BoatLock = false;

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
    [SerializeField]  BoatCtrl boatRef;
    float x,y;
    Vector3 moveDir;
    Transform originalParent;
    TB_animator tbAn;

    private bool isJumping;
    public bool isClimbing;
    public bool touchingRamp;
    public float boatheight;

    public enum MovingMode{
        ground = 1,
        ramp=2,
        falling=3,
        climbing=4,
        stepping=5,
        riding=6,
        sleeping=7,
        idle=8

    }
    public MovingMode movingMode;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        tbAn = GetComponent<TB_animator>();
        //sr = GetComponent<SpriteRenderer>();
        cam = Camera.main;
        // if (BoatModeAllow){
        //     boatRef = FindObjectOfType<BoatCtrl>();
        //     if (boatRef== null){
        //         Debug.Log("no boat");
        //     }
        // }
    }

    void Update()
    {
        if (!controlsHardLocked){
            if (movingMode != MovingMode.sleeping || movingMode != MovingMode.idle ){
                GroundProbe(true);
            }
        }

        CheckQuit();
    }

    void FixedUpdate(){
        if (!controlsHardLocked){
            if(movingMode != MovingMode.sleeping){
                //Debug.Log("not sleeping according to update");
                if (ParentedToBoat){
                    boatRef.MoveBoat(moveDir);
                } else {
                    ProcessForce();
                    RotateCharacter();
                }
            }
        }
    }

    void OnTriggerEnter(Collider other){
        if (other.gameObject.CompareTag("boat")){
             if(BoatModeAllow && !ParentedToBoat ){
                parentToBoat();
        }
            nearBoat = true;
        }   
    }

    void OnTriggerExit(Collider other){
    if (other.gameObject.CompareTag("boat")){
            nearBoat = false;
        }
    }
    void OnCollisionEnter(Collision other ){
        if (other.gameObject.layer == LayerMask.NameToLayer("Ladder")){
            isClimbing = true;
            movingMode = MovingMode.climbing;
        };
    }
      void OnCollisionExit(Collision other ){
         if (other.gameObject.layer == LayerMask.NameToLayer("Ladder")){
        if(isClimbing){

            EndClimbing();};
        };
        }

    void GroundProbe( bool debug)
        {
        
           float xOffset = (Mathf.Abs(x)*XRayoffset) ;
           float yOffset = (Mathf.Abs(y)*XRayoffset);
           Vector3 off = new (moveDir.x*xOffset,0,moveDir.z*yOffset);
           Vector3 heightOffset = new(0,playerHeight*0.5f,0);
           Vector3 startray = transform.position + off;
            groundAhead = Physics.Raycast(startray, Vector3.down, out slopeHit,  probeLength, TerrainLayer);
            groundBelow = Physics.Raycast(transform.position, Vector3.down, out groundslopeHit,  groundprobelength, TerrainLayer);
            steppable = Physics.Raycast(startray, Vector3.down, out slopeHit,  probeLength-steppableLength, TerrainLayer);
            CalulateMoveMode();
        }
  
    void OnMove(InputValue move){
        if (!controlsHardLocked){
            if (movingMode == MovingMode.sleeping)
                {StartCoroutine(wakeUp());}
        if (!controlsSoftLocked)
            {  
            // if (movingMode == MovingMode.idle)
            // {//resumeGame();}
                ProcessMovement(move);
            }
        }
    }

 

    void OnJump(){
        if (!controlsHardLocked){
        if (movingMode == MovingMode.sleeping)
            {
                StartCoroutine(wakeUp());
            } else {

            if(ParentedToBoat && !BoatLock){unparentFromBoat();};
            if (movingMode != MovingMode.falling){
                rb.velocity += new Vector3(0,jumpPower,0);
                movingMode = MovingMode.falling;
            }
            }
        }
    }

    void OnFire(){
        //Debug.Log("action");
    }



    

    void CalulateMoveMode(){

        if (ParentedToBoat){ return; };
        if(controlsSoftLocked){return; };
        float angle = Vector3.Angle(Vector3.up,groundslopeHit.normal);
        SlopeAngleDebug = angle;

        if (isClimbing){
            // if on ladder
            movingMode = MovingMode.climbing;
            return;
        } else if (!groundBelow && !groundAhead){
            movingMode = MovingMode.falling;
            return;
        } else if ( angle != 0  && angle < maxSlopeAngle ){
            movingMode = MovingMode.ramp;
            return;
        } else if (groundAhead && steppable && !nearBoat){
            movingMode = MovingMode.stepping;
            return;
        }
            else {movingMode = MovingMode.ground;}; 
    
    }
 
     void ProcessForce(){
       
       Quaternion playerrot = transform.rotation;
   
       switch (movingMode){
        
        case MovingMode.climbing:
            rb.useGravity = false;
            force = playerSpeed *Time.fixedDeltaTime * GetLadderMoveDirection();
            ApplyForce(force);
            break;
        case MovingMode.ground:
            rb.drag = groundDrag;
            rb.useGravity = false;
            force = playerSpeed * Time.fixedDeltaTime * moveDir + (Vector3.down * 100f);
            rb.AddForce(Vector3.down * 20f, ForceMode.Force);
            ApplyForce(force);
            break;
          case MovingMode.stepping:
            rb.drag = groundDrag;
            rb.useGravity = false;
            moveDir+= new Vector3(0f,.1f,0f);
            force = playerSpeed * Time.fixedDeltaTime * moveDir;
            ApplyForce(force);
            //Debug.Log(force);
            break;
        case MovingMode.ramp:
            rb.drag = groundDrag;
            rb.useGravity = false;
            Vector3 slopeMove = GetSlopeMoveDirection(); 
            //Debug.DrawLine(transform.position,slopeMove);
            //Debug.DrawRay(transform.position,slopeMove);
            // if (slopeMove.y > 0){
            //     slopeMove += (Vector3.up*slopeUp);
            // }
            //rb.AddForce(Vector3.up * 100f, ForceMode.Force);
           // rb.AddForce(Vector3.down * 100f, ForceMode.Force);
            force = playerSpeed * Time.fixedDeltaTime * slopeMove;
            ApplyForce(force); 
            break;
        case MovingMode.falling:
            rb.useGravity= true;
            rb.drag = 0f;  
            force = playerSpeed*.01f * Time.fixedDeltaTime * moveDir;
            ApplyForce(force);
            break;
        case MovingMode.riding:
            break;
        case MovingMode.sleeping:
            Debug.Log("sleeddp");
            break;
        case MovingMode.idle:
            break;
        default: 
            ApplyForce(force);
            break;
       }
       
    }

    void ApplyForce(Vector3 force){
            rb.AddForce(force, ForceMode.Force);
            // if(rb.velocity.y > 0){
            //  rb.AddForce(Vector3.down * 100f, ForceMode.Force);
            // }
        }
   private void ProcessMovement(InputValue move)
    {
        x = move.Get<Vector2>().x;
        y = move.Get<Vector2>().y;

        if (!camerBasedMove)
        {
            moveDir = new Vector3(x, 0, y);
        }
        else
        {
            Vector3 pureMov = new Vector3(x, 0, y);
            Vector3 cameraForward = cam.transform.forward;
            Vector3 cameraRight = cam.transform.right;
            cameraForward.y = 0;
            cameraRight.y = 0;
            cameraForward.Normalize();
            cameraRight.Normalize();
            moveDir = cameraForward * y + cameraRight * x;
        }
    }
    public void RotateCharacter(){
       // Vector3 direction = new Vector3(x, 0f, y);
        Vector3 direction = new Vector3(moveDir.x, moveDir.y, moveDir.z);
         if (direction.magnitude >= 0.1f)
        {
            // Calculate the angle to rotate
            float targetAngle = Mathf.Atan2(-direction.x, -direction.z) * Mathf.Rad2Deg;
            
            // Smoothly rotate the character to face the new direction
            Quaternion targetRotation = Quaternion.Euler(0f, targetAngle, 0f);
            if(ParentedToBoat){
               // boatRef.RotateBoat(targetRotation);
            } else {
            rb.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.fixedDeltaTime * rotationSpeed);
            }
        }
    }
IEnumerator BoatWait(){
        yield return new WaitForSeconds(3);
        BoatModeAllow =true;
    }


void parentToBoat(){
        movingMode = MovingMode.riding;
        Debug.Log("parenting to boat");
        ParentedToBoat=true;
        rb.isKinematic = true; 
        originalParent = transform.parent;
        Transform boatParent = boatRef.parent;
        transform.SetParent(boatParent);
        boatRef.UdockBoat();
        transform.localPosition = new Vector3(10.18f,7f,-14.9f);
        transform.localRotation = Quaternion.Euler(0, -88.7f, 0);
        
    }
void unparentFromBoat(){
        BoatModeAllow = false;
        ParentedToBoat=false;
        rb.isKinematic=false;
        Debug.Log("removefromboat");
        transform.SetParent(originalParent);
        StartCoroutine(BoatWait());
        boatRef.DockBoat();    
    }
   private Vector3 GetSlopeMoveDirection(){
        return Vector3.ProjectOnPlane(moveDir,groundslopeHit.normal).normalized;
    }

    private Vector3 GetLadderMoveDirection(){
        if(!groundBelow || y > Mathf.Epsilon){return new Vector3(x,y,0);}
        if(groundBelow && y > Mathf.Epsilon){return new Vector3(x,y,0);}
        else { return moveDir;}
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
    void CheckQuit()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else

            Application.Quit();
#endif
        }
    }

public void sleep(bool hardLock,Transform pos){
    Debug.Log("sleep");
    movingMode = MovingMode.sleeping;
    rb = GetComponent<Rigidbody>();
    rb.position = pos.position;
    rb.rotation = pos.rotation;
    if (hardLock){
        StartCoroutine(lockControlsForTime(6f));
    }
    
    controlsSoftLocked = true;

}
IEnumerator wakeUp(){
    tbAn.WakeUp();
    //controlsLocked=true;
    //Debug.Log("waking up");
    yield return new WaitForSeconds(4);
    movingMode = MovingMode.ground;
    controlsSoftLocked = false;

  
}
IEnumerator lockControlsForTime(float time){
    controlsHardLocked=true;
    yield return new WaitForSeconds(time);
    //Debug.Log("unlocking controls");
    controlsHardLocked=false;
}


}
