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

        BlockBuildPlacing _placeMode;

        public BlockBuildSubState(GameModesSM stateMachine) : base (stateMachine) {
            _stm = stateMachine;
        }

        public override void Enter() {

            base.Enter();

            if (_stm.LastActiveChecker == null)
                Exit();

            _placeMode = new BlockBuildPlacing(_stm.LastActiveChecker, World.Instance.SpaceStation);
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
            
            var actionData = (InteractionEventArgs)e;

            switch (actionData.ActionPerformed) {
                case InteractionEventArgs.ActionType.CANCLE_ACTION:
                    _stm.SetNewState(_stm.BuildState);
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
