using Assets.Scripts.GameCore.GameControls.Controllers;
using Assets.Scripts.GameCore.WorldBuilding.BlockLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.GameCore.GameModes.GameModesStateMachine
{
    public class BaseBuildMode : BaseState {

        GameModesSM _stm;

        public BaseBuildMode(GameModesSM stm) : base(stm) {
            _stm = stm;
        }

        public override void Enter() {

            if (!Settings.isGameLoaded) return;
            // Screen highlight
            UI.ScreenHighlightState(true, Settings.ScreenHighlights.BUILD); // Zapneme highlight
            // Barevný grid podkladu
            BlockLibrary.blocksLib.ForEach(b => b.BlockGrid?.SetGridActive(true));
            UI.BuildModeElementsState(true);
            GameModesManagerNew.Instance.CurrentGameState = GameModesManagerNew.Instance.BuildModeState;

            InputManager.Instance.PlayerActionInputs.PlayerInteracted += OnPlayerAction;
        }

        public override void Exit() {
            // Turn on free camera
            Manager.Instance.cameraController.ResetTarget();
            // Screen highlight
            UI.ScreenHighlightState(false);
            // Vypneme ostatní buildmode prvky
            UI.BuildModeElementsState(false);
            // Gizmos
            GizmosInGame.GridState(false);
            BlockLibrary.blocksLib.ForEach(b => b.BlockGrid?.SetGridActive(false));
            GameModesManagerNew.Instance.CurrentGameState = GameModesManagerNew.Instance.IdleModeState;

            InputManager.Instance.PlayerActionInputs.PlayerInteracted -= OnPlayerAction;
        }

        protected virtual void OnPlayerAction(object interactionControls, EventArgs args) {

            var actionData = (InteractionData)args;

            switch (actionData.ActionPerformed) {
                case InteractionData.ActionType.CANCLE_ACTION:
                    _stm.SetNewState(_stm.IdleGameState);
                    break;
            }
        }

        public override void OnLateUpdate() {

        }

        public override void OnUpdate() {
        }
    }
}
