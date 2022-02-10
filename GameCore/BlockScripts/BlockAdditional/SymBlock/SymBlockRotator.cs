using UnityEngine;
using Assets.Scripts.BlockScripts;
using Assets.Scripts.GameCore.BlockScripts.BlockAdditional;
using Assets.Scripts.GameCore.API.Extensions;
using System;

namespace Assets.Scripts.BlockScripts.BlockAdditional
{
    public class SymBlockRotator {

        private readonly SymetricBlock _blockToRotate;
        private readonly Quaternion _rotateForAngle;

        public SymBlockRotator(Quaternion rotateForAngle, SymetricBlock blockToRotate) {
            _rotateForAngle = rotateForAngle;
            _blockToRotate = blockToRotate;
        }

        public void RotateOnBuild() {
            RotateBlockGameObjectToGivenAngle(_blockToRotate.BlockRotation);
            RotateBlockCheckersForGivenAngle(_blockToRotate.BlockRotation);
        }

        public void RotateBlockForCertainAngle() {

            var newBlockRotation = _blockToRotate.BlockRotation + _rotateForAngle.eulerAngles;

            //Nastaví rotaci
            RotateBlockGameObjectToGivenAngle(newBlockRotation);
            RotateBlockCheckersForGivenAngle(_rotateForAngle.eulerAngles);

            // Je orientace OK?

            var blockPlacementValidator = new BlockPlacementValidator(_blockToRotate, newBlockRotation);

            if (!blockPlacementValidator.CheckIfBlocksPlacementIsValid())
            {
                UI.GameScreenEvents(Settings.GameScreenEvents.NOT_ABLE_TO); // Alert zpráva

                // Vrátí rotaci
                RotateBlockGameObjectToGivenAngle(_blockToRotate.BlockRotation);
                RotateBlockCheckersForGivenAngle(_rotateForAngle.eulerAngles * (-1));
                return;
            }

            _blockToRotate.BlockRotation = newBlockRotation;
        }

        // rotace ve scéně je obráceně => *-1
        private void RotateBlockGameObjectToGivenAngle(Vector3 rotateAngle) => _blockToRotate.BlockContainer.transform.rotation = Quaternion.Euler(rotateAngle * -1); 
        
        private void RotateBlockCheckersForGivenAngle(Vector3 rotateAngle) {
            // Přepíšeme pozice checkerů dle rotace
            foreach (BlockChecker c in _blockToRotate.Checkers)
            {
                c.position = c.position.RotateOnY(rotateAngle.y);
                c.CreateCheckerGameObjectName();
                c.UpdateCheckerActiveState();
            }
        }

        public static int FindFirstPossibleAngleToRotateSymetricBlockTo(Func<int, bool> checkTheOrientationIsValidOn, int angle = 0) {
            if (angle > 270) // Přes celou otočkou
                return angle;

            bool isValid = checkTheOrientationIsValidOn(angle);

            if (isValid)
                return angle;
            else
                return FindFirstPossibleAngleToRotateSymetricBlockTo(checkTheOrientationIsValidOn, angle + 90);
        }
    }
}
