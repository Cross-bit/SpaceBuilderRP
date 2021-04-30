using System;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.GameCore.API.Extensions;
using UnityEngine;


public class BlockChecker
{
    /// <summary>
    /// LOKÁLNÍ POZICE!!! (globální má objekt) (pozice pod parent(čili checkers holderem))
    /// </summary>
    public Vector3 position;

    private Transform checker_transform;

    public Transform checkers_container;

    public Renderer checkers_graphics;

    string parent_name;

    public Settings.Checkers_types checkerType;

    public BoxCollider checkerCollider;

    public BlockChecker checkerNextTo;

    public Material CheckerMaterial { get; private set; }

    /// <summary>
    /// Je na kontrolér obsazený?
    /// </summary>
    public bool isEmpty = true;


    /// <summary>
    /// GLOBÁLNÍ POZICE!!! - Automaticky se aktualizuje na základě bloku, ke kterému je kontrolér dítětem.
    /// </summary>
    public Transform CheckerTransform { get => checker_transform; set => checker_transform = value; }

    //public bool isActive = true;


    public BlockChecker(Vector3 c_position = new Vector3(), Transform parent_c = null, string parent_b_name = null,
        Settings.Checkers_types checker_t = Settings.Checkers_types.BLANK, bool isNew = false, Renderer c_graphics = null)
    {
        position = c_position;
        parent_name = parent_b_name;
        checkers_container = parent_c;
        checkerType = checker_t;
        checkers_graphics = c_graphics;

        // Pokud se vytváří nový 
        if (isNew)
        {
            CreateChecker();

            //GenerateGraphics();
        } // Pokud se má jen např. vypnout / zapnout 
        else
        {
            //PopulateChecker();
        }

    }

    // Vytvoří se checker objektu
    private void CreateChecker()
    {
        CreaterCheckerTransform();
        SetCheckerTransformParent(checkers_container.transform);
        SetCheckerPosition(this.position);
        CreateCheckerCollider();
    }

    public void CreaterCheckerTransform() {
        // Vytvoříme checker
        string checker_name = CreateCheckerGameObjectName();
        this.CheckerTransform = new GameObject(checker_name).transform;
    }

    public void SetCheckerTransformParent(Transform parentTransform) => this.CheckerTransform.parent = parentTransform;

    public void SetCheckerPosition(Vector3 position) => this.CheckerTransform.gameObject.transform.position = position;

    public void UpdateCheckerActiveState() => this.checkerCollider.enabled = isEmpty;

    public void SetCheckerActiveState(bool setTo) => this.checkerCollider.enabled = setTo;

    public void CreateCheckerCollider()
    {
        checkerCollider = CheckerTransform.gameObject.AddComponent<BoxCollider>();
        checkerCollider.gameObject.layer = LayerMask.NameToLayer("Checker");
    }

    //
    public void SetCheckerGraphics(GameObject main_graphics_object)
    {
        if (main_graphics_object == null) {
            Debug.LogError("Obdržený objekt neobsahuje grafiku pro kontrolér.");
            return;
        }
            
        // Projedeme složky grafiku
        foreach (Transform g_parts in main_graphics_object.GetComponentsInChildren<Transform>())
        {
            string g_part_name = g_parts.name.ToLower();
            // Najdeme tu s názvem checker
            if (g_parts.name.Contains("checker"))
            {
                if (this.position == g_parts.transform.position.GetRoundedPosition())
                {
                    if (this.checkers_graphics == null)
                    {
                        this.checkers_graphics = g_parts.GetComponent<Renderer>();
                    }
                    return;
                }
            }
        }

        Debug.LogError($"Nepodařilo se nalézt grafiku pro kontrolér v objektu {main_graphics_object.name}" +
            $" {main_graphics_object.transform.position}.");

    }

    // MATERIÁL A BARVA CHECKERU
    public void SetCheckerMaterial() {

        if (checkers_graphics != null)
        {
            this.CheckerMaterial = this.checkers_graphics.materials[0];
            if (this.CheckerMaterial != null)
            {
                this.CheckerMaterial.SetColor("_EmissiveColor", Helpers.GetCheckerColorByType(this.checkerType) * 5.5f);
            }
        }
        else
            Debug.LogError("Objekt kontroléru nemá grafiku.");
    }

    public void ResetCheckerMaterial() {
        if(this.checkers_graphics != null)
            this.checkers_graphics.sharedMaterial = this.CheckerMaterial;
    }

    // vytvoří název bloku
    public String CreateCheckerGameObjectName() => 
        "Checker(" + this.checkerType + "):" + this.position + "of object:" + this.parent_name;

    private void CheckersInteractive(bool state = false)
    {
        this.checkerCollider.enabled = state;
    }

    /*
     * 
     * - Checkery generujeme, protože se v budoucnu může hodit při buildu
     * - Celkové collidery budov poté ručo, protože každý objekt má specifický tvar... 
     * 
     */
}
