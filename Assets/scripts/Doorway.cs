using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;

public class Doorway : MonoBehaviour
{
    [SerializeField] GameObject rotator;
    
    Animator animator;
    
    [SerializeField] float rotSpeed;
    [SerializeField] bool doorOpen;
    [SerializeField] string whereTo;
    [SerializeField] Color32 colour;
    [SerializeField] float transitionDuration;
    [SerializeField] bool isSpawn;
    [SerializeField] float SpwanHeight;
    [SerializeField] float SpawnDist;
    GameSession gs;
    GameObject player;
    //Material mat;
    //Renderer renderer = GetComponent<Renderer>();
    // Start is called before the first frame update
   
    void Start()
    {
        //MeshRenderer renderer = transform.GetChild(0).GetChild(0).GetComponent<MeshRenderer>();
        //renderer.material.color = color;
        //ppvol = FindObjectOfType<Volume>();
        //animator = ppvol.GetComponent<Animator>();
        gs = FindObjectOfType<GameSession>();

        if (isSpawn){

            player = FindObjectOfType<PlayerController>().gameObject;
            if (player == null){Debug.Log("no player found");} else {
               // Debug.Log("player found");
            }
            Vector3 spawnPos = transform.position-transform.forward*SpawnDist;
            spawnPos += new Vector3(0f,SpwanHeight,0f);
            //Debug.Log("teleporting to "+transform.position);
            player.transform.position = spawnPos;
            
        }

       

    }

    // Update is called once per frame
    void Update()
    {
        if (rotator != null){
        ProcessRotation();
        }
    }

    private void ProcessRotation()
    {
        float rot = Time.deltaTime * rotSpeed;
        rotator.transform.Rotate(new Vector3(0, rot, 0));
    }

    void OnTriggerEnter(Collider other){
        if (other.CompareTag("Player")){
            if (doorOpen){
                //animator.SetTrigger("NextScene");
                gs = FindObjectOfType<GameSession>();
               gs.LeaveScene(whereTo);
               
                
            } 
        }
    }
 
   
    public void BruteLoadScene(){
         Debug.Log("entered doorway");
                SceneManager.LoadScene(whereTo);
    }
   
}
