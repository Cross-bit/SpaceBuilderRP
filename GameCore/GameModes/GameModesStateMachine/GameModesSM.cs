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

        public BaseBuildState BuildState; // state grouping together build states

        public BlockBuildSubState BlockBuildSubState;

        // other
        public BlockChecker LastActiveChecker { get; set; }
        public SymetricBlock LastActiveBlock { get; set; }
        
        private void Awake() {
            IdleGameState = new IdleGameState(this);
            BuildState = new BaseBuildState(this);
            BlockBuildSubState = new BlockBuildSubState(this);
        }

        protected override BaseState GetInitialState() {
            return IdleGameState;
        }
    }
}
