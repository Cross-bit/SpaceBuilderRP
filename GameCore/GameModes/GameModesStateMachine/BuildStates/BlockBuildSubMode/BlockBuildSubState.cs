using Assets.Scripts.GameCore.GameControls.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.GameCore.GameModes.GameModesStateMachine
{
    /// <summary> Build substate for block placement. </summary>
    public class BlockBuildSubState : BaseBuildState {

        GameModesSM _stm;

        BlockBuildOnPlacement _placeMode;

        public BlockBuildSubState(GameModesSM stateMachine) : base (stateMachine) {
            _stm = stateMachine;
        }

        public override void Enter() {

            base.Enter();

            if (_stm.LastActiveChecker == null)
                Exit();

            _placeMode = new BlockBuildOnPlacement(_stm.LastActiveChecker, World.Instance.SpaceStation);
            _placeMode.TurnModeOn();

            ScreenUIManager.Instance.allBuildCards?.ForEach(card => {
                card.ClickedCard += PlayerClickedCard;
            });

            InputManager.Instance.PlayerActionInputs.PlayerInteracted += PlayerInteracted;
        }

        private void PlayerClickedCard(object sender, EventArgs e) {
            var cardData = (BlockCardEventArgs)e;

            _placeMode?.PlaceBlock(cardData.BlockType);
        }

        protected override void PlayerInteracted(object interactionControls, EventArgs e) {
            
            var actionData = (InteractionEventArgs)e; // TODO: refactor DRY of thid listener(it is in every state)

            switch (actionData.ActionPerformed) {
                case InteractionEventArgs.ActionType.CANCLE_ACTION:
                    _stm.SetNewState(_stm.BuildState);
                break;

                case InteractionEventArgs.ActionType.MOUSE_L_ACTION:

                    var interaction = new PlayerInteractionsHandler();

                    if (interaction.HitData.transform == null)
                        return;

                    if (interaction.HitData.transform.gameObject.layer == LayerMask.NameToLayer(Settings.CHECKER_LAYER))
                    {

                        _stm.SetLastActiveBlock(interaction.HitData); // resets the blocks buffer
                        _stm.SetLastActiveChecker(interaction.HitData);

                        _stm.SetNewState(_stm.BlockBuildSubState);
                    }
                break;
            }
        }

        public override void Exit() {

            base.Exit();

            _placeMode.TurnModeOff();

            ScreenUIManager.Instance.allBuildCards?.ForEach(card => {
                card.ClickedCard -= PlayerClickedCard;
            });

            InputManager.Instance.PlayerActionInputs.PlayerInteracted -= PlayerInteracted;

        }

    }
}
