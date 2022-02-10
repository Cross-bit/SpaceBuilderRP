using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.GameCore.GameModes
{
    public class BuildModeStateMachine {

        public IBuildState CurrentBuildState;

        public BuildIdleState BuildIdleState;
        public BlockPlaicingState BlockPlaicingState;
        //public DeleteBlockState BuildDeleteBlockState;

        public BuildModeStateMachine(){
            BuildIdleState = new BuildIdleState();
            BlockPlaicingState = new BlockPlaicingState();
        }


    }
}
