using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TB_animator : MonoBehaviour
{
    [SerializeField] Animator animator;
 
    Rigidbody rb;
    public Vector3 vel;
    public float speed;
    public bool climbing=false;
    
    [SerializeField] float speeToIdle= 2f;
    [SerializeField] float speedToRun= 4f;
    public enum AnimPlaying{
        idle=1,
        walking=2,
        running=3,
        sleeping=4,
        looking=5,
        rowing=6,
        climbingdown=7
    }
    [SerializeField] AnimPlaying a;
   

    
    // Start is called before the first frame update
    void Start()
    {
        animator= GetComponentInChildren<Animator>();
        if(animator==null){
            Debug.Log("animator not found");
        }
        rb = GetComponent<Rigidbody>();
 


    }

    // Update is called once per frame
    void Update()
    { if(!climbing){
        vel = rb.velocity;
        speed = Vector3.Magnitude(vel);

        if(speed > speedToRun){
            SwitchAnim(AnimPlaying.running);
            
        } else 
        if (speed > speeToIdle){
            SwitchAnim(AnimPlaying.walking);
        } else {
            SwitchAnim(AnimPlaying.idle);
        }
    }  
        
    }
    public void Sleep(){
        SwitchAnim(AnimPlaying.sleeping);
    }
    public void WakeUp(){
       // Debug.Log("waking animation");
        animator.SetBool("isWake",true);
    }
    public void Look(){
        
        SwitchAnim(AnimPlaying.looking);
    }
    public void ClimbDown(){
        climbing=true;
        SwitchAnim(AnimPlaying.climbingdown);
    }



    void SwitchAnim(AnimPlaying anim){
        switch (anim){
            case AnimPlaying.idle:
                animator.SetBool("isIdle", true);
                animator.SetBool("isWalking", false);
                animator.SetBool("isRunning", false);
                // animator.SetBool("isLooking", false);
                //animator.SetBool("isWake",true);
                
                break;

            case AnimPlaying.walking:
                animator.SetBool("isIdle", false);
                animator.SetBool("isWalking", true);
                animator.SetBool("isRunning", false);
                animator.SetBool("isLooking", false);
                //animator.SetBool("isWake",true);
                break;

            case AnimPlaying.running:
                animator.SetBool("isIdle", false);
                animator.SetBool("isWalking", false);
                animator.SetBool("isRunning", true);
                animator.SetBool("isWake",true);
                animator.SetBool("isLooking", false);
                break;
            case AnimPlaying.sleeping:
                // an.SetTrigger("sleepNow");
                // Debug.Log("firstsleep");
                animator.SetBool("isWake",false);
                animator.SetBool("isWalking", false);
                animator.SetBool("isRunning", false);
                animator.SetBool("isIdle", false);
                animator.SetBool("isLooking", false);
                break;
            case AnimPlaying.looking:
                // an.SetTrigger("sleepNow");
                // Debug.Log("firstsleep");
                // animator.SetBool("isWake",true);
                // animator.SetBool("isWalking", false);
                // animator.SetBool("isRunning", false);
                // animator.SetBool("isIdle", false);
                Debug.Log("loook");
                animator.SetBool("isLooking", true);
                break;
             case AnimPlaying.climbingdown:
                // an.SetTrigger("sleepNow");
                // Debug.Log("firstsleep");
                // animator.SetBool("isWake",false);
                // animator.SetBool("isWalking", false);
                // animator.SetBool("isRunning", false);
                // animator.SetBool("isIdle", false);
                // animator.SetBool("isLooking", false);
                //animator.SetBool("isClimbingDown",true);
                break;
            case AnimPlaying.rowing:
                break;
            default:
                break;

        }
    }

}