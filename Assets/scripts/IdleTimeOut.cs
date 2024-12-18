using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class IdleTimeOut : MonoBehaviour
{
    [SerializeField] float timeBeforeLook = 4f;
    [SerializeField] float timeBeforeSleep = 8f;
    [SerializeField] float timeBeforeReset = 15f;
    PlayerController pc;
    public float timeSinceInput;
    public bool timing=true;
    bool leaveOnce = true;
    GameSession gs;

    // Start is called before the first frame update
    void Start()
    {
        pc = GetComponent<PlayerController>();
        gs = GameSession.Persistent;
    }

    // Update is called once per frame
    void Update()
    {
        if (timing){
            timeSinceInput+=Time.deltaTime;
           
            if (timeSinceInput>=timeBeforeReset){
                HardTimeOut();}
            else if (timeSinceInput>=timeBeforeSleep){
                SoftTimeOut();}
            else if (timeSinceInput>=timeBeforeLook){
                LookTimeOut();}
        
        }
    }

    void SoftTimeOut(){
        //timeSinceInput=0f;
        //Debug.Log("timed out");
        pc.sleep(true,transform);
    }

    void HardTimeOut(){
        if (leaveOnce){
            leaveOnce = false;
            gs.ResetGameSession();
        }
        
    }
    void LookTimeOut(){
        
        pc.look();
    }


    public void ResetTimer(){
        timeSinceInput=0f;
    }




}
