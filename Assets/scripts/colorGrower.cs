using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UIElements;

public class colorGrower : MonoBehaviour
   
{
     [SerializeField] float speed = 1f;
    [SerializeField] float maxSize = 15f;
    private float floatSize = 0f;

    private Vector3 scale = new Vector3(0f, 0f,100f);
    // Start is called before the first frame update
    void Start()
    {
        floatSize = 100f;
        scale = new Vector3(floatSize,floatSize,100f);
    }

    // Update is called once per frame
    void Update()
    {
        if (floatSize < maxSize){
            floatSize += speed*Time.deltaTime;
            scale = new Vector3(floatSize,floatSize,100f);
            transform.localScale = scale;
        } else {
          Destroy(this.gameObject);
        }
    }
}
