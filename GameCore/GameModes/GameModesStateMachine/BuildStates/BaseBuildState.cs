using Assets.Scripts.GameCore.GameControls.Controllers;
using Assets.Scripts.GameCore.WorldBuilding.BlockLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.GameCore.GameModes.GameModesStateMachine
{
    /// <summary> Common base state for all build states. </summary>
    public class BaseBuildState : BaseState {

        GameModesSM _stm;

        public BaseBuildState(GameModesSM stm) : base(stm) {
            _stm = stm;
        }

        public override void Enter() {

            if (!Settings.isGameLoaded) return;
            // Screen highlight
            UI.ScreenHighlightState(true, Settings.ScreenHighlights.BUILD); // Zapneme highlight
            // Barevný grid podkladu
            BlockLibrary.blocksLib.ForEach(b => b.BlockGrid?.SetGridActive(true));
            UI.BuildModeElementsState(true);

            InputManager.Instance.PlayerActionInputs.PlayerInteracted += PlayerInteracted;
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

            InputManager.Instance.PlayerActionInputs.PlayerInteracted -= PlayerInteracted;
        }

        protected virtual void PlayerInteracted(object interactionControls, EventArgs e) {

            var actionData = (InteractionEventArgs)e;

            switch (actionData.ActionPerformed) {
                case InteractionEventArgs.ActionType.CANCLE_ACTION:
                    _stm.SetNewState(_stm.IdleGameState);
                    break;
            }
        }

    }
}
