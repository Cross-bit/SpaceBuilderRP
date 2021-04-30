using System;
using UnityEngine;

namespace Assets.Scripts.GameCore.BlockScripts.BlockAdditional
{

    public class BlockRotationValidator 
    {
        SymBlock _block;

        public BlockRotationValidator(SymBlock block) {
            _block = block;
        }

        public bool CheckIfRotationIsPossibleBasedOnCheckerTypes(int angle)
        {
            foreach (BlockChecker c in _block.Checkers)
            {
                // Získáme globální(world) pozici checkeru
                Vector2 c_rotated = Settings.Rotate2D(new Vector2(c.position.x, c.position.z), angle); //GetCheckerGlobalCordinates(c);
                Vector3 c_global_pos = new Vector3(c_rotated.x, c.position.y, c_rotated.y) + c.checkers_container.position;

                // Pokud je globální pozice objektu vedle, s nějakým natočeným kontrolérem
                if (c_global_pos == _block.BaseCheckerNextTo.CheckerTransform.position)
                    if (c.checkerType == _block.BaseCheckerNextTo.checkerType)
                        return true;
            }

            return false;
        }
    }
}
