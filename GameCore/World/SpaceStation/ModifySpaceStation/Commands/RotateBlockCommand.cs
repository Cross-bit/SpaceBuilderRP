using Assets.Scripts.BlockScripts.BlockAdditional;
using UnityEngine;

namespace Assets.Scripts.GameCore.WorldBuilding.ModifyWorld
{
    public class RotateBlockCommand : IModifySpaceStationCommand
    {
        private SymetricBlock _blockToRotate;
        private Vector3 _angleToAdd;

        public RotateBlockCommand(SymetricBlock blockToRotate, Vector3 angleToAdd) {
            _blockToRotate = blockToRotate;
            _angleToAdd = angleToAdd;
        }

        public void ModifySpaceStation() => RotateSymetricBlockForCertainAngle();

        private void RotateSymetricBlockForCertainAngle()
        {
            var rotateBlock = new SymBlockRotator(Quaternion.Euler(_angleToAdd), _blockToRotate);
            rotateBlock.RotateBlockForCertainAngle(); // Prasárna... 
        }

    }
}
