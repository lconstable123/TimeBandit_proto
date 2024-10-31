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
    GameSession gs;
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
