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
            // loads block library 
            foreach (var blockData in Resources.LoadAll<BlockSO>(Settings.BLOCKSO_PATH)) {
                Settings.blocksTypeLibrary.Add(blockData.blockType, blockData);
                UI.suitableBlocksOrganised.Add(blockData);
            }


            // Pokud se jedná o komplet nový svět:
            if (Manager.Instance.isNewWorld) {
                Settings.defaultWorldPosition = this.transform.position;
                var seed = Mathf.Abs("seed".GetHashCode()).ToString();
                SpaceStation = new SpaceStation(seed);
                SpaceStation.CreateNewSpaceStation();
            }
            else {
                // Load todo:
            }

            // Load zbylých proměnných
            Settings.largestSymConstant = Helpers.GetLargestSymConstantValue();

        }
    
        // BUILDMODE
        public void PlaceBlockInWorld(Settings.Blocks_types blockType) {
            var subMode = GameModesManager.Instance.subModesHandler.CurrentSubMode as BuildSubModePlace;
            subMode.PlaceBlock(blockType);
        }
        
        /*public void TurnBuildModeOn(BlockChecker controllerToBuildOn = null) {

            GameModesManagerNew.Instance.CurrentGameState.TurnOnBuildMode();

            if (controllerToBuildOn != null) {
                var placeMode = new BuildSubModePlace(controllerToBuildOn, SpaceStation);
                GameModesManager.Instance.subModesHandler.SetSubMode(placeMode);
                GameModesManager.Instance.subModesHandler.TurnModeOn();
            }
        }*/

        // turn build mode and submodes
        /*public void TurnBuildModeOff() {
            GameModesManager.Instance.subModesHandler.StopCurrentSubMode(typeof(BuildSubModePlace));
            GameModesManagerNew.Instance.CurrentGameState.TurnOnIndleMode();
        }*/
}