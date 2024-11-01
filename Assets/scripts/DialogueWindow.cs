using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;
using Cinemachine;
//using System.Numerics;
public class DialogueWindow : MonoBehaviour
{
    public TMP_Text Text;
    const string kAlphaCode = "<color=#00000000>";
    const float kMaxTextTime = 0.1f;
    public int TextSpeed =1;
    private string CurrentText;

    //ItemSO item;
    
    Animator Anim;
    private Camera currentCamera;
    Vector3 target;
    public Vector3 offset;
    public float animatedOffset;
    public RectTransform BoxAppear;

    Vector3 boxappearAlign;
   
    void Start()
    {
        Text.text = "";
        Anim = GetComponent<Animator>();
        if (Anim == null){
            Debug.Log("animator not found");
        }

       
    }

    void Update(){
        SetPosition();
    }
   
    public void Show(string text){
        Anim.SetBool("Open", true);
        CurrentText = text;
        
    }
    public void Close(){
        Anim.SetBool("Open", false);
        
    }
    //public void SetItem(ItemSO settoitem){
      //  item = settoitem;
   // }

    private IEnumerator DisplayText(){
        Text.text = "";
        string originalText = CurrentText;
        string displayedText = "";
        int alphaIndex = 0;

        foreach(char c in CurrentText.ToCharArray()){
            alphaIndex++;
            Text.text = originalText;
            displayedText = Text.text.Insert(alphaIndex,kAlphaCode);
            Text.text = displayedText;
            yield return new WaitForSeconds(kMaxTextTime/TextSpeed);
        }
        yield return null;
    }

   public void SetPosition(){
        Camera mainCamera = Camera.main;
        Vector3 pos = mainCamera.WorldToScreenPoint(target);
        
        Vector2 newoffset = new Vector2(0,animatedOffset);
        if(transform.position != pos){
            transform.position = pos+offset;
        }    
        
        BoxAppear.localPosition = newoffset;
        
        
   }
   public void UpdatePosition(Vector3 pos){
        target = pos;
   }
   public void OnDialogueOpen(){
        StartCoroutine(DisplayText());
   }
   public void OnDialogueClosed(){
        StopAllCoroutines();
        Text.text="";
   }


    
}
