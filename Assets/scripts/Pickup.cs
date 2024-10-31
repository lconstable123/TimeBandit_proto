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
    GameObject Textbox;
    GameSession gs;
    //public UIWindow WindowHeader;
    
    DialogueWindow Dialogue;
    public string DialogueText;
    //Material mat;
    //Renderer renderer = GetComponent<Renderer>();
    // Start is called before the first frame update
    void Start()
    {
        gs = FindObjectOfType<GameSession>();
        objectId = GetComponent<UniqueId>().GetId();
        Textbox = GetComponentInChildren<Canvas>().gameObject;
        
        if (Textbox == null){
            Debug.Log("no textbox found");
        }
        Textbox.SetActive(true);
        Dialogue = Textbox.GetComponentInChildren<DialogueWindow>();
        if (Dialogue == null){
            Debug.Log("no dialogue container found");
        }

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
