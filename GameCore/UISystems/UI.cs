using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Assets.Scripts.GameCore.GameModes;

/// <summary>
/// STATE u oken znamená, pokud false => zavři okno, jednoduché ne???
/// </summary>
public static class UI
{
    ///<summary> Prefab UI - políčka pro blok. </summary>
    //public static Image blockLibraryWindow = GraphicsSettings.Instance.mainCanvas;
    public static GameObject block_ui_prew = Resources.Load<GameObject>(Settings.PREFABS_CODE_LOADED + "/BLOCK-UI");
    //public static Settings.Checkers_types LastUsedType = Settings.Checkers_types.BLANK;

    public static List<BlockSO> suitableBlocksOrganised = new List<BlockSO>();  // ORGANIZOVANÝ List se všemi vhodnými bloky pro BUILD
    public static List<BlockSO> suitableBlocks = new List<BlockSO>();

    internal static void BlockLibraryWindowState(bool state = false, Settings.Checkers_types c_type = Settings.Checkers_types.BLANK)
    {

        if (state) // Otevření okna TODO:
        {
            //if (LastUsedType != c_type) 
            suitableBlocks = Helpers.LoadSuitableBlocks(c_type);

            // TODO: Pooling systém better
            // Pokud je vyplý, jinak je něco špatně, nechceme ukazovat "buggy" reloadu knihovny
            if (ScreenUIManager.Instance.buildLibraryWindow.container.gameObject.activeSelf == false)
            {
                // -- OBECNÉ PRO OKNO --

                // Nastavíme název okna
                ScreenUIManager.Instance.buildLibraryWindow.title.text = TextHolder.BUILD_LIBRARY_NAME.ToUpper();
                // Nastavíme tlačítko okna
                PrepareButton(ScreenUIManager.Instance.buildLibraryWindow.closeBtn);
                ScreenUIManager.Instance.buildLibraryWindow.closeBtn.onClick.AddListener(() => GameModesManager.Instance.subModesHandler.StopCurrentSubMode(typeof(BuildSubModePlace)));

                // -- UI KARTY --

                // Pokud existuje instance
                if (ScreenUIManager.Instance.buildLibraryWindow.buildLibraryBlockContainer != null)
                {
                    /*if (LastUsedType != c_type) // TZN. Okno se pouze aktivuje/deaktivuje, ale nereloaduje.
                    {// Proč?? Protože chceme řadit pouze až při změně.

                        Debug.Log("Bylo změněno z : " + LastUsedType + " na " + c_type);
                        LastUsedType = c_type; // Tak zapiš jaká byla.*/

                        foreach (BlockBuildCart b_cart in ScreenUIManager.Instance.allBuildCarts)
                        {
                            b_cart.CartGraphics.gameObject.SetActive(true);

                            // Vymažeme všechny listenery tlačítka
                            Button cart_btn = b_cart.CartGraphics.GetComponent<Button>();
                            PrepareButton(cart_btn);

                        }

                        // Nejdřív zjistíme jestli počet již načtených shoduje/je větší s počtem kolik potřebujeme načíst
                        bool isLenghtSame = ScreenUIManager.Instance.allBuildCarts.Count >= suitableBlocks.Count ? true : false;

                        // Pokud jsou délky stejné (poptávka == nabídka)
                        if (isLenghtSame)
                        {
                            short ctr = 0;
                            foreach (BlockBuildCart b_cart in ScreenUIManager.Instance.allBuildCarts)
                            {
                                // Kolik Karet skutečně potřebujeme??
                                if (ctr < (short)suitableBlocks.Count)
                                {
                                    /*BlockBuildCart standart_cart = null;
                                    if (suitableBlocks[ctr] != LastUsedtype)
                                    {
                                        standart_cart = b_cart;
                                    }*/
                                    // PŘEPÍŠEME KARTY

                                    // Získáme text karty
                                    TextMeshProUGUI cart_name = b_cart.CartGraphics.GetComponentInChildren<TextMeshProUGUI>();

                                    // Přepíšeme
                                    cart_name.text = suitableBlocks[ctr].block_name;

                                    // Získáme obrázek karty
                                    Image cart_img = Settings.FindComponentInChildrenWithTag<Image>(b_cart.CartGraphics.transform, "IMAGE");
      
                                    // Změníme obrázek
                                    cart_img.sprite = suitableBlocks[ctr].UIprew;

                                    // Změníme typ bloku
                                    b_cart.BlockType = suitableBlocks[ctr].blockType;

                                    // POŠLEME DATA PRO BUILD LIBRARY
                                    b_cart.BlockData = suitableBlocks[ctr];
                                    b_cart.CheckerType = c_type;

                                    // Přidáme click funkci tlačítku
                                    Button cart_btn = b_cart.CartGraphics.GetComponent<Button>();

                                    
                                    cart_btn.onClick.AddListener(() => World.Instance.PlaceBlockInWorld(b_cart.BlockType));
                                    cart_btn.onClick.AddListener(() => Helpers.ReorganiseSuitableBlocks(b_cart.BlockData));
                                    
                                }
                                else
                                {
                                    b_cart.CartGraphics.gameObject.SetActive(false);
                                }

                                ctr++;
                            }
                        }

                        else // Jinak ještě musíme přidat potřebný počet cartů... - možná v budoucnu uděláme to
                             // že se vygeneruje nejvyšší možný počet hned na začátku a tohle už nebudemem muset řešit, ale pořád je tohle nejvíce optimalizovaná možnost
                        {
                            // Kolik Karet je tedy potřeba dodat abs((co potřebujeme) - (to co máme))
                            short cartsToAdd = (short)(suitableBlocks.Count - ScreenUIManager.Instance.allBuildCarts.Count);
                            for (short i = 0; i < cartsToAdd; i++)
                            {
                                if (block_ui_prew != null)
                                {
                                    GameObject new_ui_element = GameObject.Instantiate(block_ui_prew);
                                    new_ui_element.transform.SetParent(ScreenUIManager.Instance.buildLibraryWindow.buildLibraryBlockContainer.transform);
                                    new_ui_element.GetComponent<Transform>().localScale = new Vector3(1, 1, 1);

                                    ScreenUIManager.Instance.allBuildCarts.Add(new BlockBuildCart(new_ui_element.transform as RectTransform));
                                }
                                else
                                {
                                    Debug.LogError("V UI.cs, nebyl načten block_ui_prew. X-(");
                                }

                            }

                            // ScreenUIHolder.Instance.allBuildCarts = Settings.FindComponentsInChildrenWithTag<RectTransform>(ScreenUIHolder.Instance.buildLibraryBlockContainer.transform, Settings.UI_TAG_BLOCK_CART);

                            // Pro ověření si znovu načteme všechny karty a znovu checkneme délky 
                            //all_block_carts = Settings.FindComponentsInChildrenWithTag<RectTransform>(ScreenUIHolder.Instance.buildLibraryBlockContainer, Settings.UI_TAG_BLOCK_CART);
                            isLenghtSame = ScreenUIManager.Instance.allBuildCarts.Count >= suitableBlocks.Count ? true : false;

                            // Pokud je vše ok, tak přepisujeme:
                            if (isLenghtSame) {
                                // Prostě projíždíme všechny karty, které vůbec existují(buď byly dodtaečně vytvořeny, nebo...)
                                short ctr = 0; // => žádné omezení ctr
                                foreach (BlockBuildCart b_cart in ScreenUIManager.Instance.allBuildCarts)
                                {
                                    // PŘEPÍŠEME KARTY

                                    // Získáme text karty
                                    TextMeshProUGUI cart_name = b_cart.CartGraphics.GetComponentInChildren<TextMeshProUGUI>();
                                    // Přepíšeme
                                    cart_name.text = suitableBlocks[ctr].block_name;

                                    // Získáme obrázek karty
                                    Image cart_img = Settings.FindComponentInChildrenWithTag<Image>(b_cart.CartGraphics.transform, "IMAGE");
                                    if(cart_img == null)
                                        Debug.LogError("Nebyl nalezen komponent v childs!");

                                // Změníme obrázek
                                if (cart_img != null)
                                        cart_img.sprite = suitableBlocks[ctr].UIprew;

                                    b_cart.BlockType = suitableBlocks[ctr].blockType;
                                    Button cart_btn = b_cart.CartGraphics.GetComponent<Button>();

                                    // POŠLEME DATA PRO BUILD LIBRARY
                                    b_cart.BlockData = suitableBlocks[ctr];
                                    b_cart.CheckerType = c_type;

                                    // Přidáme click funkci
                                    cart_btn.onClick.AddListener(() => World.Instance.PlaceBlockInWorld(b_cart.BlockType));
                                    cart_btn.onClick.AddListener(() => Helpers.ReorganiseSuitableBlocks(b_cart.BlockData));

                                    //Přidáme zvuk
                                    AddButtonSound(cart_btn);

                                    ctr++;
                                }
                            }
                            else
                            {
                                Debug.LogError("Nebylo možné načíst data do build knihovny(UI.cs) X-|");
                            }
                        }
                   // }

                    // Možná hodíme ještě nějaké načítání(kolečko uprostřed monitoru na scéně, nebo tak něco) - TODO:
                    ScreenUIManager.Instance.buildLibraryWindow.container.gameObject.SetActive(true);
                    /*
                     * Pozn.
                     * Proč tohle řešení(proč lepší?)?? -> Podle mě nejúspornější, proč?
                     * Nemusí se o nic starat GarbageCollector(nic se neničí jen vypíná), vytváříme toho jen tolik co bylo zatím potřeba. => šetří výkon + memory(s create by to bylo cca o půl víc, ale i tak...)
                     * Navíc v buildu budeme potřebovat butny a ty nalezneme v již vytvořeném array, a nebudeme ty karty muset znovu hledat apod. => šetří výkon
                     */
                }
                else{
                    Debug.LogError("Před otevřením knihovny nebyl načten (v ScreenUIHolder.cs), container s bloky! X-|");
                    return;}
            }else {
                Debug.LogError("Knihovna nebyla před otevřením zavřená X-|");
                return; }

        }
        else
        {
            // Zavření okna
            if(ScreenUIManager.Instance?.buildLibraryWindow != null)
                ScreenUIManager.Instance.buildLibraryWindow.container.gameObject.SetActive(false);

            /*if (Settings.isBuildMode && !ScreenUIManager.Instance.AskDialogWindow.askDialogueContainer.gameObject.activeInHierarchy)
            {
                //WorldBuilder.Instance.TerminateBuildMode();
            }*/
        }
    }

    // -- 1. FOCUS tzn. statistiky bloku na který se kliklo
    internal static void BlockDetailWindowState(bool state, SymetricBlock b_data = null)
    {
        if (state)
        {
            if (b_data == null) { Debug.LogError("BLOCK WINDOW STATE (UI)- data bloku jsou = null"); }

            // -- Zapneme okno bloku --
            ScreenUIManager.Instance?.blockDetailWindow.container?.gameObject.SetActive(true);
            // Removelisteners + zvuk
            PrepareButton(ScreenUIManager.Instance?.blockDetailWindow.editBtn);
            PrepareButton(ScreenUIManager.Instance?.blockDetailWindow.closeBtn);
            ScreenUIManager.Instance.blockDetailWindow.editBtnText.text = TextHolder.EDIT_BTN;
            ScreenUIManager.Instance.blockDetailWindow.closeBtnText.text = TextHolder.CLOSE_BTN;

            // Nastavíme vypnutí okna
            ScreenUIManager.Instance?.blockDetailWindow.closeBtn.onClick.AddListener( () => BlockDetailWindowState(false));

            //  -- Načteme data --
            // Titulek
            ScreenUIManager.Instance.blockDetailWindow.title.text = b_data.BlockName.ToUpper();

            // -- Spustíme dodatečné UI funkce --
        }
        else
        {
            // Vypnutí okna, b_data = null

            ScreenUIManager.Instance.blockDetailWindow.container.gameObject.SetActive(false);
        }
    }

    // Maybe (Byl by FOCUS 2.)
    internal static void CheckerWindowState(BlockSO b_data = null)
    {
    }

    /// <summary> Gizmos při stavbě - šipky rotace apod. </summary> <param name="b_pos"></param> <param name="state"></param>
    internal static void BlockBuildGizmosState(bool state, SymetricBlock b_data = null)
    {
        if (state)
        {
            if (b_data.BlockPosition == null) { Debug.LogError("BLOCK BUILD GIZMO STATE (UI.cs)- b_pos je = null"); }
            // -- Hlavní okno --

            // Získáme referenci okna
            ScreenUIManager.Instance.gizmos.container.gameObject.SetActive(true); // Zapneme

            // Nastavíme pozici
            ScreenUIManager.Instance.gizmos.controller.positionToCalculateOn = new Vector3(b_data.BlockPosition.x, b_data.BlockPosition.y + Settings.uiRotateGizmosHeight, b_data.BlockPosition.z);

            // -- Tlačítka --

            // Raději odstraníme veškeré exitující listnery (TODO: Kdyžka ověření, pokud se bude okno zobrazovat i jinde nějaký bool)
            PrepareButton(ScreenUIManager.Instance.gizmos.rotateLBtn);// V LEVO
            PrepareButton(ScreenUIManager.Instance.gizmos.rotateRBtn); // V PRAVO

            BuildSubModePlace buildSubMode = null;
            if (GameModesManager.Instance.subModesHandler.CurrentSubMode is BuildSubModePlace)
                buildSubMode = GameModesManager.Instance.subModesHandler.CurrentSubMode as BuildSubModePlace;

            if (buildSubMode != null) {
                ScreenUIManager.Instance.gizmos.rotateLBtn.onClick.AddListener( 
                    () => buildSubMode.RotateBlockBeforePlace(b_data, new Vector3(0, 90, 0))); // V LEVO
                ScreenUIManager.Instance.gizmos.rotateRBtn.onClick.AddListener(
                    () => buildSubMode.RotateBlockBeforePlace(b_data, new Vector3(0, -90, 0))); // V PRAVO
            }

        }
        else
        {
            // Zavření okna
            ScreenUIManager.Instance.gizmos.container.gameObject.SetActive(false);
        }
    }

    internal static void BuildModeElementsState(bool state){
        ScreenUIManager.Instance.buildMode.container.gameObject.SetActive(state);
        ScreenUIManager.Instance.buildMode.top_title.text = TextHolder.BUILDMODE_TITLE;
    }


    /// <summary> Y/N jestli blok bude Placnut </summary><param name="b_pos"></param> <param name="state"></param>
    /*internal static void BlockBuildAskDialogueState(bool state, Vector3 b_pos = new Vector3())
    {

    }*/



    // SCREEN STATIC UI

    // Tady inicializujeme statické listnery, které se nebudou během hry měnit
    public static void InitializeScreenStaticUI(){
        // show grid
       // ScreenUIHolder.Instance.leftPanel.enableGrid.onValueChanged.AddListener(delegate { GizmosInGame.SetGridGame(); });

        // Tlačítko zapnutí build-modu TODO:
        //ScreenUIManager.Instance.bottomPanel.buildModeOn.onClick.AddListener(() => World.Instance.TurnBuildModeOn());
    }

   
    internal static void ScreenStaticLeftPanelState(bool state = false) {
       
        /*if (state)
        {
            ScreenUIHolder.Instance.leftPanel.mainContainer.gameObject.SetActive(true);

        }
        else
        {
            ScreenUIHolder.Instance.leftPanel.mainContainer.gameObject.SetActive(false);
        }*/
    }


    #region HIGHLIGHTY/EVENTY
    public static void GameScreenEvents(Settings.GameScreenEvents eventType)
    {
        if (eventType == Settings.GameScreenEvents.NOT_ABLE_TO)
        {

            var simple_text_obj = ScreenUIManager.Instance.UIPoolList.GetFromPool(Settings.PoolTypes.UI_SIMPLE_MESSAGE_TEXT, new Vector3(0, 0, 0), Quaternion.identity, ScreenUIManager.Instance.mainScreenStaticCanvas.transform);
            TextMeshProUGUI simple_text = simple_text_obj.GetComponent<TextMeshProUGUI>();

            if (simple_text != null)
            {
                // Kontrolér 
                SimpleMessageController simple_text_controller = simple_text.GetComponent<SimpleMessageController>();

                simple_text_controller.move = true;
                simple_text_controller.translateSpeed = new Vector3(0, 0.5f, 0f);
                simple_text_controller.distanceFromEdges = 50f;

                // Nastavíme pozici
                simple_text.GetComponent<RectTransform>().localPosition = new Vector3(0,0,0);

                // Zapneme blok
                simple_text.gameObject.SetActive(true);


                // Nastavíme message
                simple_text.text = TextHolder.NOT_ABLE_TO_PLACE_BLOCK;
                // Nastavíme barvu 
                simple_text.color = ColorDecryptor.GetColorFromString(Settings.COLOR_RED_WARNING);
                ScreenUIManager.Instance.FadeTextColor(simple_text, 1, 2f, 1f);
            }

            ScreenUIManager.Instance.UIPoolList.ReturnToPool(Settings.PoolTypes.UI_SIMPLE_MESSAGE_TEXT, simple_text_obj);


        }
    }

    // Nastavení highlightů na obrazovce - rámeček okolo obrazovky např. buildmode
    internal static void ScreenHighlightState(bool state = false, Settings.ScreenHighlights type = Settings.ScreenHighlights.NONE)
    {
        if (ScreenUIManager.Instance.highlightMaskComponent == null)
        {
            Debug.LogError("U screen highlightu neexistuje refrence pro masku");
            return;
        }

        // Pokud se má zapnout
        if (state)
        {
            ScreenUIManager.Instance.highlightMaskComponent.gameObject.SetActive(true);
            if (type != Settings.ScreenHighlights.NONE) // Pokud chceme něco dohledat
                foreach (ScreenUIManager.ScreenHighlightMasks highlight in ScreenUIManager.Instance.screenHighlights)
                {
                    if (highlight.type == type)
                    {
                        ScreenUIManager.Instance.highlightMaskComponent.sprite = highlight.mask; // Nastavíme danou masku
                        if (ScreenUIManager.Instance.screenHighlightComponent != null)
                            ScreenUIManager.Instance.screenHighlightComponent.sprite = highlight.highlight; // Nastavíme dané zvýraznění
                    }
                }
        }
        else
        {
            // Odstraníme zvýraznění
            ScreenUIManager.Instance.highlightMaskComponent.gameObject.SetActive(false);
        }

    }
    #endregion

    // WORLD UI
    //-- Až něco bude tak sem :-)

    // OSTATNÍ FUNKCE


    static void ArrowButtonSwitch(Button[] buttons)
    {

    }

    /// <summary>
    /// // Removelisteners + zvuk
    /// </summary>
    /// <param name="btn"> Button. </param>
    public static void PrepareButton(Button btn)
    {
        // Odstraníme listenery
        btn.onClick.RemoveAllListeners();

        // Přidáme zvuk
        AddButtonSound(btn);
    }

    // Obecné funkce tlačítek
    static void AddButtonSound(Button btn)
    {

        btn.onClick.AddListener(delegate { AudioManager.Instance.Play(Settings.SoundEffects.mouse_click_1); });
    }




}
