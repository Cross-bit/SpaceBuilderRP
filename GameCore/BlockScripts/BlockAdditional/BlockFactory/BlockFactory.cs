using Assets.Scripts.GameCore.BlockScripts.GenericBlock;
using Assets.Scripts.BlockScripts.BlockAdditional;
using UnityEngine;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System.Text;
using System;

namespace Assets.Scripts.GameCore.BlockScripts.BlockAdditional.BlockFactory
{
    public static class BlockFactory
    {
        // Symetrický blok
        public static IBlock CreateSymBlock(Vector3 position, Vector3 rotation, BlockSO BlockData, BlockChecker baseChecker) {
            return new SymBlock(position, rotation, BlockData, baseChecker);
        }

        public static SymBlockConstructor BlockConstructor(SymBlock newBlock) {
            return new SymBlockConstructor(newBlock);
        }


        // Generický blok
        public static IBlock CreateGenBlock() {
            return new GenBlock();
        }

        public static GenericBlockConstructor BlockConstructor(GenBlock newBlock) {
            return new GenericBlockConstructor(newBlock);
        }
    }
}
