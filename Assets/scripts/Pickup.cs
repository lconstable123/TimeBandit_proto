using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Pickup : MonoBehaviour
{
    [SerializeField] GameObject rotator;
    
    [SerializeField] float rotSpeed;

    [SerializeField] string whereTo;
    [SerializeField] Color32 colour;
    [SerializeField] string objectId;
    GameSession gs;
    //Material mat;
    //Renderer renderer = GetComponent<Renderer>();
    // Start is called before the first frame update
    void Start()
    {
        gs = FindObjectOfType<GameSession>();
        objectId = GetComponent<UniqueId>().GetId();

       if (gs.IsItemPickedUp(objectId) == true){
            Destroy(gameObject);
        }
    }

    void Update()
    {
        ProcessRotation();
    }

    private void ProcessRotation()
    {
        float rot = Time.deltaTime * rotSpeed;
        rotator.transform.Rotate(new Vector3(0, rot, 0));
    }

    void OnTriggerEnter(Collider other){
    
            Debug.Log("Picked up object");
            
            gs.AddPickedUpItem(objectId);
            Destroy(gameObject);
        
       
    }
   
}
