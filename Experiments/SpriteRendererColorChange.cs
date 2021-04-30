using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EmissionColorChange : MonoBehaviour {

    public Color color1 = Color.red;
    public Color color2 = Color.blue;
    [Header("Průběh")]
    public float duration = 3.0F;
    [Header("Ostatní")]
    public SpriteRenderer thisBg;

    void Start()
    {
        thisBg = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        float t = Mathf.PingPong(Time.time, duration) / duration;
        thisBg.color = Color.Lerp(color1, color2, t);
    }
}
