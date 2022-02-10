using Assets.Scripts.GameCore.GameControls.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.GameCore.GameModes.GameModesStateMachine
{
    public class BlockBuildSubMode : BaseBuildMode {

        GameModesSM _stm;

        public BlockBuildSubMode(GameModesSM stateMachine) : base (stateMachine) {
            _stm = stateMachine;
        }

        public override void Enter() {

            base.Enter();

            var placeMode = new BuildSubModePlace(_stm.LastActiveChecker, World.Instance.SpaceStation);
            GameModesManager.Instance.subModesHandler.SetSubMode(placeMode);
            GameModesManager.Instance.subModesHandler.TurnModeOn();

            _stm.LastActiveChecker = placeMode.NextChecker;

            InputManager.Instance.PlayerActionInputs.PlayerInteracted += OnPlayerAction;
        }

        protected override void OnPlayerAction(object interactionControls, EventArgs args) {
            
            var actionData = (InteractionData)args;

            switch (actionData.ActionPerformed) {
                case InteractionData.ActionType.CANCLE_ACTION:
                    _stm.SetNewState(_stm.BuildState);
                break;
            }
        }

        public override void Exit() {

            base.Exit();

            GameModesManager.Instance.subModesHandler.StopCurrentSubMode(typeof(BuildSubModePlace));

            InputManager.Instance.PlayerActionInputs.PlayerInteracted -= OnPlayerAction;

        }

    }
}
