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
        gs.debug.text="found "+doors.Length+ " doors";
        Debug.Log("found "+doors.Length+ " doors");
        foreach (Doorway door in doors){

            Debug.Log("checking door "+ door.doorId);
            if (door.doorId == enteredDoor){
                gs.debug.text="is valid";
                door.doorOpen=false;
                spawnLoc = door.ProcessSpawn();
                gs.debug.text="spawn loc is" + spawnLoc;
                
                
                Debug.Log(door.doorId+ " is valid door at loc " + spawnLoc);
                break;
            }
        }
        //transform.position = spawnLoc;
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.position = spawnLoc;
       // Debug.Log("moving on");
    }

    void Update(){
        gs.currentLoc.text=transform.position.ToString();
    }


}
