using System.Collections;
using System.Collections.Generic;
using Cinemachine.Utility;
using Unity.VisualScripting;
using UnityEngine;

public class SpawnController : MonoBehaviour
{
    //public Vector3 spawnLoc;
    GameSession gs;

    void Start()
    {
 gs = GameSession.Persistent;
       
        Vector3 spawnLoc = new Vector3();
        Quaternion spawnRot = new Quaternion();
        Vector3 spawnVel = new Vector3();

        int enteredDoor = gs.GetDoorEntered();
        
        Doorway[] doors = FindObjectsOfType<Doorway>();
        foreach (Doorway door in doors){
            if (door.doorId == enteredDoor){
                door.doorOpen=false;
                spawnLoc = door.ProcessSpawn();
                spawnVel = door.transform.forward*door.SpawnVel;
                spawnRot = door.transform.rotation;
                break;
            }
        }

        Rigidbody rb = GetComponent<Rigidbody>();
        rb.position = spawnLoc;
        rb.rotation = spawnRot.Normalized();
        rb.AddForce(spawnVel);
    }
    void Awake(){
               

    }



}
