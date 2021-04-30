using Assets.Scripts.GameCore.UISystems.AskDialogueWindow;
using Assets.Scripts.GameCore.BuildCoroutinesLib;
using Assets.Scripts.GameCore.GameModes;
using UnityEngine;
using System;
using Assets.Scripts.GameCore.WorldBuilding.ModifyWorld;
using System.Collections;
using System.Collections.Generic;

public class WorldBuilderManager : Singleton<WorldBuilderManager>
{
        // Nastavení světa
        public static World World { get; private set; }


        // Inicializace všeho
        private void Awake()
        {
            foreach (var blockData in Resources.LoadAll<BlockSO>(Settings.BLOCKSO_PATH)) {
                Settings.blocksTypeLibrary.Add(blockData.blockType, blockData);
                UI.suitableBlocksOrganised.Add(blockData);
            }
        }
        
        void Start() {

            // Pokud se jedná o komplet nový svět:
            if (Manager.Instance.isNewWorld) {
                Settings.defaultWorldPosition = this.transform.position;
                var seed = Mathf.Abs("seed".GetHashCode()).ToString();
                World = new World(seed);
                World.CreateNewWorld();
            }
            else
            {
                // Load
            }

            // Load zbylých proměnných
            Settings.largestSymConstant = Helpers.GetLargestSymConstantValue();

        }
    
        // BUILDMODE
        
        public void TurnBuildModeOn(BlockChecker controllerToBuildOn = null) {
            GameModesManager.Instance.SetGameMode(new BuildMode());
            GameModesManager.Instance.TurnModeOn();
        
            if (controllerToBuildOn != null) {

                var placeMode = new BuildSubModePlace(controllerToBuildOn);
                GameModesManager.Instance.subModesHandler.SetSubMode(placeMode);
                GameModesManager.Instance.subModesHandler.TurnModeOn();
            }
         }

        // Rekurzivně vypne submode a buildmode
        public void TurnBuildModeAndSubModeOffRecursive() {
            if (Settings.isBuildMode)
            {
                if (Settings.isPlacingBlock)
                    GameModesManager.Instance.subModesHandler.StopCurrentSubMode(typeof(BuildSubModePlace));
                else
                    GameModesManager.Instance.StopCurrentGameMode<BuildMode>();
            }
        }

        public void RunBuildTimer(WaitForSeconds buildTime, GameObject timerUIObj, SymBlock block) {
           Instance.StartCoroutine(BuildCoroutinesLib.BuildCoroutine(buildTime, timerUIObj, block));
        }
}