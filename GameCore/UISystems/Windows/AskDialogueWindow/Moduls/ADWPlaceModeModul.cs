using Assets.Scripts.GameCore.GameModes;
using Assets.Scripts.GameCore.WorldBuilding.AdditionalForBuild;
using Assets.Scripts.GameCore.WorldBuilding.BlockLibrary;
using Assets.Scripts.GameCore.WorldBuilding.ModifyWorld;
using Assets.Scripts.GameCore.BuildCoroutines;
using System;
using UnityEngine;


namespace Assets.Scripts.GameCore.UISystems.AskDialogueWindow
{
    public class ADWPlaceModeModul : IAskDialogueWindowModul
    {
        private Vector3 _positionToDraw;
        private AskDialogueWindowController _controller = ScreenUIManager.Instance.AskDialogWindowController;

        private readonly SpaceStationModificator _modifyWorldActionHandler;

        public event EventHandler PlayerAccepted;
        public event EventHandler PlayerRejected;


        public ADWPlaceModeModul(SpaceStationModificator modifyWorldActionHandler, Vector3 positionToDraw) {
            _modifyWorldActionHandler = modifyWorldActionHandler;
            _positionToDraw = positionToDraw;
        }

        public void OnTrue() {
            PlayerAccepted?.Invoke(this, EventArgs.Empty);
        }

        public void OnFalse() {
            PlayerRejected?.Invoke(this, EventArgs.Empty);
        }

        public void OnWindowOn() => this.WindowInit();

        public void OnWindowOff() => _controller.gameObject.SetActive(false);

        private void WindowInit() {

            _controller.Message.text = TextHolder.BUILDMODE_TITLE;

            _controller.PositionToDrawOn = new Vector3(_positionToDraw.x, _positionToDraw.y + Settings.dialogWindowHeight, _positionToDraw.z);

            _controller.AskDialogueContainer.gameObject.SetActive(true); // Zapneme
        }
    }
}
