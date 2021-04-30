using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakeGlassSolid : MonoBehaviour
{
    public Renderer rend;

    protected float _alpha = 0;

    #region nastavení alphy
    public float alpha
    {
        get
        {
            return _alpha;
        }
        set
        {
            if (value <= 0)
            {
                _alpha = 0;
            }
            else if (value >= 1)
            {
                _alpha = 1;
            }
            else
            {
                _alpha = value;
            }
        }
    }
    #endregion nastavení alphy
    public Material glass_mat;

    public Color solid_color_dif;

    [SerializeField]
    bool is_solid;

    // Start is called before the first frame update
    void Start()
    {
        // Načtení materiálu skla z render queue
        glass_mat = rend.materials[0];
    }

    // Update is called once per frame
    void Update()
    {
        // Rozhodneme zda je solid, nebo ne 
        if (Input.GetKeyDown(KeyCode.Space) && !is_solid)
        {
            is_solid = true;
        } 
        else if (Input.GetKeyDown(KeyCode.Space))
        {
            is_solid = false;
        }

        // Spustíme transformaci skla
        if (is_solid)
        {
            alpha += 0.01f;
        }
        else
        {
            alpha -= 0.01f;
        }

        // Změna base color materiálu
        glass_mat.SetColor("_BaseColor", new Color(solid_color_dif.r, solid_color_dif.g, solid_color_dif.b, alpha));
    }

    public void Switch_Button()
    {
        if (!is_solid)
        {
            is_solid = true;
        }
        else
        {
            is_solid = false;
        }
    }

}
