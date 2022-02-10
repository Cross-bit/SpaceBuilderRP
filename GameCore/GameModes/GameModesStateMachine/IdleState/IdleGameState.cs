using Assets.Scripts.GameCore.GameControls.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.GameCore.GameModes.GameModesStateMachine
{
    public class IdleGameState : BaseState {

        GameModesSM _stm;
        public IdleGameState(GameModesSM stateMachine) : base(stateMachine) {
            _stm = stateMachine;
        }

        public override void Enter() {

            _stm.LastActiveBlock = null;
            _stm.LastActiveChecker = null;

            InputManager.Instance.PlayerActionInputs.PlayerInteracted += OnPlayerWorldInteraction;
        }

        public override void Exit() {
            InputManager.Instance.PlayerActionInputs.PlayerInteracted -= OnPlayerWorldInteraction;
        }

        protected virtual void OnPlayerWorldInteraction(object interactionControls, EventArgs args) {

            var actionData = (InteractionEventArgs)args;

            switch (actionData.ActionPerformed) {
                case InteractionEventArgs.ActionType.MOUSE_L_ACTION:

                    var interaction = new PlayerInteractionsHandler();

                    if (interaction.HitData.transform == null)
                        return;
                    

                    if (interaction.HitData.transform.gameObject.layer == LayerMask.NameToLayer(Settings.CHECKER_LAYER)) {
                        SetLastActiveChecker(interaction.HitData);
                        _stm.SetNewState(_stm.BlockBuildSubState);
                    }

                    /*else if (interaction.HitData.transform.gameObject.layer == LayerMask.NameToLayer(Settings.BLOCK_LAYER)) {
                        SetLastActiveBlock(interaction.HitData); // todo: show window etc.
                    }*/

                    break;
            }
        }

        private void SetLastActiveBlock(RaycastHit hitData) {
            GameObject checker_obj = hitData.transform.gameObject;
            Vector3 checkers_pos = checker_obj.transform.parent.gameObject.transform.position; //!!! GLOBÁLNÍ POZICE Bloku (GameObjektu)!!!
            _stm.LastActiveBlock = Helpers.GetBlock(checkers_pos);
        }

        private void SetLastActiveChecker(RaycastHit hitData) {
            if (_stm.LastActiveBlock == null)
                SetLastActiveBlock(hitData);

            _stm.LastActiveChecker = Helpers.GetLastActiveChecker(_stm.LastActiveBlock, hitData.transform.position);
        }

        public override void OnUpdate() { }

        public override void OnLateUpdate() { }

    }
}
