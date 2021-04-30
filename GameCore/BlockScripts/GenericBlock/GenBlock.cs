using Assets.Scripts.BlockScripts.BlockAdditional;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using System;

namespace Assets.Scripts.GameCore.BlockScripts.GenericBlock
{
    public class GenBlock : IBlock
    {
        public GameObject BlockContainer { get; set; }
        public Vector3 BlockDimensions { get; }
        public IBlockGrid BlockGrid { get; set; }
        public string BlockName { get; set; }
        public GameObject BlocksMainGraphics { get; set; }
        public List<Collider> BlockColliders { get; set; }
        public Settings.Checkers_types blocksPrimaryFunctionality { get; set; }
        public Vector3 BlockPosition { get; set; }
        public Vector3 BlockRotation { get; set; }
        public Settings.Blocks_types BlockType { get; set; }
        public BlockChecker BaseCheckerNextTo { get; set; }
        public bool HasManualNodes { get; set; }
        public List<BlockChecker> Checkers { get; }
        public bool IsNode { get; set; }
        public bool IsOrientationValid { get; set; }
        public float TimeToBuild { get; set; }

        public List<SymBlock> GetBlocksInNeighbour() {
            return null;
        }

        public bool ConstructBlock(IBlockConstructor blockConstructor) { return false; }
        public bool ConstructBlockPost(IBlockConstructor blockConstructor) { return false; }
        public void SetBlockOrientation() { }


        public void AddToPathFinding() { }
        public void BuildBlock() { }
        public void ActivateBlock() { }
    }
}
