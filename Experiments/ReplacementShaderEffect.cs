using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Rendering;
//using UnityEngine.Experimental.Rendering.HDPipeline;

[ExecuteInEditMode]
public class ReplacementShaderEffect : MonoBehaviour
{
    public Shader ReplacementShader;
    public Color OverDrawColor;
    public Material mat;
    void OnValidate()
    {
        Shader.SetGlobalColor("_BaseColor", OverDrawColor);
    }
    
    void Start()
    {
        //MaterialPropertyBlock.
     /*   Renderer asas = GetComponent<Renderer>();
        MaterialPropertyBlock b_property = new MaterialPropertyBlock();
        b_property.SetColor("_BaseColor", Color.white);
        HDAdditionalCameraData a = new HDAdditionalCameraData();*/

        /*DrawingSettings drawingSettings = new DrawingSettings();
        drawingSettings.overrideMaterial = mat;*/
        //drawingSettings.overrideMaterial = mat;
       // a.customRender += asas;

        //GetComponent<Camera>().SetReplacementShader(ReplacementShader, "RenderType");
    }

     void OnEnable()
     {
        //
            GetComponent<Camera>().renderingPath = RenderingPath.DeferredShading;

        if (ReplacementShader != null)
            GetComponent<Camera>().RenderWithShader(ReplacementShader, ""); //RenderType

        Debug.Log(GetComponent<Camera>().actualRenderingPath);  //RenderWithShader(ReplacementShader, "RenderType");

     }

     void OnDisable()
     {
         GetComponent<Camera>().ResetReplacementShader();
     }
}