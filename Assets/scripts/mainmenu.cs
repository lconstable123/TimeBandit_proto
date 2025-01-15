using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class mainmenu : MonoBehaviour
{
    GameSession gs;
    [SerializeField]  GameObject canvas;
    bool doonce = false;
    // Start is called before the first frame update
    void Start()
    {
        gs = GameSession.Persistent;
        StartCoroutine(Unlock());
    }

    void Update(){
        CheckQuit();
    }

    void OnAnyKey(){
        if (doonce){
            Animator an = canvas.GetComponent<Animator>();
            an.SetTrigger("fade");
        }
    }

    public void startGame(){
        gs.LeaveScene("StartRoom");
    }

    public void DestroyThis(){
        Destroy(gameObject);
    } 

    IEnumerator Unlock(){
        yield return new WaitForSeconds(1.5f);
        doonce = true;
    }

       void CheckQuit()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else

            Application.Quit();
#endif
        }
    }

}

