using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;


public class Pickup : MonoBehaviour
{
    

    
    [SerializeField] ItemSO item;

    [Header("styling")]
    [SerializeField] float rotSpeed;
    

    [Header("Internal References")]
    [SerializeField] GameObject Textbox;
    [SerializeField] GameObject ActionText;
    [SerializeField] GameObject NameText;
    [SerializeField] GameObject rotator;

    //GameObject Textbox;
    GameSession gs;
    public string objectId;
    //public UIWindow WindowHeader;
    
    DialogueWindow Dialogue;
    
    
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
        //Dialogue.SetItem(item);
        Textbox.SetActive(true);

        if (item.GetIsInteractable() == false){
            ActionText.SetActive(false);
            Dialogue.offset.y -= 20;
            
        }
        if (NameText != null && string.IsNullOrEmpty(item.GetName()) == false ){
            NameText.SetActive(true);
            //Debug.Log("assigning title");
            NameText.GetComponent<TextMeshProUGUI>().text = item.GetName();
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
    public void SetItem(ItemSO Setitem){
        item = Setitem; 
    }

    void OnTriggerEnter(Collider other){
        if (other.tag == "Player"){
           
            //Dialogue.UpdatePosition(transform.position);
            //Dialogue.Show(item.GetDescription());
            PickupObject();
            
        }
    }

    void OnTriggerExit(Collider other){
        if(other.tag == "Player"){
            //Debug.Log("leaving object");
    Dialogue.Close();
        }
    }

    void PickupObject(){
        gs.AddPickedUpItem(objectId,item);
        Destroy(gameObject);
    }
   
}
