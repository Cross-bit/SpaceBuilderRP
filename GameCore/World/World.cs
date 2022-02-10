using Assets.Scripts.GameCore.UISystems.AskDialogueWindow;
using Assets.Scripts.GameCore.BuildCoroutines;
using Assets.Scripts.GameCore.GameModes;
using UnityEngine;
using System;
using Assets.Scripts.GameCore.WorldBuilding.ModifyWorld;
using System.Collections;
using System.Collections.Generic;

public class World : Singleton<World>
{
        // Nastavení světa
        public SpaceStation SpaceStation { get; private set; }

        void Start() {

            InitBlockLibrary();

            LoadWorldData();

            // Load zbylých proměnných
            Settings.largestSymConstant = Helpers.GetLargestSymConstantValue();

    }

    private void LoadWorldData() {

        if (Manager.Instance.isNewWorld) {
            // crate new space station
            Settings.defaultWorldPosition = this.transform.position;
            var seed = Mathf.Abs("seed".GetHashCode()).ToString();
            SpaceStation = new SpaceStation(seed);
            SpaceStation.CreateNewSpaceStation();
        }
        else {
            // Load from disk todo:
        }
    }


    private void InitBlockLibrary() {
        // loads block library 
        foreach (var blockData in Resources.LoadAll<BlockSO>(Settings.BLOCKSO_PATH)) {
            Settings.blocksTypeLibrary.Add(blockData.blockType, blockData);
            UI.suitableBlocksOrganised.Add(blockData);
        }
    }

}