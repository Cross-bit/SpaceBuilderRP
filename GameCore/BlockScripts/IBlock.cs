using System.Collections.Generic;
using Assets.Scripts.BlockScripts.BlockAdditional;
using UnityEngine;

public interface IBlock
{
    GameObject BlockContainer { get; set; }
    Vector3 BlockDimensions { get; }
    IBlockGrid BlockGrid { get; set; }
    string BlockName { get; set; }
    GameObject BlocksMainGraphics { get; set; }
    List<Collider> BlockColliders { get; set; }
    Settings.Checkers_types blocksPrimaryFunctionality { get; set; } 
    Vector3 BlockPosition { get; set; }
    Vector3 BlockRotation { get; set; }
    Settings.Blocks_types BlockType { get; set; }
    BlockChecker BaseCheckerNextTo { get; set; }
    bool HasManualNodes { get; set; }
    List<BlockChecker> Checkers { get; }
    bool IsNode { get; set; }
    bool IsOrientationValid { get; set; }
    float TimeToBuild { get; set; }

    bool ConstructBlock(IBlockConstructor blockConstructor);
    bool ConstructBlockPost(IBlockConstructor blockConstructor);
    void SetBlockOrientation();

    void AddToPathFinding();
    void BuildBlock();
    void ActivateBlock();
    List<SymetricBlock> GetBlocksInNeighbour();
}