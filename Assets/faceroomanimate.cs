using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class faceroomanimate : MonoBehaviour
{
    Renderer objectRenderer;
    Material mat;
     
     //[SerializeField] Material mat;
     [SerializeField] float delay = 1f;
     public bool isChanging = true;
     public int num;
     int val=0;
    // Start is called before the first frame update
    void Start()
    {
      objectRenderer = GetComponent<Renderer>();
       //mr = GetComponent<MeshRenderer>();
       mat = objectRenderer.material;
    StartCoroutine(facechanger());
        
    }

    // Update is called once per frame
    void Update()
    {

        
    }

    IEnumerator facechanger(){
        yield return new WaitForSeconds(delay);
        changeface();
        if (isChanging){
            StartCoroutine(facechanger());
        }


    }

    void changeface(){
        int randomVal = Random.Range(0,3);
        if (randomVal == val){
            randomVal = (randomVal+1)%4;
        }
       val = randomVal;
       mat.SetFloat("_frame",val);

    }

}
