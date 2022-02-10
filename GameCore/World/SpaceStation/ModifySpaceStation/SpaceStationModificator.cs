using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.GameCore.WorldBuilding.ModifyWorld
{
    // middle structure between space station and rest of the game
    // using command pattern, this is invoker

    /// <summary> Invoker for space station modification commands </summary>
    public class SpaceStationModificator //(invoker)
    {
        IModifySpaceStationCommand lastCommand;

        public void ModifySpaceStation(IModifySpaceStationCommand modifyAction) {
            this.lastCommand = modifyAction;
        }

        public void Modify() {
            lastCommand?.ModifySpaceStation();
        }
    }
}
