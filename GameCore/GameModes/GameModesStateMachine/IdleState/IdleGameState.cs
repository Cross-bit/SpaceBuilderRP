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
                        _stm.SetLastActiveChecker(interaction.HitData);
                        _stm.SetNewState(_stm.BlockBuildSubState);
                    }

                    else if (interaction.HitData.transform.gameObject.layer == LayerMask.NameToLayer(Settings.BLOCK_LAYER)) {
                        _stm.SetLastActiveBlock(interaction.HitData);
                        UI.BlockDetailWindowState(true, _stm.LastActiveBlock);
                        AudioManager.Instance.Play(Settings.SoundEffects.mouse_click_1);
                    }

                    break;
            }
        }
    }
}
