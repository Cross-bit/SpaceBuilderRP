using Assets.Scripts.GameCore.WorldBuilding.AdditionalForBuild;
using Assets.Scripts.GameCore.WorldBuilding.ModifyWorld;
using Assets.Scripts.GameCore.UISystems.AskDialogueWindow;
using Assets.Scripts.GameCore.WorldBuilding.BlockLibrary;
using Assets.Scripts.GameCore.BuildCoroutines;
using UnityEngine;
using System;

namespace Assets.Scripts.GameCore.GameModes
{
    public class BlockBuildPlacing {


        private BlockChecker _checkerToBuildOn; // last active; last clicked on 
        public readonly ModifyWorldActionHandler ModifyWorldActionHandler = new ModifyWorldActionHandler();
        public SymetricBlock LastPlacedBlock;

        private SpaceStation _spaceStation;

        private ADWPlaceModeModul _dialogWindow;

        public BlockChecker NextChecker = null;

        public BlockBuildPlacing(BlockChecker checkerToBuildOn, SpaceStation spaceStation) {
            _checkerToBuildOn = checkerToBuildOn;
            _spaceStation = spaceStation;
        }

        public void PlaceBlock(Settings.Blocks_types blockTypeToPlace) {

            // Vytvoření bloku
            var addBlockModAction = new AddBlockToWorldAction(_spaceStation, _checkerToBuildOn, blockTypeToPlace);
            addBlockModAction.ModifyTheWorld();
            this.LastPlacedBlock = addBlockModAction.LastPlacedBlock;

            // Kontrola, jestli je placement ok
            bool isPlaceValid = LastPlacedBlock.CheckBlockPlacement();

            if (isPlaceValid)
                this.OnBlockPlacementValid();
            else
                this.OnPlacementInvalid();
        }

        private void OnBlockPlacementValid() {
            if (LastPlacedBlock == null)
                return;

            if (LastPlacedBlock.canRotate)
                UI.BlockBuildGizmosState(true, LastPlacedBlock);

            UI.BlockLibraryWindowState(false);

            // player dialog window y/n

            _dialogWindow = new ADWPlaceModeModul(ModifyWorldActionHandler, LastPlacedBlock.BlockPosition);

            ScreenUIManager.Instance.AskDialogWindowController.SetWindowModul(_dialogWindow);
            _dialogWindow.PlayerAccepted += OnPlayerAccept;
            _dialogWindow.PlayerRejected += OnPlayerReject;
            _dialogWindow.OnWindowOn();


            LastPlacedBlock.BlocksMainGraphics.gameObject.SetActive(true);
        }

        public void SetBuildTimer() {

            var blockContainerPos = LastPlacedBlock.BlockContainer.transform.position;

            // get timer from pool
            var UIBuildTimerObject = ScreenUIManager.Instance.UIPoolList.GetFromPool(
                Settings.PoolTypes.UI_TIMER,
                new Vector3(blockContainerPos.x, blockContainerPos.y + Settings.timerHeight, blockContainerPos.z),
                Quaternion.identity, ScreenUIManager.Instance.timersHolder);

            // set time
            UIBuildTimerObject.GetComponent<BuildTimerController>().SetData(LastPlacedBlock.TimeToBuild);

            RunBuildTimer(new WaitForSeconds(LastPlacedBlock.TimeToBuild), UIBuildTimerObject, LastPlacedBlock);
        }

        private void RunBuildTimer(WaitForSeconds buildTime, GameObject timerUIObj, SymetricBlock block) {
            World.Instance.StartCoroutine(BuildCoroutinesLib.BuildCoroutine(buildTime, timerUIObj, block));
        }

        private void OnPlayerAccept(object sender, EventArgs args) {

            _dialogWindow.OnWindowOff();

            BlockLibrary.AddBlockToBlocksLib(LastPlacedBlock);

            LastPlacedBlock.isBlockPlaced = true;

            LastPlacedBlock.UpdateNeighboursActivityOnBuild();

            LastPlacedBlock.BaseCheckerNextTo?.ResetCheckerMaterial();

            // grid
            LastPlacedBlock.BlockGrid?.SetGridOrientation();

            UI.BlockBuildGizmosState(false);

            SetBuildTimer();

            // Nastavíme AUTOMATICKÝ SWTITCH CHECKERU NA DALŠÍ
            if (Settings.switchCheckers && LastPlacedBlock.Checkers.Count > 1)
                SwitchFocusToAnotherChecker();
            else
                TurnModeOff();

        }

        private void SwitchFocusToAnotherChecker() {
            var nextCheckerFinder = new NextCheckerForBuildFinder(LastPlacedBlock);
            var nextCheckerToBuildOn = nextCheckerFinder.GetNextCheckerToBuild();

            if(nextCheckerToBuildOn == null)
                Debug.LogError("V WorldBuilder.cs nebyl nalezen další checker ke stavbě ");

            NextChecker = nextCheckerToBuildOn;
        }

        private void OnPlayerReject(object sender, EventArgs args) {

            // Zavře se Y/N dialogové okno
            _dialogWindow.OnWindowOff();

            // Odstraní blok
            ModifyWorldActionHandler.ModifyWorld(new RemoveBlockFromTheWorldAction(LastPlacedBlock));

            // UI
            UI.BlockLibraryWindowState(true, LastPlacedBlock.BaseCheckerNextTo.checkerType);

            // zavřeme gizmos
            UI.BlockBuildGizmosState(false);
        }

        private void OnPlacementInvalid() {

            UI.GameScreenEvents(Settings.GameScreenEvents.NOT_ABLE_TO);
            ModifyWorldActionHandler.ModifyWorld(new RemoveBlockFromTheWorldAction(LastPlacedBlock));
        }


        public void RotateBlockBeforePlace(SymetricBlock blockToRotate, Vector3 rotateAngleToAdd) {
            ModifyWorldActionHandler.ModifyWorld(new RotateBlockAction(blockToRotate, rotateAngleToAdd));
        }

        public void TurnModeOn() {

            // Make sure library is off
            UI.BlockLibraryWindowState(false);

            //Set camera
            Manager.Instance.cameraController.SetTarget(_checkerToBuildOn.CheckerTransform);

            _checkerToBuildOn.checkers_graphics.sharedMaterial = Helpers.GameHighlights(Settings.GameHighlights.CHECKER, _checkerToBuildOn.checkers_graphics);

            // UI blocks library on with coresponding last block 
            UI.BlockLibraryWindowState(true, _checkerToBuildOn.checkerType);

            // Set block grid on
            BlockLibrary.blocksLib.ForEach(b => b.BlockGrid?.SetGridActive(true));
        }

        public void TurnModeOff() {

            // Musí se povolit kamera
            Manager.Instance.cameraController.ResetTarget();

            // Musí se zavřít všechna okna
            UI.BlockLibraryWindowState(false);

            // Zavřeme Y/N dialog
            ScreenUIManager.Instance.AskDialogWindowController.gameObject.SetActive(false);

            UI.BlockBuildGizmosState(false);

            // Na kontroléru nastavíme zpět materiál
            _checkerToBuildOn.ResetCheckerMaterial();

            this.RestorePlace();

            if(_dialogWindow != null) {
                _dialogWindow.PlayerAccepted -= OnPlayerAccept;
                _dialogWindow.PlayerRejected -= OnPlayerReject;
            }
        }

        private void RestorePlace() {
            if (LastPlacedBlock == null || LastPlacedBlock.isBlockPlaced)
                return;

            if (LastPlacedBlock.BlockContainer == null || LastPlacedBlock.BlockType == Settings.Blocks_types.CITY_HALL)
                return;

            MonoBehaviour.Destroy(LastPlacedBlock.BlockContainer);
            LastPlacedBlock = null;
        }
    }
}
