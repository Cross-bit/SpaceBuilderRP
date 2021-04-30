using System;
using UnityEngine;

namespace Assets.Scripts.GameCore.WorldBuilding.ModifyWorld
{
    public class AddBlockToWorldAction : IModifyWorldAction 
    {
        readonly World _world;
        readonly Settings.Blocks_types _blockTypeToAdd;
        readonly BlockChecker _checkerBuildingOn; // Last active checker
        public SymBlock LastPlacedBlock { get; private set; }

        //public event EventHandler<ModifyWorldEventArgs> AddBlockEvent;

        public AddBlockToWorldAction(World world, BlockChecker checkerBuildingOn, Settings.Blocks_types blockTypeToAdd) {
            _world = world;
            _blockTypeToAdd = blockTypeToAdd;
            _checkerBuildingOn = checkerBuildingOn;
        }

        public void ModifyTheWorld() {
         //   AddBlockEvent?.Invoke(this, EventArgs.Empty);
            // Necháme si světem vytvořit nový blok
            this.LastPlacedBlock = WorldBuilderManager.World.CreateNewSymBlock(new Vector3(0, 0, 0),
               new Vector3(0, 0, 0), Settings.blocksTypeLibrary[_blockTypeToAdd], _checkerBuildingOn); // 
        }
    }
}
