using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
[ExecuteInEditMode]
public class UniqueId : MonoBehaviour
{
    
   
    [SerializeField] string id;
    // Start is called before the first frame update
    void Awake(){
        if (string.IsNullOrEmpty(id)){
            AssignUniqueId();
        }
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void AssignUniqueId(){
        id = System.Guid.NewGuid().ToString();
        EditorUtility.SetDirty(this);
    }
    public string GetId(){
        return id;
    }
}
