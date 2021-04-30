using Assets.Scripts.GameCore.BlockScripts.GenericBlock;
using UnityEngine;
//silence is golden
namespace Assets.Scripts.BlockScripts.BlockAdditional
{
    public class GenericBlockConstructor : IBlockConstructor
    {
        private GenBlock _blockToConstruct;
        public IBlock BlockToConstruct { get => _blockToConstruct; set => _blockToConstruct = value as GenBlock; }

        public GenericBlockConstructor(GenBlock newBlock) {
            this.BlockToConstruct = newBlock;
        }

        public void CreateBlockCheckers() { }
        public void CreateBlockContainer() { }
        public void CreateBlockController() { }
        public void CreateBlockGraphicsGameObject() { }
        public void CreateBlockGrid() { }
        public void CreateParentTransformForCheckers() { }
        public Collider[] FindBlockColliders() { return new Collider[0]; }
        public void InitializeBlocksCheckersGraphics() { }
        public void InitializeBlocksColliders() { }
        public void SetBlockFunctionaly() { }

    }
}