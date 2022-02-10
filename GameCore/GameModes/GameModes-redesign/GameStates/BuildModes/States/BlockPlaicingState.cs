using Assets.Scripts.GameCore.UISystems.AskDialogueWindow;
using Assets.Scripts.GameCore.WorldBuilding.ModifyWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.GameCore.GameModes
{
    public class BlockPlaicingState : IBuildState {

        /*private BlockChecker _checkerToBuildOn; // last active; last clicked on 
        public readonly ModifyWorldActionHandler ModifyWorldActionHandler = new ModifyWorldActionHandler();
        public SymBlock LastPlacedBlock;

        ADWPlaceModeModul _dialogWindow;*/

        public void TurnOnBlockDeleteState()
        {
            throw new NotImplementedException();
        }

        public void TurnOnBlockEditState()
        {
            throw new NotImplementedException();
        }

        public void TurnOnBlockPlacingState() { // todo: remove
            return;
        }

        public void TurnOnBlockPlacingState(BlockChecker checkerToBuildOn)
        {
            throw new NotImplementedException();
        }

        public void TurnOnBuildIdleState(BlockChecker checkerToBuildOn) {


            // Musí se povolit kamera
            Manager.Instance.cameraController.ResetTarget();

            // Musí se zavřít všechna okna
            UI.BlockLibraryWindowState(false);

            // Zavřeme Y/N dialog
            ScreenUIManager.Instance.AskDialogWindowController.gameObject.SetActive(false);
            //   ScreenUIManager.Instance.AskDialogWindowController.OnWindowOff();

            UI.BlockBuildGizmosState(false); // (a gizmos)

            // Na kontroléru nastavíme zpět materiál
            checkerToBuildOn.ResetCheckerMaterial();

            /*if (LastPlacedBlock != null)
                if (!LastPlacedBlock.isBlockPlaced)
                    this.RestorePlace();*/   // Odstraníme poslední nedokončený blok   

        }

        /*private void OnPlayerAcceptedBlockPlacement() {
            if (LastPlacedBlock == null)
                return;

            if (LastPlacedBlock.canRotate)
                UI.BlockBuildGizmosState(true, LastPlacedBlock);

            UI.BlockLibraryWindowState(false);


            /*ScreenUIManager.Instance.AskDialogWindowController.SetWindowModul(_dialogWindow);
            _dialogWindow.Bind(LastPlacedBlock);
            _dialogWindow.OnWindowOn();


            LastPlacedBlock.BlocksMainGraphics.gameObject.SetActive(true);
        }*/


        /*private void OnPlayerRejectedBlockPlacment() {

            if (LastPlacedBlock == null)
                Debug.Log("lastPlacedBlock je null!!!!! to by se němělo stávat!!!");

            UI.GameScreenEvents(Settings.GameScreenEvents.NOT_ABLE_TO);
            ModifyWorldActionHandler.ModifyWorld(new RemoveBlockFromTheWorldAction(LastPlacedBlock));
        }*/

      /*  private void RestorePlace() {
            if (LastPlacedBlock.BlockContainer == null || LastPlacedBlock.BlockType == Settings.Blocks_types.CITY_HALL)
                return;

            MonoBehaviour.Destroy(LastPlacedBlock.BlockContainer);
            LastPlacedBlock = null;
        }*/
    }
}
