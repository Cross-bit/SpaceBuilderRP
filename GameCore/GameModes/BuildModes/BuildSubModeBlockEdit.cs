using Assets.Scripts.GameCore.WorldBuilding.ModifyWorld;
using UnityEngine;

namespace Assets.Scripts.GameCore.GameModes
{
    public class BuildSubModeBlockEdit : IGameMode
    {
        private readonly ModifyWorldActionHandler modifyWorldActionHandler = new ModifyWorldActionHandler();

        public void RotateBlock(SymetricBlock blockToRotate, Vector3 rotateAngleToAdd)
        {
            //if (Settings.isBuildMode)
                this.modifyWorldActionHandler.ModifyWorld(new RotateBlockAction(blockToRotate, rotateAngleToAdd));
        }


        public void TurnModeOn() {
            // Todo
        }

        public void TurnModeOff()
        {
            // Todo
        }
    }
}
