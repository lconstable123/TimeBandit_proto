using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChracterAudio : MonoBehaviour
{
    [SerializeField] AudioClip stepsound;
    [SerializeField] AudioClip jumpsound;
    AudioSource ac;
    [SerializeField] float StepVol =.29f;
    [SerializeField] float JumpVol =.4f;
    // Start is called before the first frame update
    void Start()
    {
        ac = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void playJump(){
        if (ac != null){ac.PlayOneShot(jumpsound,JumpVol);}
    }
    public void playStep(){
        if (ac != null){ac.PlayOneShot(stepsound,StepVol);}

    }
}
