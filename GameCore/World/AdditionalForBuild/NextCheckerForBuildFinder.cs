using Assets.Scripts.GameCore.API.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.GameCore.WorldBuilding.AdditionalForBuild
{
    class NextCheckerForBuildFinder
    {
        private SymetricBlock _blockToFindOn;

        public NextCheckerForBuildFinder(SymetricBlock blockToFindOn) {
            _blockToFindOn = blockToFindOn;
        }

        // Najde další kontrolér pro build
        public BlockChecker GetNextCheckerToBuild() {
            BlockChecker nextBlock = null;

            // Nalezneme další checker pro build
            nextBlock = Settings.LoopCheckersInList_And_ReturnByLambda(TryToGetNextCheckerForBuild, _blockToFindOn.Checkers);

            // Pokud se je kontrolér stále null, zvolíme kontrolér napravo.
            if (nextBlock == null)
                nextBlock = Settings.LoopCheckersInList_And_ReturnByLambda(TryToGetBlocksCheckerByPopulation, _blockToFindOn.Checkers);
            return nextBlock;
        }

        private BlockChecker TryToGetBlocksCheckerByPopulation(BlockChecker c) {
            Vector3 population = Settings.GetVector3Population_(c.position).RotateOnY(_blockToFindOn.BlockRotation.y);
            return c = population.z == 1 ? c : null;
        }

        private BlockChecker TryToGetNextCheckerForBuild(BlockChecker c) {
            if (c.CheckerTransform.position == _blockToFindOn.BaseCheckerNextTo.CheckerTransform.position)
            {
                foreach (BlockChecker c_potentional in _blockToFindOn.Checkers)
                {
                    if (c_potentional != c)
                        if (c.position == c_potentional.position * (-1)) // Pokud je to ten na proti
                        {
                            return c_potentional;
                        }
                }
            }
            return null;
        }

    }
}
