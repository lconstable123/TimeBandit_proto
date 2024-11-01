using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Rendering.Universal;
using UnityEngine.Rendering;
using TMPro;
using Unity.VisualScripting;
using System.Linq;

public class GameSession : MonoBehaviour
{
    public HashSet<string> pickedUpItems = new();
    public List<ItemSO> inventory = new();
    [SerializeField] Volume ppvol;
    public float transitionDuration = .5f;
    [SerializeField] bool useSceneFade;
    ColorAdjustments cAdjust;
    [SerializeField] TextMeshProUGUI inventoryGUI;
    Dictionary<string,ItemSO> itemsDict = new Dictionary<string,ItemSO>();
    GameObject player;
    [SerializeField] GameObject itemPrefab;
    void Awake()
    {
        Singleton();
    }
    void Start(){
        if(ppvol.profile.TryGet(out cAdjust)){
            Debug.Log("found post effect");
        } else {
            Debug.Log("could not find effect");  
        }
    player = FindObjectOfType<PlayerController>().gameObject;
    }

    void Update(){
        if (Input.GetKeyDown(KeyCode.P)){
            Debug.Log("dropping");
            DropObject();
        }
    }

    private void Singleton()
    {
        int numGameSessions = FindObjectsOfType<GameSession>().Length;

        if (numGameSessions > 1)
        {
            Debug.Log("game session exists, destroying lastest one");
            Destroy(gameObject);
        }
        else
        {
            Debug.Log("keeping game session");
            DontDestroyOnLoad(gameObject);
        }
    }

    void ResetGameSession(){
        SceneManager.LoadScene(0);
        FindObjectOfType<ScenePersist>().ResetScenePersist();
        Destroy(gameObject);
    }
    public void AddPickedUpItem(string itemId, ItemSO item)
    {
        pickedUpItems.Add(itemId);
        //inventory.Add(item);
        itemsDict.Add(itemId, item);
        RefreshInventory();
    }

    private void RefreshInventory()
    {
        string itemList = "";
        foreach (ItemSO itemL in itemsDict.Values)
        {
            itemList += itemL.GetName() + "\n";
        }
        inventoryGUI.text = itemList;
    }

    public bool IsItemPickedUp(string itemId){
        return pickedUpItems.Contains(itemId);
    }
    public void ResetPickedUpItems(){
        pickedUpItems.Clear();
    }
    public void LeaveScene(string whereTo){
        if(useSceneFade){
            StartCoroutine(ILeaveScene(whereTo));
        } else {
        SceneManager.LoadScene(whereTo);
        }
    }

    IEnumerator ILeaveScene(string whereTo){
                Debug.Log("entered doorway");
                float elapsed = 0f;
                while (elapsed < transitionDuration){
                    cAdjust.postExposure.value = Mathf.Lerp(0f,-10f, elapsed/transitionDuration);
                    elapsed += Time.deltaTime;
                    yield return null;
                }
                cAdjust.postExposure.value = -10;

                SceneManager.LoadScene(whereTo);
                elapsed = 0f;
                while (elapsed < transitionDuration){
                    cAdjust.postExposure.value = Mathf.Lerp(-10f,0f, elapsed/transitionDuration);
                    elapsed += Time.deltaTime;
                    yield return null;
                }
                cAdjust.postExposure.value = 0;

                
    }

    void DropObject(){
        
        if (itemsDict != null && itemsDict.Count > 0){
            player = FindObjectOfType<PlayerController>().gameObject;
            Vector3 pos = player.transform.position - player.transform.forward*2f;
            GameObject droppedItem = Instantiate(itemPrefab, pos, Quaternion.identity);
            Pickup p= droppedItem.GetComponent<Pickup>();
            p.objectId = itemsDict.Keys.First();
            p.SetItem(itemsDict.Values.First());
            //work on this
            itemsDict.Remove(p.objectId);
        }
    }
    
  


}
