using Assets.Scripts.GameCore.WorldBuilding.ModifyWorld;
using UnityEngine;

namespace Assets.Scripts.GameCore.GameModes
{
    // helping cleaning class for block edit methods

    public class BuildSubModeBlockEdit
    {
        // todo

        private readonly ModifyWorldActionHandler modifyWorldActionHandler = new ModifyWorldActionHandler();

        public void RotateBlock(SymetricBlock blockToRotate, Vector3 rotateAngleToAdd)
        {
            //if (Settings.isBuildMode)
                this.modifyWorldActionHandler.ModifyWorld(new RotateBlockAction(blockToRotate, rotateAngleToAdd));
        }
    }
}
