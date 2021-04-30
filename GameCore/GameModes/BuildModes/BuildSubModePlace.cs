using Assets.Scripts.GameCore.WorldBuilding.ModifyWorld;
using Assets.Scripts.GameCore.UISystems.AskDialogueWindow;
using UnityEngine;
using Assets.Scripts.GameCore.WorldBuilding.BlockLibrary;
using System;

namespace Assets.Scripts.GameCore.GameModes
{
    public class BuildSubModePlace : IGameMode
    {
        private BlockChecker _checkerToBuildOn; // last active; last clicked on 
        public readonly ModifyWorldActionHandler ModifyWorldActionHandler = new ModifyWorldActionHandler();
        public SymBlock LastPlacedBlock;

        private ADWPlaceModeModul _dialogWindow;

        public BuildSubModePlace(BlockChecker checkerToBuildOn){
            _checkerToBuildOn = checkerToBuildOn;
        }

        public event EventHandler<Settings.Blocks_types> PlaceEvent;
        /* public event EventHandler<Settings.Blocks_types> PlaceEvent;
         public event EventHandler<bool> PlaceValidEvent;*/

        public void PlaceBlock(Settings.Blocks_types blockTypeToPlace)
        {
            if (!Settings.isBuildMode) return;

            // Vytvoření bloku
            var addBlockModAction = new AddBlockToWorldAction(WorldBuilderManager.World, _checkerToBuildOn, blockTypeToPlace);
            addBlockModAction.ModifyTheWorld();
            this.LastPlacedBlock = addBlockModAction.LastPlacedBlock;

            // Kontrola, jestli je placement ok
            bool isPlaceValid = LastPlacedBlock.CheckBlockPlacement();
            
            if (isPlaceValid)
                this.OnBlockPlacementValid();
            else
                this.OnPlacementInValid();
        }

       /* public virtual void OnPlace(Settings.Blocks_types e) {
           // this.PlaceEvent?.Invoke(this, e);
        }*/

        private void OnBlockPlacementValid() {
            if (LastPlacedBlock == null)
                return;

            if (LastPlacedBlock.canRotate)
                UI.BlockBuildGizmosState(true, LastPlacedBlock);

            UI.BlockLibraryWindowState(false);
           

            ScreenUIManager.Instance.AskDialogWindowController.SetWindowModul(_dialogWindow);
            _dialogWindow.Bind(LastPlacedBlock);
            _dialogWindow.OnWindowOn();


            LastPlacedBlock.BlocksMainGraphics.gameObject.SetActive(true);
        }
        private void OnPlacementInValid() {
            if (LastPlacedBlock == null)
                Debug.Log("lastPlacedBlock je null!!!!! to by se němělo stávat!!!");

            UI.GameScreenEvents(Settings.GameScreenEvents.NOT_ABLE_TO);
            ModifyWorldActionHandler.ModifyWorld(new RemoveBlockFromTheWorldAction(LastPlacedBlock));
        }


        public void RotateBlockBeforePlace(SymBlock blockToRotate, Vector3 rotateAngleToAdd) {
            if (Settings.isBuildMode)
                ModifyWorldActionHandler.ModifyWorld(new RotateBlockAction(blockToRotate, rotateAngleToAdd));
        }

        public void TurnModeOn()
        {
            Settings.isPlacingBlock = true;

            // vypneme knihovnu bloků
            UI.BlockLibraryWindowState(false); // Proč?? Nechceme vidět load knihovny (v případěm že by karet bylo víc tak by to mohlo bugovat a to by bylo zlé... )

            if (_checkerToBuildOn != null)
            {
                // Nastavíme kameru
                Manager.Instance.cameraController.SetTarget(_checkerToBuildOn.CheckerTransform);

                // Změna scény při kliknutí na checker - zvuk/zvýraznění
                _checkerToBuildOn.checkers_graphics.sharedMaterial = Helpers.GameHighlights(Settings.GameHighlights.CHECKER, _checkerToBuildOn.checkers_graphics);

                // Otevřeme UI Knihovnu bloků (v hře)
                UI.BlockLibraryWindowState(true, _checkerToBuildOn.checkerType); // Pošleme pouze typ posledního bloku

                // Barevný grid podkladu
                BlockLibrary.blocksLib.ForEach(b => b.BlockGrid?.SetGridActive(true));
            }

            _dialogWindow = new ADWPlaceModeModul(ModifyWorldActionHandler);
        }

        public void TurnModeOff()
        {
            Settings.isCamera = true;

            Settings.isPlacingBlock = false;

            // Musí se povolit kamera
            Manager.Instance.cameraController.ResetTarget();

            // Musí se zavřít všechna okna
            UI.BlockLibraryWindowState(false);

            // Zavřeme Y/N dialog
            ScreenUIManager.Instance.AskDialogWindowController.gameObject.SetActive(false);
         //   ScreenUIManager.Instance.AskDialogWindowController.OnWindowOff();

            UI.BlockBuildGizmosState(false); // (a gizmos)

            // Na kontroléru nastavíme zpět materiál
            _checkerToBuildOn.ResetCheckerMaterial();

            if (LastPlacedBlock != null)
                if (!LastPlacedBlock.isBlockPlaced)
                        this.RestorePlace();   // Odstraníme poslední nedokončený blok   
        }

        private void RestorePlace() {
            if (LastPlacedBlock.BlockContainer == null || LastPlacedBlock.BlockType == Settings.Blocks_types.CITY_HALL)
                return;

                MonoBehaviour.Destroy(LastPlacedBlock.BlockContainer);
                LastPlacedBlock = null;
        }
    }
}
