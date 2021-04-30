using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmissionChangeColor : MonoBehaviour
{
    [Header("Barvičky")]
    public Color[] colorsToLerp;

    [Header("Průběh")]
    public float duration = 3.0F;

    [Header("Ostatní")]
    public Material material;

    void Start()
    {
        //material = GetComponent<Material>();
    }

    void FixedUpdate()
    {
        float t = Mathf.PingPong(Time.time, duration) / duration;
        Color changingCol = Color.Lerp(colorsToLerp[0], colorsToLerp[1], t);
        changingCol *= 3f;
            //_EmissiveColor
        material.SetColor("_EmissiveColor", changingCol); // 3f == intensity
        
        
    }
}
