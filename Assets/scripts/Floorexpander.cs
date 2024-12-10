using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorExpander : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] GameObject shape;
    [SerializeField] Shader shader;
    [SerializeField] Color32[] colors;

    [SerializeField] float increment = .2f;
    public bool isSpawning = true;
    public int colorcounter;
    public int actualcounter;

    [SerializeField] float heightIncrement = .001f;

   
    void Start()
    {
        colorcounter = 0;
      actualcounter = 0;
        startSpawner();
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void startSpawner(){
        if(isSpawning){
            StartCoroutine(spawntimer());
        }

    }
    
    IEnumerator spawntimer(){
        spawnObject(colorcounter);
        yield return new WaitForSeconds(increment);
        colorcounter++;
        colorcounter %= colors.Length;
        
       actualcounter++;
        startSpawner();
        if (colorcounter == colors.Length-1){
            moveAlldown();
        }
        
        

    }

    void spawnObject(int col){
        GameObject inst;
        Vector3 offsetTransform = new Vector3(transform.position.x, transform.position.y+(colorcounter*heightIncrement), transform.position.z);
        transform.eulerAngles = new Vector3(45f, 90f, 0f);
        inst = Instantiate(shape,offsetTransform, Quaternion.Euler(-90f, 0f, 0f));
        Material dynamicMat = new Material(shader);
        Renderer rend = inst.GetComponent<Renderer>();
        rend.material = dynamicMat;
        rend.material.SetColor("_col", colors[col]);
        inst?.SetActive(true);
    }

    void moveAlldown(){
        
        colorGrower[] colfoors = FindObjectsOfType<colorGrower>();

      foreach(colorGrower col in colfoors){
        
          col.gameObject.transform.position = col.gameObject.transform.position- new Vector3(0,heightIncrement*colors.Length,0);
        }
    }

}
