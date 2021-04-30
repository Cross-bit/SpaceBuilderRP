using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.GameCore.WorldBuilding.ModifyWorld
{
    public class ModifyWorldActionHandler
    {
        IModifyWorldAction lastModifyWorldAction;

        public void ModifyWorld(IModifyWorldAction modifyAction) {

            modifyAction?.ModifyTheWorld();
            this.lastModifyWorldAction = modifyAction;
        }

    }
}
