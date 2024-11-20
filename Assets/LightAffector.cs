using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;

public class LightAffector : MonoBehaviour
{
    Transform playerTrans;
    SpriteRenderer sr;
    public Color32 nearestlightcol;
    public Color32 blendcol;
    public float normdist;
    public Color32 basecol;
    public float maxSearch = 10f;       // Maximum distance for color change
    public float minSearch = 2f;        // Minimum distance for color change
    // Start is called before the first frame update
    public float dist;
    public float foundDist;
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        lightspiller[] lights = FindObjectsOfType<lightspiller>();
        lightspiller nearestLight = null;
        float minDistance = Mathf.Infinity;
        Vector3 pos = gameObject.transform.position;
        
        

        foreach (lightspiller light in lights){
            float distance = Vector3.Distance(pos, light.gameObject.transform.position);
            if (distance < minDistance){
                minDistance = distance;
                nearestLight = light;
                dist = distance;
            }
        }

        if (nearestLight != null){
            Color32 tempcol;
            foundDist = nearestLight.distance;
            dist = Mathf.Clamp(dist, minSearch, foundDist);
            normdist = (dist - minSearch) / (foundDist - minSearch);
            tempcol = Color.Lerp( nearestLight.colly,basecol, normdist);
            nearestlightcol = nearestLight.colly;
            blendcol = tempcol;
            sr.color = tempcol;
            }
    }
}
