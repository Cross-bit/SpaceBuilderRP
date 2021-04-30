using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.BlockScripts.BlockAdditional;
using Assets.Scripts.GameCore.API.Extensions;
using System;
using Assets.Scripts.GameCore.BlockScripts.BlockAdditional;
using Assets.Scripts.GameCore.WorldBuilding.BlockLibrary;
using System.Linq;
using Assets.Scripts.GameCore.PathFinding;

public class SymBlock : IBlock, ISymetricBlock
{
    // Je blok placnut - tzn. je pobuildu, ale ještě není blok postavený => roboti, kteří blok vystaví
    public bool isBlockPlaced = false;

    // Je blok konečně dostavěn
    public bool isBlockBuilded = false;

    // Je blok Aktivní??
    public bool isBlockActive = false;

    public bool canRotate { get; private set; } = false;

    // Uchováváme rotaci & pozici bloku
    public Vector3 BlockPosition { get; set; } // TODO: nemusí být je obsaženo v GAMEOBEJCT block !!!
    public Vector3 BlockRotation { get; set; }
    public Vector3 BlockDimensions { get; }

    // Ostatní settings bloku
    private Settings.Blocks_types blockType;

    public string BlockName { get; set; }
    readonly int levelOfDetail;
    readonly int numOfPorts;
    public float TimeToBuild { get; set; }
    //public Settings.Checkers_types blockFunctionality;

    // Funkční proměnné
    readonly bool hasBlockEnergy;
    readonly bool hasBlockAir; // bude composed vzduch, dusík apod. => těžba apod. + syntetyzátory apod.
    readonly bool hasBlockWater;

    // Dává bere?
    readonly int energy = 0;
    readonly int water = 0;
    readonly int air = 0;

    // Pro path finding
    public bool isNode = false;
    private bool hasManualNodes = false;

    // REFERENCE OBJEKTŮ PŘÍMO VE SCÉNĚ 
    public GameObject BlockContainer { get; set; } // empty transform, s dětmi - grafika, checkery, 

    /// <summary> 3D Grafika bloku</summary>
    public GameObject BlocksMainGraphics { get; set; }

    /// <summary> Hlavní Collidery bloku. (Dá se uvažovat jako jeden...) </summary>
    public List<Collider> BlockColliders { get; set; } = new List<Collider>();

    /// <summary> Hlavní světla bloku. (Dá se uvažovat jako jeden...) </summary>
    readonly Light[] mainBlockLights; // TODO: Kontrola světel
    public BlockController blockController;

    // GRAFIKA - Ostatní

    //private BlockGrid blockGrid;

    public Settings.Checkers_types blocksPrimaryFunctionality { get; set; }

    #region Checkers

    public bool isSymetric; // Znamená, že checkery jsou rozmístěny rovnoměrně a jejich vzdálenost od středu je stejná
    public float symConstant;

    public Transform CheckersContainer;

    // Lokální data checkerů objektu TODO: něco provést s Vector3, aby nemusela být pozice public (Problém např. s GetChecker())
    public Vector3[] CheckersPosition; // LOKÁLNÍ POZICE (Pro převod přičti světovou pozici objektu bloku)
    public Settings.Checkers_types[] BlockCheckersTypes;

    /// <summary> Kontrolér na který je daný blok připojený. Resp.kontrolér na který byl blok prvně usazen. </summary>
    public BlockChecker BaseCheckerNextTo { get; set; }

    #endregion

    // PROPERTIES

    public bool IsOrientationValid { get; set; }
    public List<BlockChecker> Checkers { get; set; } = new List<BlockChecker>();

    /// <summary> grida pod blokem (barevný čtverec) </summary>
    private SymBlockGrid _blockGrid { get; set; }

    public bool IsNode { get => isNode; set => isNode = value; }
    public bool HasManualNodes { get => hasManualNodes; set => hasManualNodes = value; }
    public Settings.Blocks_types BlockType { get => blockType; set => blockType = value; }
    public IBlockGrid BlockGrid { get => _blockGrid; set => _blockGrid = (SymBlockGrid)value; }


    //public Block() : this(new Vector3().normalized, Quaternion.identity){}
    public SymBlock(Vector3 blockPosition = new Vector3(), Vector3 rotation = new Vector3(), BlockSO blockData = null, BlockChecker activeChecker = null)
    {
        if (blockData != null){
            // Věci z SO
            this.BlockName = blockData.block_name;
            this.levelOfDetail = blockData.levelOfDetails;
            this.numOfPorts = blockData.mainNumberOfSlots;
            this.BlocksMainGraphics = blockData.mainGraphics;
            this.isSymetric = blockData.isSymetric;
            this.symConstant = blockData.checkersDistance;
            this.BlockRotation = rotation;
            this.TimeToBuild = blockData.buildTime;
            this.BlockDimensions = blockData.blockDimensions;
            this.blocksPrimaryFunctionality = blockData.blockFunctionality;
            this.energy = blockData.energy;
            this.water = blockData.water;
            this.air = blockData.air;
            this.isNode = blockData.isNode;
            this.canRotate = blockData.canRotate;

            // Pouze pokud se délky rovnají !!!
            if (blockData.checkersPositions.Length == blockData.checkersTypes.Length){
                CheckersPosition = blockData.checkersPositions;
                BlockCheckersTypes = blockData.checkersTypes;
            }

            // Věci z JSONU
            blockType = blockData.blockType;
            this.BlockPosition = blockPosition;
            BaseCheckerNextTo = activeChecker;
        }
        else{
            Debug.LogError("Chybí data k načtení!!!(Block.cs... neřešit:-))"); // - TODO: jednou vyřešit :)
        }

    }

    #region RAW TVORBA BLOKU

    public bool ConstructBlock(IBlockConstructor blockConstructor)
    {
        bool constructionState = false;

        if (!(blockConstructor is SymBlockConstructor))
            return false;

        // Block Objekt
        blockConstructor.CreateBlockContainer();
        blockConstructor.CreateBlockGraphicsGameObject();

        blockConstructor.InitializeBlocksColliders();
        blockConstructor.SetBlockFunctionaly();

        // Checkery
        blockConstructor.CreateParentTransformForCheckers();
        blockConstructor.CreateBlockCheckers();
        blockConstructor.InitializeBlocksCheckersGraphics();

        constructionState = true;
        return constructionState;
    }

    public bool ConstructBlockPost(IBlockConstructor blockConstructor) {
        if (!(blockConstructor is SymBlockConstructor))
            return false;

        bool constructionState = false;

        // Ostatní
        blockConstructor.CreateBlockGrid();
        blockConstructor.CreateBlockController();


        constructionState = true;
        return constructionState;
    }


    public void SetBlockOrientation() {
        SetPositionInTheWorldToSymetricBlock();
        SetRotationInTheWorldToSymetricBlock();

        var blockRotator = new SymBlockRotator(
        Quaternion.identity, this);

        blockRotator.RotateOnBuild();
    }

    public bool CheckBlockPlacement() {
        var blockChecker = new BlockPlacementValidator(this, this.BlockRotation);
        return blockChecker.CheckIfBlocksPlacementIsValid();
    }
    #endregion

    #region Funkce Bloku

    //Vypne/zapne collidery bloku
    private void SetBlockCollidersTo(bool setTo) {
        for (int i = 0; i < this.BlockColliders.Count; i++){
            this.BlockColliders[i].enabled = setTo;
        }
    }

    public List<SymBlock> GetBlocksInNeighbour(){

        var neighbour_blocks = new List<SymBlock>();

        foreach (BlockChecker c in Checkers){
            neighbour_blocks.Add(Helpers.GetBlock(c.checkerNextTo.checkers_container.position));
        }

        if (neighbour_blocks.Count > 0)
            return neighbour_blocks;
        else{
            Debug.LogError("Blok nemá žádné sousedy.");
            return null;
        }

    }

    #endregion

    #region Funkce Checkerů bloku

    // Nastaví Všechny collidery bloku on/off
    public void SetCheckersCollidersTo(bool setTo = false){
        for (int i = 0; i < this.Checkers.Count; i++)
            this.Checkers[i].SetCheckerActiveState(setTo);
    }

    #endregion

    // Přidá blok do seznamu path-pointů v PathFinderu // TODO: dodělat zbytek cesty
    public void AddToPathFinding() {
        // Pokud ještě není blok postavený tak raději vrať
        if (!this.isBlockBuilded)
            return;

        PathFindingNodesLibrary.AddBlockToPathFinding(this);
    }

    #region Výpočet rotace a pozice při tvorbě nového bloku

    private void SetPositionInTheWorldToSymetricBlock()
    {
        if (!this.isSymetric) return;

        // Získáme pozici holdru checkeru, na kterém blok chceme postavit
        Vector3 checkers_pos = BaseCheckerNextTo.checkers_container.transform.parent.gameObject.transform.position; //!!! GLOBÁLNÍ POZICE Bloku 

        // např. vektor (0,-1,0) na základě orientace checkeru
        Vector3 orientationBase_b_pop = Settings.GetVector3Population_(BaseCheckerNextTo.position);

        // pozice jak daleko od středu sousedního bloku stavíme
        Vector3 offset_cordinates = orientationBase_b_pop * Mathf.Abs(symConstant);

        Vector3 finalBlockPosition = BaseCheckerNextTo.CheckerTransform.position + offset_cordinates;

        // Nastavíme offsetlou pozici bloku
        this.BlockContainer.transform.position = finalBlockPosition;
        this.BlockPosition = finalBlockPosition;

    }

    private void SetRotationInTheWorldToSymetricBlock()
    {
        if (!this.isSymetric) return;

        var blockRotValidator = new BlockRotationValidator(this);

        int blockRotAngle = SymBlockRotator.FindFirstPossibleAngleToRotateSymetricBlockTo(blockRotValidator.CheckIfRotationIsPossibleBasedOnCheckerTypes, 0);
        this.BlockRotation = new Vector3(0, blockRotAngle, 0);
    }

    #endregion

    #region FINALIZACE BUILDU
    /// <summary> Ukládáno jako soused kontroléru. </summary>
    public void UpdateNeighboursActivityOnBuild() {
        float searchRadius = this.symConstant * 2f + Settings.largestSymConstant + Mathf.Sqrt(2 * Mathf.Pow(Settings.largestSymConstant, 2)) + 1f; 

        List<SymBlock> allCloseBlocks = Helpers.GetAllBlocksInRadius(this, searchRadius);
        foreach (SymBlock b_around in allCloseBlocks) {

            b_around.Checkers.ForEach((otherBlocksChecker) =>
            {
                this.Checkers.ForEach((blockChecker) =>
              {
                  if (otherBlocksChecker.CheckerTransform.transform.position == blockChecker.CheckerTransform.transform.position)
                      if (otherBlocksChecker.checkerType == blockChecker.checkerType)
                          if (blockChecker.checkerNextTo == null)// Pokud ještě souseda nemá
                        {

                            // Přiřadíme souseda základnímu kontroléru
                            blockChecker.checkerNextTo = otherBlocksChecker;
                              blockChecker.isEmpty = false;
                              blockChecker.UpdateCheckerActiveState();
                              blockChecker.CreateCheckerGameObjectName();

                              otherBlocksChecker.checkerNextTo = blockChecker;
                              otherBlocksChecker.isEmpty = false;
                              otherBlocksChecker.UpdateCheckerActiveState();
                              otherBlocksChecker.CreateCheckerGameObjectName();
                          }

              });
            });
        }
    }

    // Finální metoda, která postaví blok.
    public void BuildBlock()
    {
        isBlockBuilded = true;
        // TODO: Kontrole jestli je možno - dostatek energie, dostatek vody
        isBlockActive = true;
    }

    public void ActivateBlock()
    {

    }

    #endregion
}
