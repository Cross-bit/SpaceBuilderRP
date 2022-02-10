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

        // this should not be here todo:
        public void SetLastActiveBlock(RaycastHit hitData) {
            GameObject checker_obj = hitData.transform.gameObject;
            Vector3 checkers_pos = checker_obj.transform.parent.gameObject.transform.position; //!!! GLOBÁLNÍ POZICE Bloku (GameObjektu)!!!
            this.LastActiveBlock = Helpers.GetBlock(checkers_pos);
        }

        public void SetLastActiveChecker(RaycastHit hitData) {
            if (this.LastActiveBlock == null)
                SetLastActiveBlock(hitData);

            this.LastActiveChecker = Helpers.GetLastActiveChecker(this.LastActiveBlock, hitData.transform.position);
        }
    }
}
