using Assets.Scripts.GameCore.WorldBuilding.BlockLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.GameCore.GameModes
{
    public class BuildModeState : IGameState
    {
        public BuildModeStateMachine BuildStateMachine = new BuildModeStateMachine();

        public void TurnOnBuildMode() {
            return;
        }

        public void TurnOnIndleMode() {

            // Turn on free camera
            Manager.Instance.cameraController.ResetTarget();

            // Screen highlight
            UI.ScreenHighlightState(false);

            // Vypneme ostatní buildmode prvky
            UI.BuildModeElementsState(false);

            // Gizmos
            GizmosInGame.GridState(false);

            BlockLibrary.blocksLib.ForEach(b => b.BlockGrid?.SetGridActive(false));
            // todo:

            GameModesManagerNew.Instance.CurrentGameState = GameModesManagerNew.Instance.IdleModeState;
        }
    }
}
