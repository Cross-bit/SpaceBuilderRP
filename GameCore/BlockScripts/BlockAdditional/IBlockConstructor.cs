using UnityEngine;

namespace Assets.Scripts.BlockScripts.BlockAdditional
{
    public interface IBlockConstructor
    {
        IBlock BlockToConstruct { get; }

        void CreateBlockCheckers();
        void CreateBlockContainer();
        void CreateBlockController();
        void CreateBlockGraphicsGameObject();
        void CreateBlockGrid();
        void CreateParentTransformForCheckers();
        Collider[] FindBlockColliders();
        void InitializeBlocksCheckersGraphics();
        void InitializeBlocksColliders();
        void SetBlockFunctionaly();
    }
}