using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.UI;
using TMPro;
using System.Linq;

public class ScreenUIManager : Singleton<ScreenUIManager>
{
    // SCREEN -FLOAT

    [Header("Hlavní Screen Canvas")]
    public Canvas mainScreenCanvas;

    [Header("Build window")]
    public BuildLibraryWindow buildLibraryWindow;
    public List<BlockBuildCard> allBuildCards = new List<BlockBuildCard>();
    public RectTransform timersHolder;
    public RectTransform simpleMessageHolder;

    [System.Serializable]
    public struct BuildLibraryWindow{
        ///<summary> Hlavní Container </summary>
        public RectTransform container;
        public RectTransform buildLibraryBlockContainer;
        public TextMeshProUGUI title;
        public Button closeBtn;

    }

    [Header("Build mode")] // BUILDMODE BUDE SMĚSKA STATIKU(panely na stranách obrazkovky) A FLOATU(okna)!!!!

    [Tooltip("UI STATICKÁ Okna a elementy(tzn. ne build window, atd.), které se aktivují při spuštění buildu.")] //TODO: Možná v budoucnu přidat i buildwindow 
    public BuildModeElements buildMode; 
    [System.Serializable]
    public struct BuildModeElements{ // TODO INICIALIZACE!!!!!!
        public RectTransform container;
        public TextMeshProUGUI top_title;
        public Button exit_build_mode_btn; // TODO:  ANIMACE adt...
    }


        [Header("Ask Dialogue")]
   // public AskDialogWindow askDialogWindow;

    public AskDialogueWindowController AskDialogWindowController;

    //  [System.Serializable]
    /*public struct AskDialogWindow{
        public RectTransform askDialogueContainer; // Y/N dialogue
        public AskDialogueWindowController controller;
        public TextMeshProUGUI message;
        public Button acceptBtn;
        public Button rejectBtn;
    }*/

    [Header("UI Gizmos")]
    public Gizmos gizmos;
    [System.Serializable]
    public struct Gizmos{
        public RectTransform container;
        public UIGizmosController controller;
        public RectTransform rotateGizmos;
        public Button rotateLBtn;
        public Button rotateRBtn;

    }


    [Header("Block Detail Window")]
    public BlockDetailWindow blockDetailWindow;
    [System.Serializable]
    public struct BlockDetailWindow{
        public RectTransform container; // Y/N 
        public Button closeBtn;
        public TextMeshProUGUI closeBtnText;
        public Button editBtn;
        public TextMeshProUGUI editBtnText;
        public TextMeshProUGUI title;
        public Image healthBar;
        public Image powerBar;
        public Image shieldBar;
    }


    // SCREEN - STATIC

    [Header("Hlavní Screen static Canvas")]
    public Canvas mainScreenStaticCanvas;

    [Header("Static - Left Panel")]
   /* public LeftPanel leftPanel;

    [System.Serializable]
    public struct LeftPanel{
        public RectTransform mainContainer;
        public Toggle enableGrid; // TODO: vlastní netovací okno (zatím asi)
    }*/

    [Header("Static - Bottom Panel")]
    public BottomPanel bottomPanel;
    [System.Serializable]
    public struct BottomPanel
    {
        public RectTransform mainContainer;
        public Button destroyBlockBtn; // todo. buildmode static UI
        public Button buildModeOn;
    }

    // WORLD
    [Header("Hlavní World Canvas")]
    public Canvas mainWorldCanvas;


    [Header("Array UI objektů pro pool.")]
    public GeneralObjectPool UIPoolList;


    [Header("Zvýraznění na obrazovce.")]
    //public Image defaultHighlightMask;
    public Image highlightMaskComponent;
    public Image screenHighlightComponent;

    public ScreenHighlightMasks[] screenHighlights;
    [System.Serializable]
    public class ScreenHighlightMasks
    {
        [Header("Černobílá maska vyříznutí.")]
        [Tooltip("Pravděpodobně defaultní")]
        public Sprite mask;
        public Sprite highlight;
        public Settings.ScreenHighlights type;
    }

    public TextMeshProUGUI fadingText;


    void Start()
    {
        //Canvas UI objektů na SCREEN
        // 2 Šance zda se najde UI věc, pokud ani jedno, tak..., tak ERROR typu => X-|
        // Nemělo by dojít k searchi, vše ručně ve scéně, ale kdyby náhodou...
        mainScreenCanvas = Settings.IsComponentLoaded<Canvas>(mainScreenCanvas, Settings.UI_TAG_SCREEN_CANVAS, "ScreenUICanvas");
        if (timersHolder == null)
        {
            timersHolder = mainScreenCanvas.transform as RectTransform;
            Debug.LogError("timersHolder byl defaultně přiřazen canvas!!!!"); // Pozn. Nechtělo se mi přidávat další zbytečný tag na dohledání...
        }

        
        #region ZVÝRAZNĚNÍ - OBRAZOVKA

        //Highlighty
        if (highlightMaskComponent == null)
        {
            highlightMaskComponent = GameObject.FindGameObjectWithTag(Settings.UI_TAG_SCREEN_HIGHLIGHT).GetComponent<Image>();
            Debug.LogError("highlightMask component(reference) se musel dohledat - zbytečné žraní výkonu(ScreeUIHolder)"); // TODO: v budoucnu udělat highlight tak, aby se nemuselo maskovat
        }

        if (highlightMaskComponent != null)
        {
            UI.ScreenHighlightState(true); // Jen Kvůli inicializaci ostatních komponentů
            if(screenHighlightComponent == null)
                screenHighlightComponent = Settings.FindComponentInChildrenWithTag<Image>(highlightMaskComponent.transform, Settings.UI_TAG_IMAGE);
        }
        else { Debug.LogError("highlightMask component(reference) se nenašel (ScreeUIHolder)");  }

        // Dafultně zvýraznění vypneme
        UI.ScreenHighlightState(false);
        #endregion


        // Canvas hlavní statické okno
        mainScreenStaticCanvas = Settings.IsComponentLoaded<Canvas>(mainScreenStaticCanvas, Settings.UI_TAG_STATIC_SCREEN, "StaticScreenUI");


        // -- Statické UI panely
        if (mainScreenStaticCanvas != null)
        {
            // Levý panel
          //  leftPanel.mainContainer = Settings.FindComponentInChildrenWithTag<RectTransform>(mainScreenStaticCanvas.transform, Settings.UI_TAG_STATIC_LEFT_PANEL);
            // Spodní panel
            bottomPanel.mainContainer = Settings.FindComponentInChildrenWithTag<RectTransform>(mainScreenStaticCanvas.transform, Settings.UI_TAG_STATIC_BOTTOM_PANEL);
        }


        //Canvas UI objektů ve WORLD
        mainWorldCanvas = Settings.IsComponentLoaded<Canvas>(mainWorldCanvas, Settings.UI_TAG_WORLD_CANVAS, "WorldUICanvas");

        #region STATICKÉ UI - JEDNOTLIVÉ OBJEKTY

        // -- Objekty levého panelu
        /* if (leftPanel.mainContainer != null){
             if(leftPanel.enableGrid == null)
                 leftPanel.enableGrid = Settings.FindComponentInChildrenWithTag<Toggle>(leftPanel.mainContainer, Settings.UI_TAG_TOGGLE);
         }*/

        // -- Objekty spodního panelu
        if (bottomPanel.mainContainer != null)
        {
            if (bottomPanel.destroyBlockBtn == null) // DEstrukce bloků todo: buildmode static UI možná?
                Debug.LogError("NEBYLO NALEZENO TLAČÍTKO DESTRUKCE BLOKU (LEVÝ PANEL SCREEN_UI_HOLDER.cs)");

            if (bottomPanel.buildModeOn == null) // Builed mode ON tlačítko
                Debug.LogError("NEBYLO NALEZENO TLAČÍTKO ZAPNUTÍ BUILDMODU (LEVÝ PANEL SCREEN_UI_HOLDER.cs)");
        }

        // Inicializujeme tlačítka, image apod. statických panelů
       // UI.InitializeScreenStaticUI();
        #endregion


        //TODO: Možná zrušit - (je to takové zbytečně zamotané)
        #region ČÁSTEČNÉ UI(dynamické..) // Např. build okna/tlačítka

        if (buildMode.container != null){
            buildMode.container.gameObject.SetActive(true);

            if (buildMode.top_title != null)
                buildMode.top_title.text = TextHolder.BUILDMODE_TITLE;
            else
                Debug.LogError("NEBYLO NALEZENO HLAVNÍ OKNO BUILDMODU SCREEN_UI_HOLDER.cs)");

            buildMode.container.gameObject.SetActive(false);
        }
        else{
            Debug.LogError("NEBYLO NALEZENO HLAVNÍ OKNO BUILDMODU SCREEN_UI_HOLDER.cs)");
        }

        
        #endregion

        // -- Float bloky
        #region KNIHOVNA BLOKŮ

        // Build window
        buildLibraryWindow.container = Settings.IsComponentLoaded<RectTransform>(buildLibraryWindow.container, Settings.UI_TAG_BUILD_LIBRARY, "ScreenUIHolder");
        buildLibraryWindow.container.gameObject.SetActive(true);

        if (buildLibraryWindow.container != null)
        {
            //Najdeme buildWindow dítě, podle tagu  
            if(buildLibraryWindow.buildLibraryBlockContainer == null) // KARTY
                buildLibraryWindow.buildLibraryBlockContainer = Settings.FindComponentInChildrenWithTag<RectTransform>(buildLibraryWindow.container, Settings.UI_TAG_SCROLL_WINDOW);
            if (buildLibraryWindow.title == null) // TITLE
                buildLibraryWindow.title = Settings.FindComponentInChildrenWithTag<TextMeshProUGUI>(buildLibraryWindow.container, Settings.UI_TAG_TITLE);
            if (buildLibraryWindow.closeBtn == null) // CLOSE BTN
                buildLibraryWindow.closeBtn = Settings.FindComponentInChildrenWithTag<Button>(buildLibraryWindow.container, Settings.UI_TAG_BT_CLOSE);

            // Vypneme knihovnu (Debug: true) - Tady natvrdo
            UI.BlockLibraryWindowState(false);
        }

        #endregion


        #region OKNO DETAILU BLOKU

        // Okno detailu bloku
        blockDetailWindow.container = Settings.IsComponentLoaded<RectTransform>(blockDetailWindow.container, Settings.UI_TAG_DETAIL_WINDOW, "Block detail window");

        if (blockDetailWindow.container != null)
        {
            // Při loadu zapneme
            blockDetailWindow.container.gameObject.SetActive(true);

                            
            if(blockDetailWindow.closeBtn == null) // CLOSE BTN
                blockDetailWindow.closeBtn = Settings.FindComponentInChildrenWithTag<Button>(blockDetailWindow.container, Settings.UI_TAG_BT_CLOSE);
            if (blockDetailWindow.editBtn == null) // BUILD BTN
                blockDetailWindow.editBtn = Settings.FindComponentInChildrenWithTag<Button>(blockDetailWindow.container, Settings.UI_TAG_BT_TRUE);
            if (blockDetailWindow.title == null) // TITLE
                blockDetailWindow.title = Settings.FindComponentInChildrenWithTag<TextMeshProUGUI>(blockDetailWindow.container, Settings.UI_TAG_TITLE);

            if (blockDetailWindow.editBtn != null){
                if (blockDetailWindow.editBtnText == null)
                    blockDetailWindow.editBtnText = Settings.FindComponentInChildrenWithTag<TextMeshProUGUI>(blockDetailWindow.editBtn.transform, Settings.UI_TAG_TEXT);
            }

            if (blockDetailWindow.closeBtnText != null){
                if (blockDetailWindow.closeBtnText == null)
                    blockDetailWindow.closeBtnText = Settings.FindComponentInChildrenWithTag<TextMeshProUGUI>(blockDetailWindow.closeBtn.transform, Settings.UI_TAG_TEXT);
            }



            /*  // Bar HEALTH TODO:
                blockDetailWindow.healthBar = Settings.FindComponentInChildrenWithTag<Image>(blockDetailWindow.container, Settings.UI_SCEEN_NAME);*/ // Ba                                                                                                                                        blockDetailWindow.healthBar = Settings.FindComponentInChildrenWithTag<Image>(blockDetailWindow.container, Settings.UI_TAG_BT_TRUE);*/

            // zavřeme - Tady natvrdo
            UI.BlockDetailWindowState(false); // Vypneme block window
        }

        #endregion

        // -- Jednoduchá okna --

        #region Y/N OKNO
        // Najdeme Y/N Dialog
      //  askDialogWindow.askDialogueContainer = Settings.IsComponentLoaded<RectTransform>(askDialogWindow.askDialogueContainer, Settings.UI_TAG_ASK_DIALOGUE, "AskDialogue");
      //  if (askDialogWindow.askDialogueContainer != null)
      //  {
      //          // Při loadu zapneme
      //          askDialogWindow.askDialogueContainer.gameObject.SetActive(true);
      //
      //          if (askDialogWindow.acceptBtn == null) // ACCEPT BUTTON
      //              askDialogWindow.acceptBtn = Settings.FindComponentInChildrenWithTag<Button>(askDialogWindow.askDialogueContainer, Settings.UI_TAG_BT_TRUE); // Accept_btn
      //          if (askDialogWindow.rejectBtn == null) // REJECT BUTTON
      //              askDialogWindow.rejectBtn = Settings.FindComponentInChildrenWithTag<Button>(askDialogWindow.askDialogueContainer, Settings.UI_TAG_BT_FALSE); // Reject_btn
      //          if (askDialogWindow.message == null) // MESSAGE
      //              askDialogWindow.message = Settings.FindComponentInChildrenWithTag<TextMeshProUGUI>(askDialogWindow.askDialogueContainer, Settings.UI_TAG_TITLE); // Reject_btn
      //
      //          // Kontrolér   
      //          if (askDialogWindow.controller == null)
      //          {
      //              askDialogWindow.controller = askDialogWindow.askDialogueContainer.GetComponent<AskDialogueWindowController>();
      //
      //              if (askDialogWindow.controller == null)
      //                  askDialogWindow.controller = askDialogWindow.askDialogueContainer.gameObject.AddComponent<AskDialogueWindowController>();
      //          }
      //
      //           UI.BlockBuildAskDialogueState(false);
      //  }

        #endregion

        #region GIZMOS

        gizmos.container = Settings.IsComponentLoaded<RectTransform>(gizmos.container, Settings.UI_TAG_ASK_DIALOGUE, "AskDialogue");
        gizmos.container.gameObject.SetActive(true);
        if (gizmos.container != null)
        {
            if(gizmos.rotateGizmos == null)
                Debug.Log("Nebyl přiřazen riotační UI gizmos");//gizmos.rotateGizmos = 
            if (gizmos.rotateLBtn == null)
                Debug.Log("Nebyl nalezen UI gizmos");
            if (gizmos.rotateRBtn == null)
                Debug.Log("Nebyl nalezen UI gizmos");

            // Kontrolér   
            if (gizmos.controller == null)
            {
                gizmos.controller = gizmos.container.GetComponent<UIGizmosController>();

                if (gizmos.controller == null)
                    gizmos.controller = gizmos.container.gameObject.AddComponent<UIGizmosController>();
            }
        }
        UI.BlockBuildGizmosState(false);

        #endregion

        #region Ostatní
        #region UIPOOL

        // Inicializace UI pool 
        // Data s Resources
        PoolObjectSO[] poole_UI_object_types = Resources.LoadAll<PoolObjectSO>(Settings.POOL_UI_OBJECTS_PATH);

        // Načteme si objekty do poolu
        UIPoolList = new GeneralObjectPool(poole_UI_object_types.OfType<PoolObjectSO>().ToList());
        UIPoolList.InitializeObjectsToPool();

        #endregion
        #endregion
    }

    #region UI Coroutines

    /// <summary>Funkce pro fading textu.</summary> <param name = "textToFade" >Typu TextMeshProUGUI</param><param name="direction">Pokud 1, tak fadeout.</param><param name = "delay" >Zpoždění před startem fadu.</param ><param name="fadeStep">(Jaké n v: 1-n) při každém kroku.</param> <param name="fadeSpeed">Čas mezi kroky</param>
    public void FadeTextColor(TextMeshProUGUI textToFade, short direction = 1, float delay = 0f, float fadeSpeed = 10f)
    {StartCoroutine(FadeTextColorTimer(textToFade, new Color(), false, direction, delay, fadeSpeed));}

    /// <summary>Funkce pro fading textu.</summary> <param name = "textToFade" >Typu TextMeshProUGUI</param><param name="direction">Pokud 1, tak fadeout.</param><param name = "delay" >Zpoždění před startem fadu.</param ><param name="fadeStep">(Jaké n v: 1-n) při každém kroku.</param> <param name="fadeSpeed">Čas mezi kroky</param>
    public void FadeTextColor(TextMeshProUGUI textToFade, Color fadeTO,short direction = 1, float delay = 0f, float fadeSpeed = 10f)
    {StartCoroutine(FadeTextColorTimer(textToFade, fadeTO, false,direction, delay, fadeSpeed));}

    public void FadeTextColor(TextMeshProUGUI textToFade, Color fadeTO, bool turnOnOffAfterFade, short direction = 1, float delay = 0f, float fadeSpeed = 10f)
    { StartCoroutine(FadeTextColorTimer(textToFade, fadeTO, turnOnOffAfterFade, direction, delay, fadeSpeed)); }

    public void FadeTextColor(TextMeshProUGUI textToFade, bool turnOnOffAfterFade, short direction = 1, float delay = 0f, float fadeSpeed = 10f)
    { StartCoroutine(FadeTextColorTimer(textToFade, new Color(), turnOnOffAfterFade, direction, delay, fadeSpeed)); }

    public IEnumerator FadeTextColorTimer(TextMeshProUGUI textToFade, Color fadeTO, bool turnOnOffAfterFade, short direction = 1, float delay = 0f, float fadeSpeed = 10f)
    {
        textToFade.color = new Color(textToFade.color.r, textToFade.color.g, textToFade.color.b, 1);

        while (delay > 0.0f)
        {
            delay -= 0.1f;
            yield return null;
        }
        
        if (delay <= 0.0f)
        while (textToFade.color.a > 0.0f)
        {
            if (fadeTO != new Color())
            {
                textToFade.color = Color.Lerp(textToFade.color, new Color(fadeTO.r, fadeTO.g, fadeTO.b, textToFade.color.a), 1 - textToFade.color.a);
            }
            textToFade.color = new Color(textToFade.color.r, textToFade.color.g, textToFade.color.b, textToFade.color.a - (Time.deltaTime / fadeSpeed));

            if (textToFade.color.a <= 0.2f && turnOnOffAfterFade)
            {
                if (direction == 1)
                    textToFade.gameObject.SetActive(false);
                else
                    textToFade.gameObject.SetActive(true);
            }
                    
            yield return null;
        }
        

        /* if (fadeTO != new Color())
         {
             textToFade.color = Color.Lerp(textToFade.color, new Color(fadeTO.r, fadeTO.g, fadeTO.b, textToFade.color.a), Mathf.Abs(1-textToFade.color.a));
             //Debug.Log(Mathf.Abs(1 - textToFade.color.a));
         }

         if (delay > 0f)
         {
             delay -= fadeStep;
             yield return new WaitForSeconds(fadeSpeed);
             StartCoroutine(FadeTextColorTimer(textToFade, fadeTO, direction, delay));
         }

             Debug.Log(delay);
         if (delay <= 0f)
         {
             textToFade.color = new Color(textToFade.color.r, textToFade.color.g, textToFade.color.b, textToFade.color.a - fadeStep * direction);
         }



         if ((textToFade.color.a > 0 && direction == 1) || (textToFade.color.a < 0 && direction == -1) && delay <= 0)
         {
             yield return new WaitForSeconds(fadeSpeed);
             StartCoroutine(FadeTextColorTimer(textToFade, fadeTO));
         }*/
    }

    #endregion
}
