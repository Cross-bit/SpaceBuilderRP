using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

/*@file
@brief This file is marvelous.*/

/// <summary>
/// Hlavní setting hry
/// </summary>
/// 
public static class Settings
{
    #region Konstanty

    // Cesty
    public const string BLOCKSO_PATH = "";
    public const string POOL_OBJECTS_PATH = "POOL_OBJECTS";
    public const string POOL_UI_OBJECTS_PATH = "POOL_UI";
    public const string IN_GAME_HIGHLIGHTS_PATH = "HIGHLIGHT/IN_GAME";
    public const string PREFABS_CODE_LOADED = "PREFABS_CODE_LOADED";
    public const string MUSIC_PATH = "MUSIC"; // Hudba hry
    public const string SOUND_PATH = "SOUND";
    public const string MATERIALS = "MATERIALS";

    // Hlavní cesta k root složce (veřejná data (to co si lidi mohou modovat/upravovat))
#if !UNITY_EDITOR
        public static readonly string ROOT_F = Application.persistentDataPath; // Pokud produkce
#else
    public static readonly string ROOT_F = "Assets"; // Pokud editor
#endif

    public static readonly string BASIC_PEOPLE_DATA_PATH = ROOT_F + "/people_data.json";

    // Scény
    public const string SCENES_PATH = "Assets/Scenes/";
    public const string SCREEN_UI_SCENE_NAME = "Screen-UI";
    public const string MAIN_MENU_SCENE_NAME = "main-menu";
    public const string MAIN_SCENE_NAME = "test-feelu";
    public const string INI_SCENE_NAME = "ini-scene";// POZN. pokud přidáš ještě další scénu, je potřeba přidat load A KOntrolu v Manageru (a změnit počet ACTIVE_SCENE_COUNT...)
    public const byte ACTIVE_SCENES_COUNT = 2;

    //UI TAGY oken
    public const string UI_TAG_BUILD_LIBRARY = "BUILD_LIBRARY";
    public const string UI_TAG_SCREEN_CANVAS = "SCREEN_UI";
    public const string UI_TAG_WORLD_CANVAS = "UI_WORLD_SPACE";

    public const string UI_TAG_STATIC_SCREEN = "UI_STATIC_SCREEN";
    public const string UI_TAG_STATIC_LEFT_PANEL = "UI_STATIC_LEFT_PANEL";
    public const string UI_TAG_STATIC_RIGHT_PANEL = "UI_STATIC_RIGHT_PANEL";
    public const string UI_TAG_STATIC_BOTTOM_PANEL = "UI_TAG_STATIC_BOTTOM_PANEL";

    public const string UI_TAG_SCROLL_WINDOW = "SCROLL_WINDOW";
    public const string UI_TAG_DETAIL_WINDOW = "DETAIL_WINDOW";
    public const string UI_TAG_BLOCK_CART = "BLOCK_CART";
    public const string UI_TAG_ASK_DIALOGUE = "ASK_DIALOGUE";
    public const string UI_TAG_BT_TRUE = "BT_TRUE";
    public const string UI_TAG_BT_FALSE = "BT_FALSE";
    public const string UI_TAG_BT_CLOSE = "BT_CLOSE";
    public const string UI_TAG_TITLE = "TITLE";
    public const string UI_TAG_TEXT = "TEXT";
    public const string UI_TAG_TOGGLE = "TOGGLE";
    public const string UI_TAG_BAR = "BAR";
    public const string UI_TAG_IMAGE = "IMAGE";
    public const string UI_TAG_SCREEN_HIGHLIGHT = "SCREEN_HIGHLIGHT";
    public const string UI_TAG_SIMPLE_MESSAGE = "SIMPLE_MESSAGE";
    public const string UI_TAG_GIZMOS = "UI_GIZMOS";

    //Názvy LAYER
    public const string CHECKER_LAYER = "Checker";
    public const string BLOCK_LAYER = "Block";

    // Názvy materiálů
    public const string HIGHLIGHT_MAT = "checker_highlight";
    #endregion

    #region Hráčovi interakce se světem
    public static LayerMask blockBuild = 9;
    public static float hitDistance = 100;
    #endregion

    // Kamera
    #region Proměnné kamery
    public static bool useCameraController = true;

    #endregion

    // Globální bool

    // Je nový stvět
    public readonly static bool isNewWorld = true;

    // Může hráč interagovat se světem?
    public readonly static bool canInteract = true;

    // Je něco zvýrazněno (checker/blok)
    public static bool isBuildMode = false;

    //Byla hra již )úspěšně načtena?
    public static bool isGameLoaded = false;

    public static bool isPlacingBlock = false;

    // Automatické přepínání kontrolérů při buildu
    public static bool switchCheckers = true;

    public static Vector3 defaultWorldPosition = new Vector3();

    // Je něco zvýrazněno (checker/blok)
    //public static bool isHighlighted = true;


    //Scény
    /*    public static bool is_ui_sceen_loaded = false;
        public static bool is_main_sceen_loaded = false;*/
    public static Dictionary<string, UnityEngine.SceneManagement.Scene> loadedScenes = new Dictionary<string, UnityEngine.SceneManagement.Scene>();


    // Proměnné KAMERY
    public static bool isCamera = true;

    // Custom tools
    /*public static bool rotate = true;
    public static bool highlightBlocksInRange = false;*/

    /// <summary>Databáze bloků</summary> 
    //public static List<Block> blocks = new List<Block>();

    // Knihovna Bloků
    /// <summary>Knihovna jednotlivých typů bloků</summary> TODO: Možná bude statčit přepsat na ten organizovaný list v knihovně bloků (UI.cs)
    public static Dictionary<Settings.Blocks_types, BlockSO> blocksTypeLibrary = new Dictionary<Settings.Blocks_types, BlockSO>();

    // OSTATNÍ proměnné
    public static float largestSymConstant;

    // Lidé
    /// <summary>Databáze Lidí</summary> 
    public static List<Person> people = new List<Person>();


    // COLORS // TODO: v hexadecimalce => proteď v Manager.cs

    // focus levly 
    // public enum focus_levels { All ,Building_manage, Add_building }

    /*
    * 
    * VERY IMPORTANT DO ENUM vždy přidávat na poslední pozici, jinak se ostatní položky přepíši!!!!! VERY VERY IMPORTANT
    * VERY IMPORTANT DO ENUM vždy přidávat na poslední pozici, jinak se ostatní položky přepíši!!!!! VERY VERY IMPORTANT
    * VERY IMPORTANT DO ENUM vždy přidávat na poslední pozici, jinak se ostatní položky přepíši!!!!! VERY VERY IMPORTANT
    * 
    */

    // Focus levly
    public enum Focus_levels { WORLD, BLOCK, CHECKER }

    // Typy bloků ve hře
    public enum Blocks_types { CITY_HALL, GLASS_TUBE, BASIC_TUBE, GLASS_CROSS, BASIC_GLASS_T, POWER_GENERATOR, LIVING_TOWER_1 }

    //Akce, které může hráč vyvolat, + ADD_BLOCK, DESTROY_BLOCK, ...
   // public enum BuildActions { LOAD, NEW, NEWWORLD }
    // EDIT_STANDART - změna typu trubky(kříž na rovnou apod.), 
    // EDIT_POWERPLANT - 

    // Typy checkerů/portů, ... / build typy funkce 
    public enum Checkers_types { BLANK, STANDART, POWER, LIFE_SUPPORT }

    /* <summary> Typy uzlů (jestli se jedná o křižovatku, blok apod.)</summary>
    /*public enum Nodetypes { LINE, NODE, BUILDING }*/ // TODO: Možná

    /// <summary> Zprávy, které se hráčovi ukáží na obrazovce, jaký screen highlight se spustí. Jakýsi hlavní highlight manager. </summary>
    public enum GameScreenEvents { WARNING, NOT_ABLE_TO, MESSAGE } // Řídí, vše co se Hráčovi ukáže za zpravu na obrazovce

    /// <summary> Highlighty přímo ve scéně. </summary>
    public enum GameHighlights { WARNING, BLOCK, CHECKER }

    /// <summary> Rámeček okolo obrazovky. </summary> //TODO: Nejspíš předělám, nelíbí se mi tam to maskování.
    public enum ScreenHighlights { NONE, BUILD, WARNING }

    /// <summary> Typy Poolů </summary>
    public enum PoolTypes { UI_TIMER, UI_SIMPLE_MESSAGE_TEXT, BUILD_ROBOTS, PATH_POINT, GIZ_GRID_SEGMENT }; //KONVENCE pokud UI objekt je nutné specifikovat UI

    public enum Axis { ZERO, x, y, z, w }; // osy

    public enum CanvasTypes { STATIC, WORLD, FLOAT }  // zatím jen pro pool systém (kdyžtak dopiš)

    // Enumy lidí
    public enum PersonQualification { DEFAULT, ENGINEER }
    public enum Gender { MALE, FEMALE, }



    // Barvy
    public const string COLOR_RED_WARNING = "F82B35"; // vybledlejší červená

    // Barvičky
    public const string COLOR_C_TYPE_BLUE = "69D2FF";
    public const string COLOR_C_TYPE_ORANGE = "FFA71E";
    public const string COLOR_C_TYPE_GREAN = "45FF4A";

    /*
     * VERY IMPORTANT DO ENUM vždy přidávat na poslední pozici, jinak se ostatní položky přepíši!!!!! VERY VERY IMPORTANT
     * VERY IMPORTANT DO ENUM vždy přidávat na poslední pozici, jinak se ostatní položky přepíši!!!!! VERY VERY IMPORTANT
     * VERY IMPORTANT DO ENUM vždy přidávat na poslední pozici, jinak se ostatní položky přepíši!!!!! VERY VERY IMPORTANT
     * 
     */

    // WORLD UI Settings
    //public static float timerSize = 0.06f;
    public static float timerHeight = 2.5f;// TODO: Bude se muset počítat dle colliderů... ?? jednou přijdi na to co tím básník myslel...
    public static float dialogWindowHeight = 6f;
    public static float uiRotateGizmosHeight = 0f;


    #region Hudba, zvuky, apod.
    // Zvukové efekty
    public enum SoundEffects { mouse_click_1, button_press_1, }

    #endregion


    /*
    * 
    * FUNKCE NAŠICH API APOD.
    * 
    */

    #region Functions

    /// <summary>
    /// Hodí nějaký dbug erro
    /// </summary>
    /// <param name="er_message"> Text message. </para>
    /// <param name="debug"></param>
   /* public static void ThrowError(string er_message, bool pauseGame = false)
    {
        GameObject error_obj = new GameObject("ERROR!!!!: " + er_message);
        Scripts.Utility.HierarchyHighlighter error_present = error_obj.AddComponent<Scripts.Utility.HierarchyHighlighter>();
        error_present.Background_Color = Color.red;

        if (pauseGame)
        {
            UnityEngine.Debug.LogError(er_message);
        }
        else
        {
            UnityEngine.Debug.LogWarning(er_message);
        }

        /* TYPY ERRORŮ
         * :-) - vše je ok
         * :-| - lehký error - hra běží, ale bylo by lepší fixnout
         * X-| - vážný error - hra běží, jen pravděpodobně nebude fungovat...
         * X-( - Hra spadne...
         */

    //}

    

    /// <summary>
    /// Vrací info, zda vec3 existuje v poli ar_vec3.
    /// </summary>
    /// <param name="vec3"> Hledaný Vector3. </param>
    /// <param name="ar_vec3"> Pole Vector3[] ve kterém hledám. </param>
    /// <returns></returns>
    public static bool Vector3Exists(Vector3 vec3, Vector3[] ar_vec3)
    {
        bool exist = false;

        foreach (Vector3 v in ar_vec3)
        {
            if (v == vec3)
            {
                exist = true;
            }
        }
        return exist;
    }

    /// <summary>
    /// Vrací info, zda je kurzor myši nad UI.
    /// </summary>
    /// <returns></returns>
    public static bool IsPointerOverUIObject()
    {
        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current)
        {
            position = InputManager.Instance.GeneralInputs.MousePosition
        };
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
        return results.Count > 0;
    }

    // Poslední (maximální) ochrana/nález komponentu(tzn. všeho co dědí z MonoB.), pro druhý stupeň => nutonost TAGU
    //Pozn. Možná dáme do settings.
    /// <summary>
    /// PRO DŮLEŽITÉ VĚCI!! POUZE POKUD EXISTUJE JEN TAG VE SCÉNĚ JEDNOU... Ověřuje zda je v Unity přiřazená refrence. V případě, že není, pokusí se najít. Pokud selže hodí X-| ERROR.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="toFind"> Jaký koliv objekt, dědící z objektu Component.</param>
    /// <param name="tag">Tag který by měl objekt ve scéně mít (defaultně Untagged).</param>
    /// <returns></returns>
    public static T IsComponentLoaded<T>(T toFind, string tag = "Untagged", string codeName = "") where T : Component
    {
        // Pokud je null
        if (toFind == null)
        {
            toFind = GameObject.FindObjectOfType<T>();
            string ob_tag = toFind?.gameObject.tag;

            if (toFind == null || ob_tag == "")
            {
                Debug.LogError("Hups zase load objekt...");
            }

            // Pokud se našel, ale není správný tag (tzn., jejich více ve stejně => jiná než find podle tagu neni 
            // !!! JE TO EXTRÉMĚ NÁROČNÉ, proto se tomu snažíme vyhnout do poslední chvíle...)
            if (!ob_tag.Equals(tag))
            {
                GameObject objectWithTag = GameObject.FindGameObjectWithTag(tag);
                toFind = objectWithTag?.GetComponent<T>();
                // Možná v budoucnu uděláme přes arraye, namísto Find => načte všechny T ve scéně a potom, najdi s tagem v poli, ale to chece test, co je náročnější. TODO: optimalizace
            }
        }
        else // Jinak se vrať
        {
            return toFind;
        }

        // Kontrola pokud proběhlo vyhledávání
        if (toFind == null)
        {

            Debug.LogError("Nebyl (v kódu: " + codeName + ") přiřazen objekt: " + tag);
        }

        return toFind;
    }

    /// <summary>
    /// Získá dítě Transform podle TAG
    /// </summary>
    /// <typeparam name="T"> </typeparam>
    /// <param name="findIn">Transform v jejíchž dětech hledáme.</param>
    /// <param name="tag">Tag objektu, který nese hledaný komponent.</param>
    /// <returns></returns>
    public static T FindComponentInChildrenWithTag<T>(Transform findIn, string tag) where T : Component
    {
        T finded_obj = null;

        if (findIn != null)
        {
            T[] childsOf_FindIn = findIn.GetComponentsInChildren<T>();

            foreach (T component in childsOf_FindIn)
            {
                if (component.gameObject.tag.Equals(tag))
                {
                    finded_obj = component;
                }
            }

            // Kontrola pokud proběhlo vyhledávání
            if (finded_obj == null)
                Debug.LogError($" {typeof(T)} Nebyl nalezen komponent v childs!");
        }

        return finded_obj;
    }

    /// <summary>
    /// Získá VŠECHNY dítě Transform podle TAG
    /// </summary>
    /// <typeparam name="T"> </typeparam>
    /// <param name="findIn">Transform v jejíchž dětech hledáme.</param>
    /// <param name="tag">Tag objektu, který nese hledaný komponent.</param>
    /// <returns></returns>
    public static T[] FindComponentsInChildrenWithTag<T>(Transform findIn, string tag) where T : Component
    {
        List<T> finded_objs = new List<T>();

        T[] childsOf_FindIn = findIn.GetComponentsInChildren<T>();

        foreach (T component in childsOf_FindIn)
        {
            if (component.gameObject.tag.Equals(tag))
            {
                finded_objs.Add(component);
            }
            // Debug.Log(component);
        }

        // Kontrola pokud proběhlo vyhledávání
        if (finded_objs == null)
        {

            Debug.LogError("Nebyl nalezen komponent v child");
        }

        if (finded_objs.Count > 0)
        {
            return finded_objs.ToArray();
        }
        else
        {
            return null;
        }
    }

    /// <summary>
    /// Vypne veškeré objekty s Monobehaviour vložené
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="array"></param>
    /// <param name="setTO"></param>
    public static void TurnOffComponent<T>(T[] array, bool setTO) where T : MonoBehaviour
    {
        foreach (T ob in array)
        {
            ob.enabled = setTO;
        }
    }

    /// <summary>
    /// Vrací vector3 s hodnotami 0,1, zda je daná pozice v původním Vector3 obsazená, nebo ne.
    /// Pokud 1 => pozice != 0
    /// </summary>
    /// <param name="vec3">Vecor3, u kterého chceme zjistit obsazení pozic</param>
    /// <returns></returns>
    public static Vector3 GetVector3Population(Vector3 vec3)
    {
        //Vector3 populationVec = new Vector3(0,0,0);

        vec3.x = (float)System.Math.Floor(vec3.x);
        vec3.y = (float)System.Math.Floor(vec3.y);
        vec3.z = (float)System.Math.Floor(vec3.z);

        // Ověříme zda je hodnota 0
        vec3.x = (vec3.x != 0) ? 1 : 0;
        vec3.y = (vec3.y != 0) ? 1 : 0;
        vec3.z = (vec3.z != 0) ? 1 : 0;
        /*if (vec3.y != 0) populationVec.y = 1;
        if (vec3.z != 0) populationVec.z = 1;*/

        // Vrátíme populated vector3
        return vec3;
    }

    /// <summary>
    /// Vrací vector3 s hodnotami 0,1,-1 podle hodnot v původním vector 3.
    /// </summary>
    /// <param name="vec3">Vecor3, u kterého chceme zjistit obsazení pozic</param>
    /// <returns></returns>
    public static Vector3 GetVector3Population_(Vector3 vec3)
    {
        //vec3 = new Vector3(-1.5f,1.5f, -50f);

        Vector3 populationVec = new Vector3(0f, 0f, 0f);
        /*Debug.Log(vec3);
        Debug.Log(populationVec);*/
        // Ověříme zda je hodnota 0
        float _x = (float)System.Math.Floor(vec3.x);
        float _y = (float)System.Math.Floor(vec3.y);
        float _z = (float)System.Math.Floor(vec3.z);

        if (_x > 0f)
        {
            populationVec.x = 1;
        }
        else if (_x < 0f)
        {
            populationVec.x = -1;
        }

        if (_y > 0f)
        {
            populationVec.y = 1;
        }
        else if (_y < 0f)
        {
            populationVec.y = -1;
        }

        if (_z > 0f)
        {
            populationVec.z = 1;
        }
        else if (_z < 0f)
        {
            populationVec.z = -1;
        }

        /*  if (vec3.y > 0f){populationVec.y = 1;}
          else if (vec3.y < 0f){populationVec.y = -1;}

          if (vec3.z > 0f){populationVec.z = 1;}
          else if (vec3.z < 0f){populationVec.z = -1;}*/

        /* populationVec.x = (vec3.x > 0) ? 1 : ((vec3.x < 0) ? -1 : 0);

         populationVec.y = (vec3.y > 0) ? 1 : ((vec3.y < 0) ? -1 : 0);

         populationVec.z = (vec3.z > 0) ? 1 : ((vec3.z < 0) ? -1 : 0);*/

        // Vrátíme populated vector3
        return populationVec;
    }

    // Get Rotated Cordinates
    /// <summary>
    /// Vrací Vector2 souřadnice po rotaci.
    /// </summary>
    /// <param name="vec2">Původní Vecor2 souřadnice.</param>
    /// <param name="degrees">O kolik chceme otáčet. (Ve stupních) </param>
    /// <returns></returns>
    public static Vector2 Rotate2D(Vector2 vec2, float degrees)
    {
        Vector2 vec2_r = new Vector2();

        float sin = Mathf.Sin(degrees * Mathf.Deg2Rad);
        float cos = Mathf.Cos(degrees * Mathf.Deg2Rad);

        float tx = vec2.x;
        float ty = vec2.y;
        vec2_r.x = (cos * tx) - (sin * ty);
        vec2_r.y = (sin * tx) + (cos * ty);

        // Úprava, jelikož raw data z rotace jsou někdy klamavá, 0 != 0 (potom vrátí něco malého s mantisou), apod.
        vec2_r.x = RoundTo(vec2_r.x);// Zaokrouhlíme float na 2 platné
        vec2_r.y = RoundTo(vec2_r.y);

        return vec2_r;
    }

    /* public static Vector3 Rotate3D(Vector3 vec, Vector3 rotation)
     {
           // X


         float sin = Mathf.Sin(degrees * Mathf.Deg2Rad);
         float cos = Mathf.Cos(degrees * Mathf.Deg2Rad);

         float tx = v.x;
         float ty = v.y;
         v.x = (cos * tx) - (sin * ty);
         v.y = (sin * tx) + (cos * ty);

         return v;
     }*/

    /// <summary>
    /// Vrací zaokrouhlenou float na 2 platné číslice.
    /// </summary>
    /// <param name="num"> Float pro zaokrouhlení. </param>
    /// <returns></returns>
    public static float RoundTo(float num, short roundTo = 2)
    {
        num = (float)System.Math.Round(num, roundTo);
        return num;
    }

    public static Dictionary<string, Scene> GetLoadedScenes() {

        Dictionary<string, Scene> loadedScenes = new Dictionary<string, Scene>();

        // Nahrajeme pole všech scén
        int numOfLoadedScenes = SceneManager.sceneCount;
        for (int i = 0; i < numOfLoadedScenes; i++)
        {
            loadedScenes.Add(SceneManager.GetSceneAt(i).name, SceneManager.GetSceneAt(i));
        }

        return loadedScenes;
    }



    /// <summary>Pokusí se loadnout scnénu, dle zadané string. <param name="sceneName"></param><returns></returns></summary>
    public static bool LoadSceneAdditive(string sceneName)
    {
        // Pokud není scéna loadnuta
        if (!Settings.loadedScenes.ContainsKey(sceneName)) {
            if (Application.CanStreamedLevelBeLoaded(sceneName))
            {
                    SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
                    Settings.loadedScenes.Add(sceneName, SceneManager.GetSceneByName(sceneName));
                    return true;
            }
        }

        return false;
    }

    public static BlockChecker LoopCheckersInList_And_ReturnByLambda(Func<BlockChecker, BlockChecker> lambdaAlgorithm, List<BlockChecker> listToSearchIn){
        for (int i = 0; i < listToSearchIn.Count; i++){
            var checker = lambdaAlgorithm(listToSearchIn[i]);
            if (checker != null)
                return checker;
        }
        return null;
    }

    public static void Loop_T_InList_And_DoJob<T>(Action<T> lambdaAlgorithm, List<T> listToSearchIn){
        for (int i = 0; i < listToSearchIn.Count; i++){
            lambdaAlgorithm(listToSearchIn[i]);
        }
    }

    /*public static void LoopCheckersInList_And_Set_To(Action<bool> lambdaAlgorithm, List<BlockChecker> listToSearchIn, bool setTo)
    {
        for (int i = 0; i < listToSearchIn.Count; i++){
             lambdaAlgorithm(setTo); 
        }
    }*/

    #endregion
}
