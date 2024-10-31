using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Doorway : MonoBehaviour
{
    [SerializeField] GameObject rotator;
    
    [SerializeField] float rotSpeed;
    [SerializeField] bool doorOpen;
    [SerializeField] string whereTo;
    [SerializeField] Color32 colour;
    //Material mat;
    //Renderer renderer = GetComponent<Renderer>();
    // Start is called before the first frame update
    void Start()
    {
        //MeshRenderer renderer = transform.GetChild(0).GetChild(0).GetComponent<MeshRenderer>();
        //renderer.material.color = color;
        
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
                Debug.Log("entered doorway");
                //var currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
                SceneManager.LoadScene(whereTo);
            } 
        }
    }
   
}
