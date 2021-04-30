
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
using Assets.Scripts.GameCore.WorldBuilding.BlockLibrary;

// Funkce, které sjou ZDE se již mohou vztahovat k objektům hry. V settings je jen settings a ty nejobecnější(např. Generic, s Vector3 apod.)

public static class Helpers
{
    // Vygenerujeme Knihovnu dostupných staveb 
    public static List<BlockSO> LoadSuitableBlocks(Settings.Checkers_types c_type)
    {

        var suitable_blocks = new List<BlockSO>();
        // Filtrujeme ze všech dostupných objektů pouze ty, které vyhovují podmínkám checkeru

        foreach (var b in UI.suitableBlocksOrganised)
        {
            if (b.blockType != Settings.Blocks_types.CITY_HALL)
                if (b.blockFunctionality == c_type || b.blockFunctionality == Settings.Checkers_types.BLANK)
                    suitable_blocks.Add(b);
        }

        return suitable_blocks;
    }

    [Obsolete("LoadSuitableBlocks již nepoužívám, použij raději  pro inicializaci všech typů bloků do knihovny na začátku.")]
    public static Dictionary<Settings.Checkers_types, List<BlockSO>> LoadAllSuitableBlocks()
    {
        var new_block_library = new Dictionary<Settings.Checkers_types, List<BlockSO>>();

        if (Settings.blocksTypeLibrary.Count > 0)
            foreach (Settings.Checkers_types c_type in Enum.GetValues(typeof(Settings.Checkers_types)))
            {

                if(!new_block_library.ContainsKey(c_type))
                    new_block_library.Add(c_type, new List<BlockSO>()); // new_block_library[c_type].Add();


                foreach (var block_type in Settings.blocksTypeLibrary)
                {
                    if (block_type.Value.blockFunctionality == c_type && block_type.Key != Settings.Blocks_types.CITY_HALL)
                    {
                        if(!new_block_library[c_type].Contains(block_type.Value))
                            new_block_library[c_type].Add(block_type.Value);
                    }
                }
            }

        return new_block_library;
    }

    public static void ReorganiseSuitableBlocks(BlockSO lastUsedBlock_data)
    {
        UI.suitableBlocksOrganised.Remove(lastUsedBlock_data);
        UI.suitableBlocksOrganised.Insert(0,lastUsedBlock_data);

        if (UI.suitableBlocksOrganised[0] != lastUsedBlock_data)
            Debug.LogError("Hups u reorganizace bloku se první item listu neshoduje s posledním použitým blokem něco je vážně špatně..."); 
    }


    /// <summary>Získá na začátku největší možný rozměr bloku.</summary>
    /// <returns></returns>
    public static float GetLargestSymConstantValue()
    {
        float[] all_sym_constants = new float[Settings.blocksTypeLibrary.Count];
        int ctr = 0;
        foreach (BlockSO block_data in Settings.blocksTypeLibrary.Values)
        {
            all_sym_constants[ctr] = block_data.checkersDistance;
            ctr++;
        }

        float maxValue = all_sym_constants.Max();
        if (maxValue > 0)
            return maxValue;
        else
            return 9; // Velikost CityHall
    }

    // Vrací blok z db(dictionary) podle pozice
    public static SymBlock GetBlock(Vector3 b_position = new Vector3())
    {
        SymBlock f_block = null;

        foreach (SymBlock b in BlockLibrary.blocksLib) // Bloky světa
        {
            if (b.BlockPosition == b_position)
            {
                f_block = b;
            }
        }

        return f_block;
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="lastActiveBlock"></param>
    /// <param name="placePos"> //!!! GLOBÁLNÍ POZICE CHECKERU !!!</param>
    /// <returns></returns>
    public static BlockChecker GetLastActiveChecker(SymBlock lastActiveBlock, Vector3 placePos)
    {
        BlockChecker lastActiveChecker = null;

        // Tady se dohledá checker na který se kliklo (v db)
        foreach (BlockChecker c in lastActiveBlock.Checkers)
        {
            Vector3 checker_pos = c.CheckerTransform.position; // GLOBÁLNÍ POZICE CHECKERU

            // Pokud se pozice daného checkeru rovná pozici kterou nám vrátili hit data
            if (checker_pos == placePos)
            {
                // Tak se jedná o last active
                lastActiveChecker = c;

                // Ujistíme se, že jsou dány správně codinates, jinak přepíšeme na správné
                if (lastActiveChecker.checkers_container.position != lastActiveBlock.BlockPosition)
                {
                    lastActiveChecker.checkers_container.position = lastActiveBlock.BlockPosition;
                }
                break;
            }
        }

        return lastActiveChecker;
    }


    /// <summary> Vrací všechny bloky od daného počátku v daném okruhu.</summary>
    /// <param name="originPos"> Globální pozice bloku. </param>
    /// <param name="radius"></param>
    /// <returns></returns>
    public static List<SymBlock> GetAllBlocksInRadius(SymBlock b_base, float radius)
    {
        var blocksInRadius = new List<SymBlock>();
        if (radius > 0)
        if (BlockLibrary.blocksLib != null)
        {
            foreach (SymBlock block in BlockLibrary.blocksLib)
            {
                Vector3 vecBetween = block.BlockContainer.gameObject.transform.position - b_base.BlockContainer.transform.position;
                float distanceBetween = vecBetween.magnitude;
                /*Debug.Log(distanceBetween);
                Debug.Log(radius);*/

                if (distanceBetween >= 0 && block != b_base)
                if (distanceBetween <= radius)
                    blocksInRadius.Add(block);
                }
        }
        else
        {
            Debug.LogError("Databáze bloků new World je epmpty. (GetAllBlocksInRadius).");
        }

        if (blocksInRadius.Count < 0)
        {
            Debug.LogError("Nebyly nalezeny žádné bloky.");
        }

        return blocksInRadius;
    }

    // Zvýraznění ve hře
    public static Material GameHighlights(Settings.GameHighlights hType, Renderer objectRenderer)
    {
        // Uložíme si base materiál pro vrácení
        Material defMaterial = objectRenderer.material;

        // Projedeme veškeré highlighty vytvořené v manageru
        foreach (Manager.InGamehighlights highlight in Manager.Instance.inGamehighlights)
        {
            if (highlight.highlight_type == hType)
            {
                if (hType == Settings.GameHighlights.CHECKER)
                {
                    // Nastavíme momentální materiál na null
                    objectRenderer.sharedMaterial = Manager.Instance.highlightMaterial;
                    objectRenderer.material.SetTexture("_BaseColorMap", defMaterial.mainTexture);
                    objectRenderer.material.SetColor("_EmissiveColor", highlight.highlight_color);

                }
            }
        }
        return defMaterial;

    }

    // -- METODY (HLAVNĚ) PRO UPDATE --


    /// <summary> Vrací pozici pro UI založenou na pozici ve 3D.</summary>
    /// <param name="pos"></param>
    /// <param name="clampToPositive"></param>
    /// <returns></returns>
    public static Vector3 UIWorldSpaceToScreenSpace(Vector3 pos, bool clampToPositive = true)
    {
        Vector3 newPos = Manager.Instance.mainCamera.WorldToScreenPoint(pos, Camera.MonoOrStereoscopicEye.Mono);

        if (newPos.z >= 0 && clampToPositive)
            return newPos;
        else
            return pos;
    }

    
    /// <summary> Vrací příslušnou barvu kontroléru, podle typu.</summary>
    /// <param name="c_type"></param>
    /// <returns></returns>
    public static Color GetCheckerColorByType(Settings.Checkers_types c_type)
    {
        // Podle typu checkeru rozřadíme barvičky
        switch (c_type){
            case Settings.Checkers_types.LIFE_SUPPORT:
                return ColorDecryptor.GetColorFromString(Settings.COLOR_C_TYPE_GREAN);

            case Settings.Checkers_types.POWER:
                return ColorDecryptor.GetColorFromString(Settings.COLOR_C_TYPE_ORANGE);

            case Settings.Checkers_types.BLANK:
            case Settings.Checkers_types.STANDART:
                return ColorDecryptor.GetColorFromString(Settings.COLOR_C_TYPE_BLUE); //Manager.Instance.themeModulColors[0];
            default:
                return new Color();
        }
    }

    public static Vector2 GetScreenCenter(){
        return new Vector2(Screen.width/2, Screen.height / 2);
    }

    /*IEnumerator BuildCoroutine(float buildTime, GameObject obj, Block block)
    {
        // Objekt UI časovače.
        GameObject currentTimer = obj;

        currentTimer.SetActive(true);
        //TODO:
        yield return new WaitForSeconds(buildTime);
        block.BuildBlock();
        currentTimer.SetActive(false);
        ScreenUIHolder.Instance.UIPoolList.ReturnToPool(Settings.PoolTypes.UI_TIMER, currentTimer);
    }*/

    public static bool CastInteractRay(out RaycastHit hitData)
    {
        hitData = new RaycastHit();

        if (Settings.canInteract)
        {
            // Ray data
            Ray ray = Manager.Instance.mainCamera.ScreenPointToRay(InputManager.Instance.GeneralInputs.MousePosition);

            if (Physics.Raycast(ray, out hitData, Settings.hitDistance) && !Settings.IsPointerOverUIObject())
                return true;
        }
        return false;
    }

}
