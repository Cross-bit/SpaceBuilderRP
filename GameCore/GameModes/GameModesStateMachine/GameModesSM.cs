using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.GameCore.GameModes.GameModesStateMachine
{
    public class GameModesSM : StateMachine {

        // states

        public IdleGameState IdleGameState;

        public BaseBuildMode BuildState; // state grouping together build states

        public BlockBuildSubMode BlockBuildSubState;

        // other
        public BlockChecker LastActiveChecker { get; set; }
        public SymetricBlock LastActiveBlock { get; set; }
        
        private void Awake() {
            IdleGameState = new IdleGameState(this);
            BuildState = new BaseBuildMode(this);
            BlockBuildSubState = new BlockBuildSubMode(this);
        }

        protected override BaseState GetInitialState() {
            return IdleGameState;
        }
    }
}
