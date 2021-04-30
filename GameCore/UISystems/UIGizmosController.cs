using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIGizmosController : MonoBehaviour
{
    // Pozice na které musí
    [Header("Obecné vlastnosti")]
    [Tooltip("3D pozice ve světě, na které má být gizmos vykresleno.")]
    public Vector3 positionToCalculateOn;

    private RectTransform gizmosTransform;

    private void Start(){
        if (gizmosTransform == null)
            this.gizmosTransform = GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void FixedUpdate(){
        // Výpočet pozice pro place
        this.gizmosTransform.position = Helpers.UIWorldSpaceToScreenSpace(this.positionToCalculateOn);
    }
}
