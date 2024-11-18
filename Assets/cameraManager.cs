using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraManager : MonoBehaviour
{
    Animator animator;
    // Start is called before the first frame update
    void Start()
    {
    animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1)){
            Debug.Log("pressed");
            animator.SetTrigger("cam1");
        }
        if (Input.GetKeyDown(KeyCode.Alpha2)){
            Debug.Log("pressed");
            animator.SetTrigger("cam2");
        }
        if (Input.GetKeyDown(KeyCode.Alpha3)){
            Debug.Log("pressed");
            animator.SetTrigger("cam3");
        }
    }
}
