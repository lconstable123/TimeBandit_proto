using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class end : MonoBehaviour
{
   [SerializeField] PlayerController pc;
    public void End(){
        if(pc != null){
            pc.endgame=true;
        }
    }


}

