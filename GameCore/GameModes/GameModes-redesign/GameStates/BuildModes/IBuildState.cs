using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.GameCore.GameModes
{
    public interface IBuildState {
        public void TurnOnBlockDeleteState();
        public void TurnOnBlockPlacingState(BlockChecker checkerToBuildOn); // the mode where player is plaicing given block
        public void TurnOnBlockEditState();
        public void TurnOnBuildIdleState(BlockChecker checkerToBuildOn);
    }
}
