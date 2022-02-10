using Assets.Scripts.GameCore.BlockScripts.BlockAdditional.BlockFactory;
using Assets.Scripts.GameCore.WorldBuilding.BlockLibrary;
using Assets.Scripts.BlockScripts.BlockAdditional;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SpaceStation
{
    private IBlock _newBlock;
    public string WorldSeed;

    // Databáze všech bodů a jejich sousedů (možných napojení), které jsou definiční pro možné cesty vně stanice
    Dictionary<Vector3, List<Vector3>> pathPointsGroups = new Dictionary<Vector3, List<Vector3>>();

    public SpaceStation(string seed) {
        this.WorldSeed = seed;
    }

    public void CreateNewSpaceStation() {
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

        var bConstructor = BlockFactory.BlockConstructor((SymetricBlock)_newBlock);

        _newBlock.ConstructBlock(bConstructor);
        _newBlock.ConstructBlockPost(bConstructor);

        _newBlock.BlocksMainGraphics.gameObject.SetActive(true);
    }

    /// <summary>Creates new symetric block on given checker</summary>
    public SymetricBlock CreateNewSymBlock(Vector3 position, Vector3 rotation, BlockSO BlockData, BlockChecker checkerToBuildOn) {
        _newBlock = (SymetricBlock) BlockFactory.CreateSymBlock(position, rotation, BlockData, checkerToBuildOn);

        var newBlock = (SymetricBlock)_newBlock;

        var bConstructor = BlockFactory.BlockConstructor((SymetricBlock)newBlock);

        bool wasBlockConstructed = newBlock.ConstructBlock(bConstructor);

        if (!wasBlockConstructed)
            Debug.LogError("Symetricý blok se nepodařilo vytvořit!");

        newBlock.SetBlockOrientation();

        newBlock.ConstructBlockPost(bConstructor); 

        return (SymetricBlock)_newBlock;
    }

}



