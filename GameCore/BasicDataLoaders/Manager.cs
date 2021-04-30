using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using RTS_Cam;
using System.Linq;
using System.IO;
//using Newtonsoft.Json;
using Assets.Scripts.ManagerLoad;


public class Manager : Singleton<Manager>
{
    public bool isNewWorld = true;

    public Vector3 worldPosition = new Vector3(0,0,0); // Pozor je v on enabled nastavený na 0,0,0
    AudioManager audioManager;

    [Header ("Kamera")]
    public Camera mainCamera;
    public CameraController cameraController;

    [Header("Grid")]
    public GameObject grid;
    

    public bool testFinderPath;
    public Vector3 start;
    public Vector3 end;

    // Nastavení theme barev hry
    /* [Header("Barvy kontrolérů")]
     [Tooltip("Barvy, které určují barvy jednotlivých typů kontrolérů.")]*/
    //public List<Color> themeModulColors = new List<Color>();

    // Highlight material
    [Header("Materiál zvýraznění")]
    [Tooltip("Materíl, který se bude aplikovat na objekty při zvýraznění.")]
    public Material highlightMaterial;

    public InGamehighlights[] inGamehighlights;

    [System.Serializable]
    public struct InGamehighlights
    {
        [Header("Barva zvýraznění")]
        public Color highlight_color;
        [Header("Typ zvýraznění")]
        public Settings.GameHighlights highlight_type;
    }

    [Header("Objekty z poolu.")]
    public GeneralObjectPool objectsToPool; // Všechny pool objekty, včetně ui

    private void OnEnable(){
         worldPosition = new Vector3(0, 0, 0);
    }

    /*
     Execute order!!!
     IMPORATNT - tohle jako první kód vůbec!!!!
    */
    void Awake()
    {
        #region -- LOAD DAT
        // Data z Resources
        PoolObjectSO[] poole_object_types = Resources.LoadAll<PoolObjectSO>(Settings.POOL_OBJECTS_PATH);

        // Načteme soubory z resources
        highlightMaterial = Resources.Load<Material>(Settings.MATERIALS + "/" + Settings.HIGHLIGHT_MAT);



        // Načteme si objekty do poolu
        objectsToPool = new GeneralObjectPool(poole_object_types.OfType<PoolObjectSO>().ToList());
        // Inicializujeme pool objekty
        objectsToPool.InitializeObjectsToPool();

        #endregion

        #region -- LOAD KAMERY
        Settings.isGameLoaded = ManagerLoad.LoadCamera();
        if (Settings.loadedScenes == null) { Debug.LogError("Nepodařilo se načíst hlavní kameru."); }

       // Debug.Log(Settings.isGameLoaded);
        #endregion

        #region  -- LOAD SCÉN

        Settings.loadedScenes = Settings.GetLoadedScenes(); // TODO: Podle mě je to zbytečné
        if (Settings.loadedScenes == null) { Debug.LogError("Nepodařilo se získat načtené scény."); }

        /*
         * 1. Loadneme UI
        */
        Settings.isGameLoaded = ManagerLoad.LoadScreenUIScene();
        if (!Settings.isGameLoaded) { Debug.LogError("Nepodařilo se načíst scrren UI scénu."); }

        /*
         * 2. Vytvoříme WorldBuilder(pokud neni)
         */
        Settings.isGameLoaded = ManagerLoad.LoadWorldBuilder();
        if (!Settings.isGameLoaded) { Debug.LogError("Nepodařilo se načíst WorldBuilder."); }
        #endregion

        #region -- LOAD GIZMOS
        // Grida
        Settings.isGameLoaded = ManagerLoad.LoadGizmos();
        #endregion

    }

    // Start is called before the first frame update
    void Start() {

        #region -- ODSTRAŇĚNÍ NECHTĚNÝCH PRVKŮ

        // Ostatní kamery
        Settings.isGameLoaded = ManagerLoad.RemoveOtherCameras();
        if (!Settings.isGameLoaded) { Debug.LogError("Cam system fail (Nepodařilo se odstranit ostatní kamery.)"); }
        #endregion


        // Inicializace UI statických tlačítek.
        UI.InitializeScreenStaticUI(); // Pozn. Je to až po loadu celé scény, protože se snažíme zabránit tomu callu Mnageru, nebo WorldBuilderu - singletonů 
    }

    List<Node> pathPoints = null;


    // Update is called once per frame
    void Update()// JEN pro debug :-(
    {
        /* PathFinder.Instance.GeneratePath();
         PathFinder.Instance.LoadAllNodeData();*/
       /* if (Input.GetKeyDown(KeyCode.L))
        {
            Block a = new Block();
            //ES3.LoadInto<Block>("blocks", a);
            Debug.Log(a);
        }*/

           // if (Input.GetKeyDown(KeyCode.Space)) {
           // SaveClass save = new SaveClass();
           // save.SaveGameData();
           //
           // //GameObject a = ES3.Load<GameObject>("block");
           // //Debug.Log(a.transform.position);
           // if (pathPoints != null) {
           //     pathPoints = PathFinder.Instance.GetPath(PathFinder.Instance.nodes_data.Find(o => o.position == start), //PathFinder.Instance.nodes_data.Find(o => o.position == end));
           //
           //     foreach (Node pPoint in pathPoints) {
           //         Debug.Log(pPoint.position);
           //     }
           //
           // }
           //
           // }
     //   if (testFinderPath)
     //   PathFinder.Instance.GeneratePath();
     //  PathFinder.Instance.LoadAllNodeData();
     // }

            // Budeme hlídat pozici gridu
            //  grid.transform.position = new Vector3(mainCamera.transform.position.x, worldPosition.y, mainCamera.transform.position.z);
    }

}
