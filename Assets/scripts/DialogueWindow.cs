using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;
using Cinemachine;
public class DialogueWindow : MonoBehaviour
{
    public TMP_Text Text;
    private string CurrentText;
    CanvasGroup Group;
    private Camera currentCamera;
    Vector3 target;
    public Vector3 offset;
    //public Vector2 pos;
    
   
    void Start()
    {
        Group = GetComponent<CanvasGroup>();
        Group.alpha = 0;
    }

    void Update(){
        SetPosition();
    }
   
    public void Show(string text){
        Group.alpha = 1;
        CurrentText = text;
        StartCoroutine(DisplayText());
    }
    public void Close(){
        Group.alpha = 0;
        StopAllCoroutines();
    }

    private IEnumerator DisplayText(){
        Text.text = "";
        foreach(char c in CurrentText.ToCharArray()){
            Text.text+=c;
            yield return new WaitForSeconds(.1f);
        }
        yield return null;
    }

   public void SetPosition(){
        Camera mainCamera = Camera.main;
        Vector3  pos = mainCamera.WorldToScreenPoint(target);
        if(transform.position != pos){
            transform.position = pos+offset;
        }    
   }
   public void UpdatePosition(Vector3 pos){
        target = pos;
   }


    
}
