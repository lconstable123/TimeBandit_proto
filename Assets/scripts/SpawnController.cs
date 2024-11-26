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

        Doorway[] doors = FindObjectsOfType<Doorway>();
        foreach (Doorway door in doors){
            if (door.doorId == enteredDoor){
                door.doorOpen=false;
                spawnLoc = door.ProcessSpawn();
                break;
            }
        }
        //transform.position = spawnLoc;
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.position = spawnLoc;

    }

    void Update(){
    }


}
