using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Rendering.Universal;
using UnityEngine.Rendering;
using TMPro;

using System.Linq;
using System.Data.Common;
using Unity.VisualScripting;
using Unity.Mathematics;

using System;

public class GameSession : MonoBehaviour
{
    public HashSet<string> pickedUpItems = new();
    AudioSource ac;
   
    public Dictionary<string,Vector3> itemLocations = new();
    public List<string> scenesInitialised = new();
    public int DebugCounter;
    public static GameSession Persistent {get; private set;} 
    [SerializeField] Volume ppvol;
    public float transitionDuration = .5f;
    [SerializeField] bool useSceneFade;
     [SerializeField] AudioClip roomEnterSound;
     [SerializeField] float roomenterVol = .5f;
    ColorAdjustments cAdjust;
    [SerializeField] TextMeshProUGUI inventoryGUI;
    [SerializeField] TextMeshProUGUI doorStatus;
    [SerializeField] public TextMeshProUGUI debug;
    [SerializeField] public TextMeshProUGUI currentLoc;
        // [SerializeField] public GameObject TimeBandit;
    public Dictionary<string,ItemSO> itemsDict = new();
    public Pickup TakeableObject;
    
    Dictionary<string, List<ItemInstance> > ItemsInLevel = new();

    public GameObject player;
    [SerializeField] GameObject itemPrefab;
    public int enteredDoor = 0;
    public bool camerasLocked = false;
    public bool centralDoorOpen=false;
    public bool EndDoorOpen=false;
    public bool centralRoomFirstTime = true;
     public bool blueDoorOpen=false;
    
    [SerializeField ] GameObject PauseMenu;
    public bool isPaused = false;
    public bool pauseable = true;

    public class ItemInstance
{
    public string Id;
    public Vector3 Loc;
    public ItemSO Iteminfo;
    public string Level;
    

    public ItemInstance(string id, Vector3 loc, ItemSO iteminfo, string level)
    {
        this.Id = id;
        this.Loc = loc;
        this.Iteminfo = iteminfo;
        this.Level = level;
    } 
}

    void Awake()
    {
        Singleton();
    }
    void Start(){
        ac = GetComponent<AudioSource>();
        // if(ppvol.profile.TryGet(out cAdjust)){
        //   Debug.Log("found post effect");
        // } else {
        //    Debug.Log("could not find effect");  
        // }
    //player = FindObjectOfType<PlayerController>().gameObject;
    }

    void Update(){
        // if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Joystick1Button1) ){
        //     if(TakeableObject!= null){
        //     TakeableObject.PickupObject();
        //     } else{
        //     DropObject();}
        // }
      // doorStatus.text = enteredDoor.ToString();
    }

    private void Singleton()
    {
        int numGameSessions = FindObjectsOfType<GameSession>().Length;

        if (numGameSessions > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            Persistent = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public void ResetGameSession(){
       // SetDoorEntered(0);  
        //SceneManager.LoadScene(0);
        StartCoroutine(LoadSceneAsync(0));
        //SceneManager.LoadSceneAsync(0,LoadSceneMode.Single);
        //FindObjectOfType<ScenePersist>().ResetScenePersist();     
    }
    IEnumerator LoadSceneAsync(int num){
        //if (ac != null){ac.PlayOneShot(roomEnterSound,roomenterVol);}
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(num);
        asyncOperation.allowSceneActivation = false;
        // while (!asyncOperation.isDone){
        //     yield return null;
        // }
        while (!asyncOperation.isDone){
             if (asyncOperation.progress >= 0.9f){
                Destroy(gameObject);
                asyncOperation.allowSceneActivation = true;
             }
            yield return null;
            }
        
   
        
    }
       IEnumerator LoadSceneAsyncString(string name){
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(name);
        while (!asyncOperation.isDone){
            yield return null;
        }
    
        
   
        
    }
    
    private void DebugInv()
    {
        string subs = "";
        string inv = "";
        foreach (string key in itemsDict.Keys)
        {
            subs = key + ", ";
            inv += subs;
        }
        Debug.Log(inv);
    }

    public void Pause( bool state){
        if (pauseable && PauseMenu != null){
            if (state){
                //isPaused = true;
                
                PauseMenu.SetActive(true);
            } else {
                //isPaused = false;
                PauseMenu.SetActive(false);
            }
        }
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
        //SceneManager.LoadScene(whereTo);
         if (ac != null){ac.PlayOneShot(roomEnterSound,roomenterVol);}
        StartCoroutine(LoadSceneAsyncString(whereTo));
       // SetSceneInitialised();
        // if(useSceneFade){
        //     StartCoroutine(ILeaveScene(whereTo));
        // } else {
        // SceneManager.LoadScene(whereTo);
        // }
    }

    public void LeaveSceneWithFade(string whereTo){
        StartCoroutine(ILeaveScene(whereTo));
    }
    IEnumerator ILeaveScene(string whereTo){
                //Debug.Log("entered doorway");
                //StoreObjectStates();
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
//     public void AddPickedUpItem(string itemId, ItemSO item)
//     {
//         pickedUpItems.Add(itemId);
//         itemLocations.Remove(itemId);
//         itemsDict.Add(itemId, item);

//         RefreshInventory();
//     }
//     void DropObject(){
        
//         if (itemsDict != null && itemsDict.Count > 0){
            
//             string id = itemsDict.Keys.First();
//             player = FindObjectOfType<PlayerController>().gameObject;
//             Vector3 pos = player.transform.position - player.transform.forward*.5f;
//             GameObject droppedItem = Instantiate(itemPrefab, pos, Quaternion.identity);
//             Pickup p = droppedItem.GetComponent<Pickup>();

//             pickedUpItems.Remove(id);
//             //find first item in inv
//             p.objectId = id;
//             Debug.Log("dropping "+p.objectId);
            
//             //set dropped item type
//             p.SetItem(itemsDict.Values.First());
//             //set persistent location

//             if (!itemLocations.ContainsKey(id)){
//                 itemLocations.Add(id,pos);
//             } else {
//                 itemLocations[id] = pos;
//             }

//             p.isEnabled = false;

//             itemsDict.Remove(p.objectId);
//             RefreshInventory();
//             //Debug.Log(p.objectId);
//         }
//     }
//     public void SetSceneInitialised(){
//     string sceneName = GetSceneName();
//     if (!scenesInitialised.Contains(sceneName)){
//         scenesInitialised.Add(sceneName);
//        // Debug.Log("initialising scene" + sceneName);
//         StoreObjectStates();
//     } else {
//        // Debug.Log("scene already initialised, saving items " + sceneName);
//         StoreObjectStates();
//     }
//    }
//     public bool IsSceneInitialised(){
//         return scenesInitialised.Contains(GetSceneName());
//    }
//     public void StoreObjectStates(){
        
//         List<ItemInstance> localPopulatedPickups = new();
        
//         Pickup[] pickups = FindObjectsOfType<Pickup>();
//        // Debug.Log("storing "+ pickups.Length + " items"); 
//         foreach (Pickup pickup in pickups){
//             string id = pickup.objectId;
//             Vector3 pos = pickup.gameObject.transform.position;
//             ItemSO objectType = pickup.getitem();
//             string level = GetSceneName();
//             ItemInstance item = new(id, pos,objectType, level);
                
//             localPopulatedPickups.Add(item);

            
//         }
       
//         ItemsInLevel[GetSceneName()] = localPopulatedPickups;
//         Debug.Log("storing "+ localPopulatedPickups.Count + " items in " + GetSceneName() ); 
//    }
//     public void PopulateObjectStates(){
//         Pickup [] existingPickups = FindObjectsOfType<Pickup>();
//         foreach (Pickup p in existingPickups){
//             Destroy(p.gameObject);
//         }

//         List<ItemInstance> items = ItemsInLevel[GetSceneName()];
//         Debug.Log("repopulating "+ items.Count + " items in " + GetSceneName() ); 
//         foreach ( ItemInstance item in items){
//           GameObject g = Instantiate(itemPrefab,item.Loc,quaternion.identity);
//           Pickup gP = g.GetComponent<Pickup>();
//           gP.objectId = item.Id; 
//           gP.SetItem(item.Iteminfo);
//         }
//     }
    string GetSceneName(){
    return SceneManager.GetActiveScene().name;
   }
    public int GetDoorEntered(){
        return enteredDoor;
    }
    public void SetDoorEntered(int door){
        enteredDoor = door;

    }
}

