using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScenePersist : MonoBehaviour
{
    // Start is called before the first frame update
   void Awake(){
    Singleton();
   }
     private void Singleton()
    {
        int numScenePersists = FindObjectsOfType<ScenePersist>().Length;

        if (numScenePersists > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }
    public void ResetScenePersist(){
        Destroy(gameObject);
    }
}
