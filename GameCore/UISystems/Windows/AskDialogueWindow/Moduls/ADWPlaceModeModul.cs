
using Assets.Scripts.GameCore.GameModes;
using Assets.Scripts.GameCore.WorldBuilding.AdditionalForBuild;
using Assets.Scripts.GameCore.WorldBuilding.BlockLibrary;
using Assets.Scripts.GameCore.WorldBuilding.ModifyWorld;
using System;
using UnityEngine;


namespace Assets.Scripts.GameCore.UISystems.AskDialogueWindow
{
    public class ADWPlaceModeModul : IAskDialogueWindowModul
    {
        private World _world;
        private SymBlock _newBlock;
        private AskDialogueWindowController _controller = ScreenUIManager.Instance.AskDialogWindowController;

        private readonly ModifyWorldActionHandler _modifyWorldActionHandler;

        /*public event EventHandler windowOnTrueBtnClick;
        public event EventHandler windowOnFalseBtnClick;*/


        public ADWPlaceModeModul(ModifyWorldActionHandler modifyWorldActionHandler) {
            _world = WorldBuilderManager.World;
            _modifyWorldActionHandler = modifyWorldActionHandler;
        }

        public void Bind(SymBlock newBlock) {
            _newBlock = newBlock;
        }

        public void OnTrue() {
//            windowOnTrueBtnClick?.Invoke(this, EventArgs.Empty);

            this.OnWindowOff();

            BlockLibrary.AddBlockToBlocksLib(_newBlock);

            _newBlock.isBlockPlaced = true; // TODO: state maschine tady začnou makat boti

            _newBlock.UpdateNeighboursActivityOnBuild();

            _newBlock.BaseCheckerNextTo?.ResetCheckerMaterial();
            
            // grid
            _newBlock.BlockGrid?.SetGridOrientation();

            UI.BlockBuildGizmosState(false);

            SetBuildTimer();
          
            // Nastavíme AUTOMATICKÝ SWTITCH CHECKERU NA DALŠÍ
            if (Settings.switchCheckers && _newBlock.Checkers.Count > 1)
                SwitchFocusToAnotherChecker();
            else
                GameModesManager.Instance.subModesHandler.StopCurrentSubMode(typeof(BuildSubModePlace));
        }

        public void OnFalse() {

            // Zavře se Y/N dialogové okno
            this.OnWindowOff();

            // Odstraní blok
            _modifyWorldActionHandler.ModifyWorld(new RemoveBlockFromTheWorldAction(_newBlock));

            // UI
            UI.BlockLibraryWindowState(true, _newBlock.BaseCheckerNextTo.checkerType);

            // zavřeme gizmos
            UI.BlockBuildGizmosState(false);
        }

        public void OnWindowOn() => this.WindowInit();

        public void OnWindowOff() => _controller.gameObject.SetActive(false);

        private void WindowInit() {
            if (_newBlock == null) {
                Debug.Log("ADWPlaceModeModul nezná block.");
                return; }

            _controller.Message.text = TextHolder.BUILDMODE_TITLE;

            var blockPosition = _newBlock.BlockPosition;

            _controller.PositionToDrawOn = new Vector3(blockPosition.x, blockPosition.y + Settings.dialogWindowHeight, blockPosition.z);

            _controller.AskDialogueContainer.gameObject.SetActive(true); // Zapneme
        }

        private void SwitchFocusToAnotherChecker() {
            var nextCheckerFinder = new NextCheckerForBuildGetter(_newBlock);
            var nextCheckerToBuildOn = nextCheckerFinder.GetNextCheckerToBuild();

            if (nextCheckerToBuildOn != null)
                WorldBuilderManager.Instance.TurnBuildModeOn(nextCheckerToBuildOn);
            else
                Debug.LogError("V WorldBuilder.cs nebyl nalezen další checker ke stavbě ");
        }

        private void SetBuildTimer() {

            var blockContainerPos = _newBlock.BlockContainer.transform.position;

            // Aktivujeme Časovač (vytáhneme ho z poolu)
            var UIBuildTimerObject = ScreenUIManager.Instance.UIPoolList.GetFromPool(
                Settings.PoolTypes.UI_TIMER,
                new Vector3(blockContainerPos.x, blockContainerPos.y + Settings.timerHeight, blockContainerPos.z),
                Quaternion.identity, ScreenUIManager.Instance.timersHolder);

            // Nastavíme time 
            UIBuildTimerObject.GetComponent<BuildTimerController>().SetData(_newBlock.TimeToBuild);

            WorldBuilderManager.Instance.RunBuildTimer(new WaitForSeconds(_newBlock.TimeToBuild), UIBuildTimerObject, _newBlock);
        }
    }
}
