using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SpawnController : MonoBehaviour
{
    public Vector3 spawnLoc;
    GameSession gs;

    void Start()
    {
        gs = GameSession.Persistent;
       

        int enteredDoor = gs.GetDoorEntered();
        Vector3 spawnVel = new Vector3();
        Doorway[] doors = FindObjectsOfType<Doorway>();
        foreach (Doorway door in doors){
            if (door.doorId == enteredDoor){
                door.doorOpen=false;
                spawnLoc = door.ProcessSpawn();
                spawnVel = door.transform.forward*door.SpawnVel;
                break;
            }
        }
        //transform.position = spawnLoc;
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.position = spawnLoc;
        
        rb.AddForce(spawnVel);

    }

    void Update(){
    }


}
