using Assets.Scripts.GameCore.WorldBuilding.ModifyWorld;
using UnityEngine;

namespace Assets.Scripts.GameCore.GameModes
{
    // helping cleaning class for block edit methods

    public class BuildSubModeBlockEdit
    {

        private readonly SpaceStationModificator modifyWorldActionHandler = new SpaceStationModificator();

        public void RotateBlock(SymetricBlock blockToRotate, Vector3 rotateAngleToAdd) {

            this.modifyWorldActionHandler.ModifySpaceStation(new RotateBlockCommand(blockToRotate, rotateAngleToAdd));
        }
    }
}
