using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;


public class Pickup : MonoBehaviour
{
    [SerializeField] GameObject rotator;
    [SerializeField] bool isInteractable;
    [SerializeField] string objectName;

    
    [SerializeField] float rotSpeed;

    [SerializeField] string whereTo;
    [SerializeField] Color32 colour;
    [SerializeField] string objectId;
    [SerializeField] GameObject Textbox;
    [SerializeField] GameObject ActionText;
    [SerializeField] GameObject NameText;

    //GameObject Textbox;
    GameSession gs;
    //public UIWindow WindowHeader;
    
    DialogueWindow Dialogue;
    public string DialogueText;
    
    void Start()
    {
        gs = FindObjectOfType<GameSession>();
        objectId = GetComponent<UniqueId>().GetId();
        
        
        if (Textbox == null){
            Debug.Log("no textbox found");
        }

        
        Dialogue = Textbox.GetComponentInChildren<DialogueWindow>();
        if (Dialogue == null){
            Debug.Log("no dialogue container found");
        }

       if (gs.IsItemPickedUp(objectId) == true){
            Destroy(gameObject);
        }

        Textbox.SetActive(true);

        if (!isInteractable){
            ActionText.SetActive(false);
            Dialogue.offset.y -= 20;
            
        }
        if (NameText != null && string.IsNullOrEmpty(objectName) == false ){
            NameText.SetActive(true);
            Debug.Log("assigning title");
            NameText.GetComponent<TextMeshProUGUI>().text = objectName;
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
