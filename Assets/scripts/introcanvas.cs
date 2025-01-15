using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class introcanvas : MonoBehaviour
{
    

    GameSession gs;

    // Start is called before the first frame update
    void Start()
    {
        gs = GameSession.Persistent;
    }



    public void startGame(){
        gs.LeaveScene("StartRoom");
    }

    public void DestroyThis(){
        Destroy(gameObject);
    } 
}
