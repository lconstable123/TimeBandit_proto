using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;


public class Pickup : MonoBehaviour
{
    [SerializeField] GameObject rotator;
    
    [SerializeField] float rotSpeed;

    [SerializeField] string whereTo;
    [SerializeField] Color32 colour;
    [SerializeField] string objectId;
    GameSession gs;
    //public UIWindow WindowHeader;
    
    public DialogueWindow Dialogue;
    public string DialogueText;
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
        if (other.tag == "Player"){
            Debug.Log("Interacting With Object");
            Dialogue.UpdatePosition(transform.position);
            Dialogue.Show(DialogueText);
        }
    }

    void OnTriggerExit(Collider other){
        if(other.tag == "Player"){
            Debug.Log("leaving object");
            Dialogue.Close();
        }
    }

    void PickupObject(){
        gs.AddPickedUpItem(objectId);
        Destroy(gameObject);
    }
   
}
