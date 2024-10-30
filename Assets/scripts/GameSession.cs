using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSession : MonoBehaviour
{
    public HashSet<string> pickedUpItems = new();
    //Dictionary<string,bool> levelItemKeys = new Dictionary<string,bool>();
    void Awake()
    {
        Singleton();
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
            DontDestroyOnLoad(gameObject);
        }
    }

    void ResetGameSession(){
        SceneManager.LoadScene(0);
        FindObjectOfType<ScenePersist>().ResetScenePersist();
        Destroy(gameObject);
    }
    public void AddPickedUpItem(string itemId){
        pickedUpItems.Add(itemId);
    }
    public bool IsItemPickedUp(string itemId){
        return pickedUpItems.Contains(itemId);
    }
    public void ResetPickedUpItems(){
        pickedUpItems.Clear();
    }
    
  


}
