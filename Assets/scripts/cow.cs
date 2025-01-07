using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cow : MonoBehaviour
{
    [SerializeField] Material mat;
    [SerializeField] float scrollSpeed = 0.1f;
    [SerializeField] float maxOffset = 1f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       Vector2 offset = mat.mainTextureOffset;
       offset.y += scrollSpeed * Time.deltaTime; 
       if (offset.y > maxOffset){
        offset.y=0f;
       }
       //mat.SetVector
       mat.mainTextureOffset = offset;
    }
}
