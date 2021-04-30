using UnityEngine;

namespace Assets.Scripts.GameCore.WorldBuilding.ModifyWorld
{
    // TODO: 
    public class RemoveBlockFromTheWorldAction : IModifyWorldAction
    {
        private readonly SymBlock _blockToRemove;

        public RemoveBlockFromTheWorldAction(SymBlock blockToRemove){
            this._blockToRemove = blockToRemove;
        }


        public void ModifyTheWorld()
        {
            if(!Settings.isBuildMode)
                WorldBuilderManager.Instance.TurnBuildModeOn(); // resetuje se buildmode

            DestroyBlock();
        }

        private void DestroyBlock()
        {
            // VYMAŽEME FYZICKÝ OBJEKT
            GameObject.Destroy(_blockToRemove.BlockContainer); // TODO: udělat pool!!!!

            this.ResetNeighbourCheckers();

            // VE FINÁLE Odstraníme z db
            BlockLibrary.BlockLibrary.blocksLib.Remove(_blockToRemove);
        }

        private void ResetNeighbourCheckers() {

            // AKTUALIZUJEME OKOLNÍ KONTROLÉRY
            foreach (var b_checker in _blockToRemove.Checkers)
            {
                if (b_checker.checkerNextTo != null)
                {
                    b_checker.checkerNextTo.isEmpty = true;
                    b_checker.checkerNextTo.CreateCheckerGameObjectName();
                    b_checker.checkerNextTo.UpdateCheckerActiveState();
                }
            }
        }

       /* public void RemoveFromTheWorldBlock() // Tak jakou TODO: ...
        {
            MonoBehaviour.Destroy(_blockToRemove.BlockContainer); // Odstraníme

            foreach (Block b in BlockLibrary.BlockLibrary.blocksLib)
            {
                if (b == _blockToRemove)
                {
                    MonoBehaviour.Destroy(b.BlockContainer);

                    // Resetujeme collidery na okolních blocích
                    foreach (BlockChecker b_c in b.Checkers)
                    {
                        foreach (BlockChecker checkerNextTo in b.Checkers)
                        {
                            checkerNextTo.CreateCheckerGameObjectName();
                            checkerNextTo.isEmpty = true;
                        }
                    }

                    // Odstraníme "abstraktní" blok z databáze
                    BlockLibrary.BlockLibrary.blocksLib.Remove(b);
                    //+TODO: ještě následky pro zbytek světa (save/load....)
                    return;
                }
            }
        }*/
    }
}
