using Assets.Scripts.GameCore.BlockScripts.BlockAdditional.BlockFactory;
using Assets.Scripts.GameCore.WorldBuilding.BlockLibrary;
using Assets.Scripts.BlockScripts.BlockAdditional;
using System.Collections.Generic;
using UnityEngine;
using System;

public class World
{
    private IBlock _newBlock;
    public string WorldSeed;

  //  public BlockChecker LastActiveChecker { get; set; } // Poslední kontrolér na který se kliklo
  //  public Block LastActiveBlock { get; set; } // Poslední blok, na který se kliklo

    // Databáze všech bodů a jejich sousedů (možných napojení), které jsou definiční pro možné cesty vně stanice
    Dictionary<Vector3, List<Vector3>> pathPointsGroups = new Dictionary<Vector3, List<Vector3>>();

    public World(string seed) {
        this.WorldSeed = seed;
    }

    public void CreateNewWorld() {
        CreateCityHallBlock();

        BlockLibrary.AddBlockToBlocksLib(_newBlock); // Úspěšně vytvořený blok do db
        _newBlock.BuildBlock(); // Postavíme blok 

    }

    private void CreateCityHallBlock() {
        if (!Settings.blocksTypeLibrary.ContainsKey(Settings.Blocks_types.CITY_HALL)) {
            Debug.Log("Cityhall block data chybí v databázi ");
            return; }

        _newBlock = BlockFactory.CreateSymBlock(Settings.defaultWorldPosition, new Vector3(),
            Settings.blocksTypeLibrary[Settings.Blocks_types.CITY_HALL], null);

        var bConstructor = BlockFactory.BlockConstructor((SymBlock)_newBlock);

        _newBlock.ConstructBlock(bConstructor);
        _newBlock.ConstructBlockPost(bConstructor);

        _newBlock.BlocksMainGraphics.gameObject.SetActive(true);
    }

    public SymBlock CreateNewSymBlock(Vector3 position, Vector3 rotation, BlockSO BlockData, BlockChecker lastActiveChecker)
    {
        _newBlock = (SymBlock) BlockFactory.CreateSymBlock(position, rotation, BlockData, lastActiveChecker);

        var newBlock = (SymBlock)_newBlock;

        var bConstructor = BlockFactory.BlockConstructor((SymBlock)newBlock);

        bool wasBlockConstructed = newBlock.ConstructBlock(bConstructor);

        if (!wasBlockConstructed)
            Debug.LogError("Symetricý blok se nepodařilo vytvořit!");

        newBlock.SetBlockOrientation();

        newBlock.ConstructBlockPost(bConstructor); 

        return (SymBlock)_newBlock;
    }

}



