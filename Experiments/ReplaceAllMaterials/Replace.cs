using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Replace : MonoBehaviour
{
    public Renderer[] allRenderers;
    public Material mat;

    public List<Material>  saves = new List<Material>();

    private MaterialPropertyBlock _propBlock;

    // Start is called before the first frame update
    void Start()
    {

        _propBlock = new MaterialPropertyBlock();

        allRenderers = FindObjectsOfType<Renderer>();

        /* int ctr = 0;
         foreach (Renderer r in allRenderers)
         {
             saves[ctr] = r.material;
             r.material = mat;
             ctr++;
         }*/
        //int ctr = 0;
        foreach (Renderer r in allRenderers)
        {
              //saves.pus = r.material;
            if (r.materials.Length > 0)
            {
                /*saves.Add(r.material);*/
                r.material = mat;
                //_propBlock.SetColor("_BaseColor", Color.white);
                //ctr++;
                //r.SetPropertyBlock(_propBlock);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {


      /*  if (Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            int ctr = 0;
            foreach (Renderer r in allRenderers)
            {
                r.material = saves[ctr];
            }
        }*/

    }
}
