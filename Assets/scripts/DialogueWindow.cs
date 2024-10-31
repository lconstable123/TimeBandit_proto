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
    //CanvasGroup Group;
    Animator Anim;
    private Camera currentCamera;
    Vector3 target;
    public Vector3 offset;
    public float animatedOffset;
    public RectTransform BoxAppear;
    //public Vector2 pos;
    Vector3 boxappearAlign;
   
    void Start()
    {
        Text.text = "";
        //Group = GetComponent<CanvasGroup>();
        Anim = GetComponent<Animator>();
        if (Anim == null){
            Debug.Log("animator not found");
        }
        
       // Group.alpha = 0;
       
    }

    void Update(){
        SetPosition();
    }
   
    public void Show(string text){
        //Group.alpha = 1;
        
        //Debug.Log("animating on");
        Anim.SetBool("Open", true);
        CurrentText = text;
        
    }
    public void Close(){
        //Group.alpha = 0;
        Anim.SetBool("Open", false);
        
    }

    private IEnumerator DisplayText(){
        Text.text = "";
        string originalText = CurrentText;
        string displayedText = "";
        int alphaIndex = 0;

        foreach(char c in CurrentText.ToCharArray()){
            alphaIndex++;
            Text.text = originalText;
            //Text.text+=c;
            displayedText = Text.text.Insert(alphaIndex,kAlphaCode);
            Text.text = displayedText;
            yield return new WaitForSeconds(kMaxTextTime/TextSpeed);
        }
        yield return null;
    }

   public void SetPosition(){
        Camera mainCamera = Camera.main;
        Vector3 pos = mainCamera.WorldToScreenPoint(target);
        //offset = new Vector3(offset.x,offset.y+animatedOffset,offset.z);
        Vector2 newoffset = new Vector2(0,animatedOffset);
        if(transform.position != pos){
            transform.position = pos+offset;
        }    
        //boxappearAlign = BoxAppear.transform.position;
        BoxAppear.localPosition = newoffset;
        //BoxAppear.transform.position = boxappearAlign+newoffset;
        
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
