using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Rendering.Universal;
using UnityEngine.Rendering;

public class GameSession : MonoBehaviour
{
    public HashSet<string> pickedUpItems = new();
    [SerializeField] Volume ppvol;
    public float transitionDuration = .5f;
    [SerializeField] bool useSceneFade;
    ColorAdjustments cAdjust;
    //Dictionary<string,bool> levelItemKeys = new Dictionary<string,bool>();
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
    public void AddPickedUpItem(string itemId){
        pickedUpItems.Add(itemId);
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
                    cAdjust.postExposure.value = Mathf.Lerp(0f,-6f, elapsed/transitionDuration);
                    elapsed += Time.deltaTime;
                    yield return null;
                }
                cAdjust.postExposure.value = -6;

                SceneManager.LoadScene(whereTo);
                elapsed = 0f;
                while (elapsed < transitionDuration){
                    cAdjust.postExposure.value = Mathf.Lerp(-6f,0f, elapsed/transitionDuration);
                    elapsed += Time.deltaTime;
                    yield return null;
                }
                cAdjust.postExposure.value = 0;

                
    }
    
  


}
